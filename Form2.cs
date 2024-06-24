using MySql.Data.MySqlClient;

namespace _012
{
    public partial class Form2 : Form
    {
        public static Form1 form1_;

        public Form2(Form1 form1)
        {
            InitializeComponent();
            form1_ = form1;
            textBox1_TextChanged(null, null);
            textBox2_TextChanged(null, null);
            textBox3_TextChanged(null, null);
            textBox4_TextChanged(null, null);
            //添加图片上传按钮的点击触发函数
            pictureBox1.Click += pictureBox1_Click;
            textBox1.TextChanged += textBox1_TextChanged;
            textBox2.TextChanged += textBox2_TextChanged;
            textBox3.TextChanged += textBox3_TextChanged;
            textBox4.TextChanged += textBox4_TextChanged;
            button1.Click += button1_Click;
            //设置头像
            pictureBox1.Image = form1_.RegisterPictureBox.Image;
            // 注册时检查用户名是否已经存在的方法的数据库链接,失效
            //注册注意事项
            //1.首先需要检查用户名是否已经存在
            //  1/1.链接数据库,返回数据库user_info表
            this.FormClosing += Form_Close;
        }
        
        //窗体关闭时打开form1
        public void Form_Close(object sender, FormClosingEventArgs e)
        {
            form1_.Show();
        }

        //图片上传,并显示(图片组件pictureBox1)
        //1.获取图片
        //2.将图片上传至数据库
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //当前现将图片打开,保存到客户端,等所有信息填写完成再一起上传至数据库
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //获取图片
                Bitmap bitmap = new Bitmap(openFileDialog.FileName);
                //将图片显示在图片组件中
                pictureBox1.Image = bitmap;
                pictureBox2.Image = bitmap;
            }
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                // 重新查询以检查用户名是否存在
                // 创建数据库链接进行查询
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(Form1.ConnectCfg))
                    {
                        connection.Open();
                        using (MySqlCommand command =
                               new MySqlCommand("SELECT uname FROM user_info WHERE uname = @uname", connection))
                        {
                            command.Parameters.AddWithValue("@uname", textBox1.Text);
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    label11.Text = "重名啦!>o<!!";
                                    button1.BackColor = Color.Red;
                                    button1.Enabled = false;
                                    textBox5.Text = "";
                                }
                                else
                                {
                                    button1.Enabled = true;
                                    button1.BackColor = Color.Beige;
                                    label11.Text = "名字不错-_-!";
                                    textBox5.Text = textBox1.Text;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                label11.Text = "输入用户名-.-!";
                button1.BackColor = Color.Red;
                button1.Enabled = false;
                textBox5.Text = textBox1.Text;
            }
        }

        //密码输入框检查函数
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (Form2Class.checkPasswd(textBox2, label4))
            {
                label4.Text = "芜湖~瑟克赛斯";
                //注册按钮有效
                button1.BackColor = Color.Beige;
                button1.Enabled = true;
                textBox6.Text = "用户";
                return;
            };
            //注册按钮失效
            button1.Enabled = false;
            button1.BackColor = Color.Red;
        }

        //邮箱检查函数
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (Form2Class.checkEmail(textBox3))
            {
                textBox3.ForeColor = Color.Black;
                textBox7.Text = textBox3.Text;
                //注册按钮有效
                button1.BackColor = Color.Beige;
                button1.Enabled = true;
                return;
            };
            textBox3.ForeColor = Color.Red;
            button1.Enabled = false;
            button1.BackColor = Color.Red;

        }

        //电话号码检查
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (Form2Class.checkPhone(textBox4))
            {
                textBox4.ForeColor = Color.Black;
                textBox8.Text = textBox4.Text;
                //注册按钮有效
                button1.BackColor = Color.Beige;
                button1.Enabled = true;
                return;
            };
            textBox4.ForeColor = Color.Red;
            button1.Enabled = false;
            button1.BackColor = Color.Red;
        }

        //注册按钮
        //数据中有一个ugroup字段,默认为users,需要通过管理员账号进行操作
        private void button1_Click(object sender, EventArgs e)
        {
            //首先,获取图片数据
            byte[] img_data = Form2Class.getImgData(pictureBox1);
            //链接数据库,准备插入数据
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Form1.ConnectCfg))
                {
                    connection.Open();
                    // 创建命令
                    using (MySqlCommand command =
                           new MySqlCommand("INSERT INTO user_info(uname,upwd,ugroup,uemail,uphone,utime,img_data,img_type) " +
                                            "VALUES(@uname,@upwd,@ugroup,@uemail,@uphone,@utime,@img_data,@img_type)", connection))
                    {
                        // 添加参数
                        command.Parameters.AddWithValue("@uname", textBox5.Text);
                        command.Parameters.AddWithValue("@upwd", textBox2.Text);
                        command.Parameters.AddWithValue("@ugroup", "users");
                        command.Parameters.AddWithValue("@uemail", textBox7.Text);
                        command.Parameters.AddWithValue("@uphone", textBox8.Text);
                        command.Parameters.AddWithValue("@utime", DateTime.Now);
                        command.Parameters.AddWithValue("@img_data", img_data);
                        command.Parameters.AddWithValue("@img_type", "jpg");
                        // 执行命令
                        command.ExecuteNonQuery();
                        MessageBox.Show("注册成功!");
                        //设置form1中的用户图片
                        form1_.RegisterPictureBox.Image = pictureBox1.Image;
                        //设置form1中的登陆名,这样注册成功之后就不需要在输入登陆名了
                        form1_.RegisterTextBox.Text = textBox5.Text;
                        //关闭当前窗口,显示登陆页面
                        //注册成功,发送邮箱
                        Email.WellcomeEmail(textBox7.Text, textBox5.Text);
                        form1_.Show();
                        // this.Dispose();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}