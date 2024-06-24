using System.Windows.Forms;
using AntdUI;
using MySql.Data.MySqlClient;

// using 

namespace _012;

public partial class Form1 : Form
{
    private static string connectCfg = UserInterface.connectCfg;
    public static string ConnectCfg => connectCfg;

    //图片组件,注册成功后在form2中设置form1中的图片
    public Avatar RegisterPictureBox { get; set; }

    //账户的文本框,注册成功后再form2中设置form1中的用户名(注册成功之后在登陆界面就不需要自己再输入)
    public Input RegisterTextBox { get; set; }

    //账户身份识别
    public string UserType { get; set; }

    //默认头像
    public Image DefaultPicture { get; set; }

    //数据库中所有的用户名列表
    public List<string> UserList { get; set; }

    public Form1()
    {
        //初始化组件
        InitializeComponent();
        //设置密码输入框的透明度
        textBox2.BackColor = Color.FromArgb(50, 0, 0, 0);
        //设置密码输入框字体颜色
        textBox2.ForeColor = Color.FromArgb(255, 255, 255);
        //设置账户输入框的透明度
        textBox1.BackColor = Color.FromArgb(50, 0, 0, 0);
        //设置账户输入框字体颜色
        textBox1.ForeColor = Color.FromArgb(255, 255, 255);
        //添加重置按钮(图片组件)的右键单击函数
        pictureBox1.MouseClick += PictureBox1_MouseClick;
        //添加注册按钮的点击触发函数
        button2.Click += button2_Click;
        //添加登录按钮的点击触发函数
        button1.Click += button1_Click;
        //注册时使用的图片组件
        RegisterPictureBox = pictureBox1;
        //注册时使用的用户名
        RegisterTextBox = textBox1;
        //保存当前默认头像,找不到认头像就使用默认头像
        DefaultPicture = pictureBox1.Image;
        //通过用户输入的账号来设置头像
        textBox1.TextChanged += textBox1_TextChanged;
        //忘记密码添加事件
        label5.Click += label5_Click;
        //测试:打开的时候设置头像为logo
        // pictureBox1.Image = Properties.Resources.logo;
        //打开form1时获取用户名
        UserList = Form1Class.GetUserAccount();
        textBox1.Text = RegisterTextBox.Text;
        // 测试用
        textBox1.Text = "leetoxi";
        textBox2.Text = "123456";
    }
    
    //重置按钮的右键单击触发函数
    private void PictureBox1_MouseClick(object? sender, MouseEventArgs e)
    {
        Form1Class.ReSet(textBox1, textBox2);
        //throw new NotImplementedException();
        //textBox1_TextChanged();
    }

    //登录按钮
    private void button1_Click(object sender, EventArgs e)
    {
        //获取当前账号和密码框的输入内容
        string account = textBox1.Text;
        string password = textBox2.Text;
        //检测输入内容
        if (account == "" || password == "")
        {
            MessageBox.Show("账号或密码不能为空");
            return;
        }

        try
        {
            //创建数据库链接
            MySqlConnection connect = new MySqlConnection(connectCfg);
            //链接数据库
            connect.Open();
            // MessageBox.Show("链接成功");
            //执行查询语句
            MySqlCommand command =
                new MySqlCommand($"select * from user_info where uname = @account", connect);
            command.Parameters.AddWithValue("@account", account);
            //获取查询到的数据
            MySqlDataReader reader = command.ExecuteReader();
            //判断账号密码是否正确
            if (reader.Read())
            {
                //获取数据库中的密码
                string dbPassword = reader.GetString("upwd");
                //设置用户身份
                UserType = reader.GetString("ugroup") == "admin" ? "管理员" : "用户";
                //注册时使用的图片组件
                RegisterPictureBox = pictureBox1;
                //注册时使用的用户名
                RegisterTextBox = textBox1;
                //判断密码是否正确
                if (dbPassword == password)
                {
                    //密码正确
                    MessageBox.Show($"登录成功\n你好! {reader.GetString("uname")}[{reader.GetString("ugroup")}]");
                    //提示窗弹出之后使用语音播报功能
                    
                    //跳转到主页
                    // Form2 form2 = new Form2();
                    // form2.Show();
                    new Form4(form1: this).Show();
                    //隐藏当前窗口
                    this.Hide();
                }
                else
                {
                    //账号不存在
                    MessageBox.Show("密码不对>_<");
                }
            }
            else
            {
                //账号不存在
                MessageBox.Show("账号不存在");
                // Form1Class.ReSet(textBox1, textBox2);
            }
        }
        catch (Exception ex)
        {
            // MessageBox.Show(ex.Message);
            //查不到用户名
            if (ex.Message == "Unknown column 'account' in 'where clause'")
            {
                MessageBox.Show("账号不存在");
                Form1Class.ReSet(textBox1, textBox2);
            }

            // MessageBox.Show(ex.ToString());
        }
    }

    //注册按钮
    private void button2_Click(object sender, EventArgs e)
    {
        //注册时需要检查密码
        //密码输入框需要检查输入的密码是否符合格式(checkPasswd)
        //1.包含特殊符号
        //2.包含大小写字母
        //3.包含数字

        // if (!Form1Class.checkPasswd(textBox2))
        // {
        //     return;
        // }
        //隐藏登陆窗口,打开注册窗口
        new Form2(this).Show();
        this.Hide();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
    }


    //输入用户名时获取头像的函数
    //输入用户名时,检测是否曾在头像,如果存在则设置为用户头像,不存在则使用默认头像
    //每次文本框的内容变动都需要调动数据库,所以需要优化
    //当前的解决方案是让函数在一段时间内只执行一次,目前设置的是300毫秒
    //每过300毫秒才会执行一次啊下面的操作,这样的话就可以大大降低数据库的访问次数
    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        // 创建节流器实例，指定最小间隔时间为300毫秒
        Throttle throttler = new Throttle();
        //经过测试,260ms比较好
        throttler.ThrottledExecute(TimeSpan.FromMilliseconds(260), () =>
        {
            //节流器执行函数
        });
        //下面的函数对服务器带宽要求过高,现在改进方法如下
        //函数的功能是用户边输入程序边检测是否存在该用户名,如果存在则获取头像
        //但用户的输入频率过高导致下面的函数频繁执行,所以需要优化
        //上面使用Throttle节流器,但效果不佳,所以准备采用缓存用户信息的方式
        //首先需要判断用户输入的用户名是否在客户端用户名列表中,存在才获取头像
        bool exists = UserList.Any(item => textBox1.Text == item);
        if (exists)
        {
            //开启数据库
            try
            {
                MySqlConnection connect = new MySqlConnection(connectCfg);
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
                    // int imgdata_index = reader.GetOrdinal("img_data");
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
        else
        {
            // MessageBox.Show("不存在");
            //设置图片组件的图片
            pictureBox1.Image = DefaultPicture;
        }
    }

    //忘记密码
    private void label5_Click(object sender, EventArgs e)
    {
        //隐藏登陆窗口,打开忘记密码窗口
        new Form3(this).Show();
        this.Hide();
    }
}