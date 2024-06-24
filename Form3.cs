using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
// using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace _012
{
    public partial class Form3 : Form
    {
        //默认头像
        private Image DefaultPicture;

        //创建验证码存储变量
        private int EmailVerifyCode;

        //创建定时器,用于发送验证码之后进行时间限制
        private System.Windows.Forms.Timer timer;

        //创建验证码有效时间
        private int EmailVerifyTime;
        private Form1 form1_;

        public Form3(Form1 form1)
        {
            //链接form1
            form1_ = form1;
            InitializeComponent();
            DefaultPicture = pictureBox1.Image;
            //用户名输入框添加触发函数
            textBox1.TextChanged += textBox1_TextChanged;
            //初始化定时器
            timer = new System.Windows.Forms.Timer();
            //设置触发时间间隔1000ms
            timer.Interval = 1000;
            //添加定时器触发函数
            timer.Tick += timer_Tick;
            //发送验证码按钮添加函数
            button1.Click += button1_Click;
            //提交修改密码添加函数
            button2.Click += button2_Click;
            //关闭窗体时打开form1
            this.FormClosing += Form_Close;
        }

        //窗体关闭时打开form1
        public void Form_Close(object sender, FormClosingEventArgs e)
        {
            form1_.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        //设置头像的函数
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // 创建节流器实例，指定最小间隔时间为300毫秒
            Throttle throttler = new Throttle();
            //经过测试,260ms比较好
            throttler.ThrottledExecute(TimeSpan.FromMilliseconds(260), () =>
            {
                //节流器执行函数
            });
            //开启数据库
            try
            {
                MySqlConnection connect = new MySqlConnection(Form1.ConnectCfg);
                connect.Open();
                MySqlCommand command =
                    new MySqlCommand($"select img_data from user_info where uname = @uname", connect);
                command.Parameters.AddWithValue("@uname", textBox1.Text);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) //判断用户名是否存在
                {
                    // MessageBox.Show("存在");
                    //设置图片组件的图片
                    //获得img_data列的索引
                    int imgdata_index = reader.GetOrdinal("img_data");
                    //获得img_data的长度
                    //参数解读
                    //参数1:列索引
                    //参数2:偏移量,当前偏移量为0,从零开始读取数据
                    //参数3:字节数组,存储读取的数据,null则不返回数据,返回的是读取的二进制数据的总长度
                    //参数4:参数3的数组的偏移量,为0,表示从零开始(参数3为null,下面这条语句的参数4没有实际作用)
                    //参数5:限制从reader读取的字节数量,设置为0则是不限制,意思是读取所有数据
                    long length = reader.GetBytes(imgdata_index, 0, null, 0, 0);
                    //上面length返回的是需要获取数据的字节长度
                    //用于创建一个相等长度的二进制数组来保存二进制格式的图片
                    // 创建一个对应长度的二进制数组
                    byte[] imageBytes = new byte[length];
                    // 从reader中获取数据,
                    // 参数1:索引依然是那个索引
                    // 参数2:偏移量,当前偏移量为0,从零开始读取数据
                    // 参数3:用来保存读取到的数据,上面的语句是null,则是不保存,返回长度
                    //      下面的是imageBytes,所以是将读取到的数据保存到二进制数组imageBytes中
                    // 参数4:保存数据的偏移量,当前不需要便宜,所以为0
                    // 参数5:限制从reader读取的字节数量,设置为0则是不限制,意思是读取所有数据
                    //      下面的语句语句获取到了相对应的二进制数据的长度,所以使用的是当前的长度length,理论上0也可以(自己的猜测`)
                    // 读取数据到二进制数组
                    reader.GetBytes(imgdata_index, 0, imageBytes, 0, (int)length);
                    //获取到的图片当前还是以二进制的形式存在imageBytes中,所以需要将其转换为图片
                    //创建一个内存流,将二进制数组imageBytes写入到内存流中
                    MemoryStream ms = new MemoryStream(imageBytes);
                    //通过数据流ms来设置图片
                    using (ms)
                    {
                        // 设置图片组件的图片
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    // MessageBox.Show("不存在");
                    //设置图片组件的图片
                    pictureBox1.Image = DefaultPicture;
                }
            }
            catch (Exception ex)
            {
                //当前存在异常
                //leetoxi为管理员用户,但是没有头像,导致获取二进制图片数据时获取不到
                //MessageBox.Show(ex.Message);
            }
        }

        //发送验证码按钮button1
        private void button1_Click(object sender, EventArgs e)
        {
            //链接数据库,查询邮箱
            try
            {
                //创建数据库链接
                MySqlConnection connect = new MySqlConnection(Form1.ConnectCfg);
                connect.Open();
                //创建查询语句
                MySqlCommand command = new MySqlCommand($"select uemail from user_info where uname = @uname", connect);
                //添加查询变量
                command.Parameters.AddWithValue("@uname", textBox1.Text);
                //执行查询
                MySqlDataReader reader = command.ExecuteReader();
                //判断用户名是否存在
                if (reader.Read())
                {
                    //用户存在,开始执行发送验证码函数
                    //首先,获取邮箱
                    string email = reader.GetString("uemail");
                    //执行发送验证码函数
                    EmailVerifyCode = Email.SendVerificationCode(email, textBox1.Text);
                    //验证码发送成功,开始倒计时,600秒内有效
                    EmailVerifyTime = 600;
                    timer.Start();
                }
                else
                {
                    MessageBox.Show("没有这个用户,是不是输错啦?");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //验证码倒计时函数
        private void timer_Tick(object sender, EventArgs e)
        {
            //验证码有效时间
            // int time = 600;
            if (EmailVerifyTime > 0)
            {
                button2.BackColor = Color.FloralWhite;
                //设置显示组件
                label3.Text = $"{EmailVerifyTime}s 内有效";
                //设置按钮可用
                button2.Enabled = true;
                //每秒减一
                EmailVerifyTime--;
            }
            else
            {
                label3.Text = "验证码已失效";
                button2.Enabled = false;
                button2.BackColor = Color.Red;
                timer.Stop();
            }
        }

        //在验证码有效期间,输入验证码,并输入密码,点击提交,以更改密码,函数在提交按钮上执行
        private void button2_Click(object sender, EventArgs e)
        {
            //首先检查验证码是否正确
            if (textBox2.Text != EmailVerifyCode.ToString())
            {
                MessageBox.Show("验证码错误");
                return;
            }

            //如果正确,则开始检查新设置的密码是否符合规范
            if (!Form2Class.checkPasswd(textBox3, label7)) return;
            //如果符合密码规范,则开始设置密码
            try
            {
                //创建数据库链接
                MySqlConnection connect = new MySqlConnection(Form1.ConnectCfg);
                //打开数据库链接
                connect.Open();
                //创建查询语句
                MySqlCommand command =
                    new MySqlCommand($"update user_info set upwd = @passwd where uname = @uname", connect);
                //添加变量
                command.Parameters.AddWithValue("@uname", textBox1.Text);
                command.Parameters.AddWithValue("@passwd", textBox3.Text);
                //执行查询语句
                command.ExecuteNonQuery();
                MessageBox.Show("密码修改成功");
                //设置登录界面的用户名
                form1_.RegisterTextBox.Text = textBox1.Text;
                //显示登录窗口
                //关闭当前窗口
                form1_.Show();
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}