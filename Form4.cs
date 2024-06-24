using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AntdUI;
using MySql.Data.MySqlClient;

namespace _012
{
    public partial class Form4 : Form
    {
        public Form1 form1_;
        public List<Book> books;
        private List<WorkOrder> workorders;
        private User user = new User();

        //创建验证码存储变量
        private int EmailVerifyCode;

        private int EmailVerifyCode_right = 0;

        //创建定时器,用于发送验证码之后进行时间限制
        private System.Windows.Forms.Timer timer;

        private System.Windows.Forms.Timer timer_right;

        //创建验证码有效时间
        private int EmailVerifyTime;

        //创建用户列表
        List<User> users = Form4Class.GetAllUsers();

        // private Form1 form1_;
        public Form4(Form1 form1)
        {
            //加载form1
            form1_ = form1;
            //窗体初始化
            InitializeComponent();
            //设置用户信息
            try
            {
                user.uname = form1_.RegisterTextBox.Text;
                user.GetUserInfo();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }

            // Form1.user000.
            MessageBox.Show("登录成功");
            //创建保存图书信息的变量
            books = Form4Class.GetAllBooks();
            //创建保存工单信息的变量
            workorders = getWorkOrder();
            //备份图书数据
            // var books_backup = new List<Book>(books);
            //用户类型,如果不是管理员,则隐藏管理界面
            if (form1_.UserType == "用户")
            {
                // MessageBox.Show("该账号不是管理员");
                tabPage7.Parent = null;
            }

            //公告列表加载数据
            // dataGridView1.DataSource = dataGridView1_LoadNotices(dataGridView1);
            dataGridView1_LoadNotices(dataGridView1);
            //图书推荐列表加载数据
            DataGridView2_LoadBookInfo(dataGridView2);
            //设置头像
            pictureBox1.Image = form1_.RegisterPictureBox.Image;
            //设置用户名
            label2.Text = form1_.RegisterTextBox.Text;
            //设置用户组
            label3.Text = form1_.UserType;
            //设置label4
            label4.Text = "今天是" +
                          DateTime.Now.ToString("yyyy-MM-dd") +
                          ",啊,真是美好的一天\n-----leetoxi";
            this.FormClosing += Form_Close;

            //工单管理界面加载数据
            Table1_load(table1, workorders);
            //工单搜索界面
            input1.TextChanged += Search_TextChanged_Table1;
            //工单处理按钮
            button5.Click += button_Click;
            button6.Click += button_Click;
            //工单处理界面刷新按钮
            button7.Click += button7_Click;

            //加载借书列表
            datagridView3_Load(dataGridView3, books); //借书界面
            datagridView3_Load(dataGridView4, books); //还书界面
            //搜索框搜索书籍,在借书和查找书籍界面添加
            textBox2.TextChanged += Search_TextChanged_lend;
            textBox3.TextChanged += Search_TextChanged_lend;
            textBox4.TextChanged += Search_TextChanged_lend;
            textBox5.TextChanged += Search_TextChanged_lend;
            //借书页面获得相关信息
            dataGridView3.CellClick += dataGridView3_CellClick;
            //搜索界面点击表格获取相关信息
            dataGridView4.CellClick += dataGridView4_CellClick;
            //借按钮添加函数
            button2.Click += button2_Click;
            //查找书籍界面
            textBox6.TextChanged += Search_TextChanged_SEARCH;
            textBox7.TextChanged += Search_TextChanged_SEARCH;
            textBox8.TextChanged += Search_TextChanged_SEARCH;
            textBox9.TextChanged += Search_TextChanged_SEARCH;
            //借书界面和搜书界面添加手动搜索按钮
            button1.Click += button4_Click;
            button4.Click += button4_Click;

            //用户还书界面代码
            //还书界面表格加载数据
            Table2_load(table2, workorders);
            //还书界面手动刷新表格按钮
            button8.Click += button8_Click;
            //还书页面通过isbn搜索工单
            input2.TextChanged += Search_TextChanged_Table2;
            //还书界面button11
            button11.Click += button11_Click;

            //验证码函数
            //实例化timer
            timer = new System.Windows.Forms.Timer();
            timer_right = new System.Windows.Forms.Timer();
            //设置触发时间间隔1000ms
            timer.Interval = 1000;
            timer_right.Interval = 1000;
            //添加定时器触发函数
            timer.Tick += timer_Tick;
            timer_right.Tick += timer_Tick_pwd;
            //个人信息界面
            //加载用户数据
            tabPage4_Load();
            //左下更改邮箱验证码时的发送验证码按钮
            button10.Click += button10_Click;
            //左下用户提交更改信息
            button12.Click += button12_Click;
            //右下角发送验证码
            button13.Click += button13_Click;
            //右下角密码修改提交按钮
            button14.Click += button14_Click;

            //图书录入界面
            button15.Click += button15_Click;

            //图书修改界面,数据加载
            dataGridView5_load(books);
            //图书修改界面,数据搜索
            // textBox1.TextChanged += Search_TextChanged_Manager;
            // textBox10.TextChanged += Search_TextChanged_Manager;
            // textBox11.TextChanged += Search_TextChanged_Manager;
            textBox12.TextChanged += Search_TextChanged_Manager;
            //图书修改界面点击显示图书详细信息
            table4.CellClick += table4_CellClick;
            //图书修改界面下架按钮
            button17.Click += button17_Click;
            //图书修改界面
            button16.Click += button16_Click;
            //用户管理界面
            try
            {
                table3_Load(users);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            //用户管理搜索界面
            textBox16.TextChanged += Search_TextChanged_UsersManager;
            //用户管理页面选择函数
            table3.CellClick += table3_CellClick;
            //用户管理页面修改提交按钮
            button20.Click += button20_Click;
        }

        //工单界面的刷新按钮button7
        private void button7_Click(object sender, EventArgs e)
        {
            //刷新工单列表
            workorders = getWorkOrder();
            Table1_load(table1, workorders);
        }

        //工单界面的处理按钮,表示该工单处理完成button5和button6
        private void button_Click(object sender, EventArgs e)
        {
            string target = "已处理";
            //判断是那个按钮点击
            if (sender == button5)
            {
                target = "已处理";
            }
            else if (sender == button6)
            {
                target = "已拒绝";
            }

            //如果selectedIndex为-1,则是没有选中
            if (table1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择工单进行操作");
                return;
            }

            // MessageBox.Show(workorder_id);
            //更新工单状态
            // 连接数据库
            try
            {
                //获取选择到的工单的工单号
                string workorder_id = workorders[table1.SelectedIndex - 1].wid.ToString();

                using (MySqlConnection connect = new MySqlConnection(Form1.ConnectCfg))
                {
                    connect.Open();
                    string sql =
                        "update work_order set wstatus = @target,wpublisher=@uname,wdate_process = @date where wid = @wid";
                    MySqlCommand command = new MySqlCommand(sql, connect);
                    command.Parameters.AddWithValue("@target", target);
                    command.Parameters.AddWithValue("@wid", workorder_id);
                    command.Parameters.AddWithValue("@uname", form1_.RegisterTextBox.Text);
                    command.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                    command.ExecuteNonQuery();
                }

                //刷新工单列表
                workorders = getWorkOrder();
                Table1_load(table1, workorders);
            }
            catch (Exception exception)
            {
                // Console.WriteLine(exception);
                // AntdUI.
                MessageBox.Show(exception.Message);
                throw;
            }
        }


        //工单处理界面的查询按钮button3
        private void button3_Click(object sender, EventArgs e)
        {
            //调用更新工单列表的函数
            Search_TextChanged_Table1(null, null);
        }

        //窗体关闭时打开form1
        public void Form_Close(object sender, FormClosingEventArgs e)
        {
            form1_.Show();
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        //公告列表加载数据
        private void dataGridView1_LoadNotices(DataGridView dataGridViewNotices)
        {
            List<Notice> list = Form4Class.GetAllNotices();
            dataGridViewNotices.AutoGenerateColumns = true; // 自动为数据源生成列
            dataGridViewNotices.DataSource = null; // 清空现有数据源
            dataGridViewNotices.DataSource = list;
            //更改列名
            dataGridViewNotices.Columns["Ncontent"].HeaderText = "公告内容";
            dataGridViewNotices.Columns["Npublisher"].HeaderText = "发布人";
            dataGridViewNotices.Columns["Ndate"].HeaderText = "发布时间";
            //设置相对列宽
            dataGridViewNotices.Columns[1].FillWeight = 0.6f;
            dataGridViewNotices.Columns[2].FillWeight = 0.2f;
            dataGridViewNotices.Columns[0].FillWeight = 0.2f;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // dataGridView1.RowHeadersWidth = 10;
            dataGridView1.RowHeadersVisible = false;
        }

        //每日推荐,现阶段推荐采取的是随机推荐
        //考虑的是根据每个学生的借阅记录,来推荐图书
        private void DataGridView2_LoadBookInfo(DataGridView dataGridViewBookInfo)
        {
            dataGridViewBookInfo.AutoGenerateColumns = false; // 不自动为数据源生成列
            dataGridViewBookInfo.DataSource = null; // 清空现有数据源
            dataGridViewBookInfo.RowHeadersVisible = false; //删除开头的空列
            //增加数据列5列
            DataGridViewTextBoxColumn column0 = new DataGridViewTextBoxColumn();
            column0.DataPropertyName = "Bname"; // 数据源中的字段名
            column0.HeaderText = "书名"; // 显示在列头的文本
            dataGridViewBookInfo.Columns.Add(column0);
            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.DataPropertyName = "Bauthor";
            column1.HeaderText = "作者";
            dataGridViewBookInfo.Columns.Add(column1);
            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.DataPropertyName = "Bpublisher";
            column2.HeaderText = "出版社";
            dataGridViewBookInfo.Columns.Add(column2);
            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.DataPropertyName = "Bcategory";
            column3.HeaderText = "类别";
            dataGridViewBookInfo.Columns.Add(column3);
            //加载数据源(随机抽取12本书)
            Random random = new Random();
            var books_out = new List<Book>();
            var books_tmp = new List<Book>(books);
            for (int i = 0; i < 11; i++)
            {
                int index = random.Next(books_tmp.Count);
                books_out.Add(books_tmp[index]);
                books_tmp.RemoveAt(index);
            }

            dataGridViewBookInfo.DataSource = books_out;
            //设置列宽
            dataGridViewBookInfo.Columns[0].FillWeight = 0.4f;
            dataGridViewBookInfo.Columns[1].FillWeight = 0.2f;
            dataGridViewBookInfo.Columns[2].FillWeight = 0.2f;
            dataGridViewBookInfo.Columns[3].FillWeight = 0.2f;
            dataGridViewBookInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        //还书界面,第一次运行时加载所有的书籍,然后通过各类搜索来列入所获取到的书籍
        private void datagridView3_Load(DataGridView dataGridView3, List<Book> books)
        {
            //关闭自动设置列名
            dataGridView3.AutoGenerateColumns = true;
            dataGridView3.DataSource = null; //清空数据
            dataGridView3.RowHeadersVisible = false; //删除开头的空列
            //第一例设置
            DataGridViewTextBoxColumn column0 = new DataGridViewTextBoxColumn();
            column0.DataPropertyName = "ISBN"; //数据源中的字段名
            column0.HeaderText = "ISBN";
            dataGridView3.Columns.Add(column0);
            //第二例设置
            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.DataPropertyName = "Bname"; // 数据源中的字段名
            column1.HeaderText = "书名"; // 显示在列头的文本
            dataGridView3.Columns.Add(column1);
            //第三例设置
            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.DataPropertyName = "Bauthor";
            column2.HeaderText = "作者";
            dataGridView3.Columns.Add(column2);
            //第四例设置
            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.DataPropertyName = "Bpublisher";
            column3.HeaderText = "出版社";
            dataGridView3.Columns.Add(column3);
            //第五例设置
            DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
            column4.DataPropertyName = "Bcategory";
            column4.HeaderText = "类别";
            dataGridView3.Columns.Add(column4);
            //第六列设置
            DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();
            column5.DataPropertyName = "Bquantity";
            column5.HeaderText = "库存";
            dataGridView3.Columns.Add(column5);
            //第七列
            DataGridViewTextBoxColumn column6 = new DataGridViewTextBoxColumn();
            column6.DataPropertyName = "Blend";
            column6.HeaderText = "已借出";
            dataGridView3.Columns.Add(column6);
            //加载数据
            dataGridView3.DataSource = books;
            //列宽自动填充
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        //借书界面搜索框
        private void Search_TextChanged_lend(object sender, EventArgs e)
        {
            //判断四个搜索框为空则返回原本的书籍列表
            if (textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
            {
                datagridView3_Load(dataGridView3, books);
                return;
            }

            //获取搜索的条件
            List<string> search_list = new List<string>()
                { textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text };
            //创建要展示的书籍列表
            //复制书籍列表
            List<Book> books_tmp = new List<Book>(books);
            List<Book> books_out = new List<Book>();
            books_out.Clear();
            try
            {
                foreach (var item in books_tmp)
                {
                    if (
                        (!string.IsNullOrEmpty(search_list[0]) && item.ISBN.Contains(search_list[0])) ||
                        (!string.IsNullOrEmpty(search_list[1]) && item.Bname.Contains(search_list[1])) ||
                        (!string.IsNullOrEmpty(search_list[2]) && item.Bauthor.Contains(search_list[2])) ||
                        (!string.IsNullOrEmpty(search_list[3]) && item.Bpublisher.Contains(search_list[3]))
                    )
                    {
                        books_out.Add(item);
                    }
                }

                //显示书籍
                datagridView3_Load(dataGridView3, books_out);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        //搜书界面搜索框
        private void Search_TextChanged_SEARCH(object sender, EventArgs e)
        {
            //判断四个搜索框为空则返回原本的书籍列表
            if (textBox9.Text == "" && textBox8.Text == "" && textBox7.Text == "" && textBox6.Text == "")
            {
                datagridView3_Load(dataGridView4, books);
                return;
            }

            //获取搜索的条件
            List<string> search_list = new List<string>()
                { textBox9.Text, textBox8.Text, textBox7.Text, textBox6.Text };
            //创建要展示的书籍列表
            //复制书籍列表
            List<Book> books_tmp = new List<Book>(books);
            List<Book> books_out = new List<Book>();
            books_out.Clear();
            try
            {
                foreach (var item in books_tmp)
                {
                    if (
                        (!string.IsNullOrEmpty(search_list[0]) && item.ISBN.Contains(search_list[0])) ||
                        (!string.IsNullOrEmpty(search_list[1]) && item.Bname.Contains(search_list[1])) ||
                        (!string.IsNullOrEmpty(search_list[2]) && item.Bauthor.Contains(search_list[2])) ||
                        (!string.IsNullOrEmpty(search_list[3]) && item.Bpublisher.Contains(search_list[3]))
                    )
                    {
                        books_out.Add(item);
                    }
                }

                //显示书籍
                datagridView3_Load(dataGridView4, books_out);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void tabPage12_Click(object sender, EventArgs e)
        {
        }

        //手动查询按钮
        private void button4_Click(object sender, EventArgs e)
        {
            //判断是button4还是button1
            if (sender == button1)
            {
                Search_TextChanged_lend(sender, e);
                return;
            }

            Search_TextChanged_SEARCH(sender, e);
        }

        //点击书的列表之后的操作函数-->借书界面
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //设置书名
                label12.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
                //设置作者
                label13.Text = dataGridView3.CurrentRow.Cells[2].Value.ToString();
                //设置出版社
                label14.Text = dataGridView3.CurrentRow.Cells[3].Value.ToString();
                //设置类别
                label16.Text = dataGridView3.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        //点击书的列表之后的操作函数-->搜索书籍界面
        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //设置书名
                label20.Text = dataGridView4.CurrentRow.Cells[1].Value.ToString();
                //设置作者
                label17.Text = dataGridView4.CurrentRow.Cells[2].Value.ToString();
                //设置出版社
                label19.Text = dataGridView4.CurrentRow.Cells[3].Value.ToString();
                //设置类别
                label18.Text = dataGridView4.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        //点击借按钮时的函数
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string ISBN = dataGridView3.CurrentRow.Cells[0].Value.ToString();
                if (ISBN == "")
                {
                    MessageBox.Show("请选择要借的书");
                    return;
                }

                //链接数据库.提交借书预约信息
                string sql = "insert into work_order(uname,wdate_submit,wtype,ISBN,wstatus)" +
                             "values (@uname,@wdate_submi,@wtype,@ISBN,@wstatus)";
                using (MySqlConnection conn = new MySqlConnection(Form1.ConnectCfg))
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(sql, conn);
                    command.Parameters.AddWithValue("@uname", label2.Text);
                    command.Parameters.AddWithValue("@wdate_submi", DateTime.Now);
                    command.Parameters.AddWithValue("@wtype", "借");
                    command.Parameters.AddWithValue("@ISBN", ISBN);
                    command.Parameters.AddWithValue("@wstatus", "未处理");
                    command.ExecuteNonQuery();
                    MessageBox.Show("借书预约成功\n请到图书馆取书");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        //工单处理界面
        //从数据库加载工单内容
        private List<WorkOrder> getWorkOrder()
        {
            List<WorkOrder> workorders = new List<WorkOrder>();
            //链接数据库
            try
            {
                //按照时间降序排序
                string sql = "select * from work_order order by wdate_submit desc";
                using (MySqlConnection conn = new MySqlConnection(Form1.ConnectCfg))
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        WorkOrder workorder = new WorkOrder();
                        workorder.wid = reader.GetInt32(0);
                        workorder.uname = reader.GetString(1);
                        workorder.wdate_submit = reader.GetDateTime(2).ToString("yyyy-MM-dd");
                        workorder.wtype = reader.GetString(3);
                        workorder.ISBN = reader.GetString(4);
                        workorder.wstatus = reader.GetString(5);
                        workorder.wpublisher = reader.GetString(6);
                        workorder.wdate_process = reader.GetDateTime(7).ToString("yyyy-MM-dd");
                        workorders.Add(workorder);
                    }
                }

                return workorders;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //加载工单数据到工单gridPanel
        private void Table1_load(Table table, List<WorkOrder> workorders)
        {
            try
            {
                //清空数据
                table.DataSource = null;
                table.DataSource = workorders;
                // table.Columns[0].Visible = false;
                // table.Columns[0].Key = "wid";
                table.Columns = new Column[]
                {
                    new Column("wid", "工单号"),
                    new Column("uname", "用户名"),
                    new Column("wdate_submit", "提交时间"),
                    new Column("wtype", "类型"),
                    new Column("ISBN", "ISBN"),
                    new Column("wstatus", "状态"),
                    new Column("wpublisher", "处理人"),
                    new Column("wdate_process", "处理时间")
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        //工单处理界面的搜索框搜索用户名
        private void Search_TextChanged_Table1(object sender, EventArgs e)
        {
            //创建临时工单数据
            List<WorkOrder> workorders_tmp = new List<WorkOrder>();
            try
            {
                if (input1.Text == "")
                {
                    Table1_load(table1, workorders);
                }
                else
                {
                    //搜索用户名
                    foreach (WorkOrder workorder in workorders)
                    {
                        if (workorder.uname.Contains(input1.Text))
                        {
                            workorders_tmp.Add(workorder);
                        }
                    }

                    Table1_load(table1, workorders_tmp);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void avatar1_Click(object sender, EventArgs e)
        {
        }

        private void panel7_Click(object sender, EventArgs e)
        {
        }

        //还书界面
        //table2加载工单数据
        private void Table2_load(Table table, List<WorkOrder> workorders)
        {
            try
            {
                //首先处理数据,因为还书只需要还自己的
                List<WorkOrder> workorders_tmp = new List<WorkOrder>();
                foreach (WorkOrder workorder in workorders)
                {
                    if (workorder.uname == label2.Text &&
                        workorder.wstatus == "未处理" &&
                        workorder.wtype == "借")
                    {
                        workorders_tmp.Add(workorder);
                    }
                }

                //获取完成,展示数据
                table.DataSource = null;
                table.DataSource = workorders_tmp;
                table.Columns = new Column[]
                {
                    new Column("wid", "工单号"),
                    new Column("uname", "用户名"),
                    new Column("wdate_submit", "提交时间"),
                    new Column("wtype", "类型"),
                    new Column("ISBN", "ISBN"),
                    new Column("wstatus", "状态"),
                    new Column("wpublisher", "处理人"),
                    new Column("wdate_process", "处理时间")
                };
                if (workorders_tmp.Count == 0)
                {
                    label3.Text = "暂无工单";
                }
                else
                {
                    label3.Text = "";
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        //还书界面手动刷新按钮button8
        private void button8_Click(object sender, EventArgs e)
        {
            workorders = getWorkOrder();
            Table2_load(table2, workorders);
        }

        //还书界面搜索工单的input2,通过搜索ISBN来查询工单
        private void Search_TextChanged_Table2(object sender, EventArgs e)
        {
            //如果搜索框被清空
            if (input2.Text == "")
            {
                workorders = getWorkOrder();
                Table2_load(table2, workorders);
                return;
            }

            //获取当前工单数据
            List<WorkOrder> workorders_tmp = new List<WorkOrder>();
            foreach (WorkOrder workorder in workorders)
            {
                if (workorder.uname == label2.Text &&
                    workorder.wstatus == "未处理" &&
                    workorder.ISBN.Contains(input2.Text) &&
                    workorder.wtype == "借"
                   )
                {
                    workorders_tmp.Add(workorder);
                }
            }

            //加载数据
            Table2_load(table2, workorders_tmp);
        }

        //button11手动查询按钮
        private void button11_Click(object sender, EventArgs e)
        {
            Search_TextChanged_Table2(sender, e);
        }

        //还书按钮button9
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                //首先获取工单
                if (table2.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择要还的书");
                    return;
                }

                //获取工单id
                string wid = workorders[table2.SelectedIndex].wid.ToString();
                //开启数据库链接
                using (MySqlConnection connection = new MySqlConnection(Form1.ConnectCfg))
                {
                    connection.Open();
                    //提交还书工单
                    string sql = "Insert into work_order(uname,wdate_submit,wtype,ISBN,wstatus) " +
                                 "values(@uname,@wdate_submit,'还',@ISBN,'未处理')";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@uname", label2.Text);
                    command.Parameters.AddWithValue("@wdate_submit", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@ISBN", workorders[table2.SelectedIndex].ISBN);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                //还书工单成功
                MessageBox.Show("预约还书成功!\n请携书前往图书馆");
            }
            catch (Exception exception)
            {
                MessageBox.Show("异常");
                throw;
            }
        }

        private void label47_Click(object sender, EventArgs e)
        {
        }

        //个人信息页面
        //个人信息界面加载数据
        private void tabPage4_Load()
        {
            //设置头像
            pictureBox2.Image = form1_.RegisterPictureBox.Image;
            avatar3.Image = form1_.RegisterPictureBox.Image;
            //设置用户名
            label31.Text = user.uname;
            label33.Text = user.uname;
            //设置用户组
            label30.Text = user.ugroup;
            label34.Text = user.ugroup;
            //设置邮箱
            label36.Text = user.uemail;
            //设置电话号码
            label37.Text = user.uphone;
        }

        //个人信息界面的左下侧发送验证码,只有更改邮箱的时候需要发送,其它信息直接修改
        private void button10_Click(object sender, EventArgs e)
        {
            //邮箱为空,说明该用户不修改邮箱
            if (input4.Text == "")
            {
                MessageBox.Show("修改邮箱才需要发送验证码");
                return;
            }

            //修改了邮箱,所以需要发送验证码
            string email = user.uemail;
            //执行发送验证码函数
            EmailVerifyCode = Email.SendVerificationCode(email, user.uname);
            //验证码发送成功,开始倒计时,600秒内有效
            EmailVerifyTime = 600;
            timer.Start();
        }

        //验证码发送函数
        private void timer_Tick(object sender, EventArgs e)
        {
            //验证码有效时间
            // int time = 600;
            if (EmailVerifyTime > 0)
            {
                button12.BackColor = Color.FloralWhite;
                //设置显示组件
                label41.Text = $"{EmailVerifyTime}s 内有效";
                //设置按钮可用
                button12.Enabled = true;
                //每秒减一
                EmailVerifyTime--;
            }
            else
            {
                label41.Text = "验证码已失效";
                button12.Enabled = false;
                button12.BackColor = Color.Red;
                timer.Stop();
            }
        }

        //修改个人信息时的提交按钮,首先用户是否更改邮箱,更改的话需要查看是否有验证码,没有则需要发送验证码
        private void button12_Click(object sender, EventArgs e)
        {
            bool isChangeEmail = false;
            // 创建临时数据
            User user_tmp = user;
            // 首先用户是否更改邮箱,更改的话需要查看是否有验--嶳鬱™证码,没有则需要发送验证码
            if (input4.Text != "")
            {
                if (input6.Text != EmailVerifyCode.ToString())
                {
                    MessageBox.Show("验证码错误");
                    return;
                }
                else if (input6.Text == "")
                {
                    MessageBox.Show("请先输入验证码");
                    return;
                }
                else if (input6.Text == EmailVerifyCode.ToString())
                {
                    //说明用户需要修改邮箱
                    isChangeEmail = true;
                }
            }

            //邮箱检查结束,开始修改个人信息
            //判断其它三个信息是否为空,为空则代表用户什么都没写就点击了提交按钮
            if (input3.Text == "" &&
                input5.Text == "")
            {
                MessageBox.Show("请输入需要修改的信息");
                return;
            }

            //开始判断用户需要修改的是那个信息
            if (input3.Text != "")
            {
                //修改用户名
                user.uname = input3.Text;
            }

            if (input5.Text != "")
            {
                //修改电话号码
                user.uphone = input5.Text;
            }

            if (isChangeEmail)
            {
                //修改邮箱
                user.uemail = input4.Text;
            }

            //开始更新数据
            try
            {
                //链接数据库
                using (MySqlConnection connection = new MySqlConnection(Form1.ConnectCfg))
                {
                    connection.Open();
                    string sql = "update user_info set " +
                                 "uname = @uname,uphone = @uphone,uemail = @uemail " +
                                 "where uname = @uname_tmp";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@uname", user.uname);
                    command.Parameters.AddWithValue("@uphone", user.uphone);
                    command.Parameters.AddWithValue("@uemail", user.uemail);
                    command.Parameters.AddWithValue("@uname_tmp", user_tmp.uname);
                    command.ExecuteNonQuery();
                    MessageBox.Show("修改成功");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("异常" + exception.Message);
                //修改失败,返回以前的数据
                user = user_tmp;
                throw;
            }
        }

        //个人信息界面,右下角发送验证码
        private void button13_Click(object sender, EventArgs e)
        {
            if (input9.Text != "")
            {
                MessageBox.Show("采用原密码修改时不用发送验证码");
                return;
            }

            //修改了邮箱,所以需要发送验证码
            string email = user.uemail;
            //执行发送验证码函数
            EmailVerifyCode_right = Email.SendVerificationCode(email, user.uname);
            //验证码发送成功,开始倒计时,600秒内有效
            EmailVerifyTime = 600;
            timer.Start();
        }

        //验证码发送函数
        private void timer_Tick_pwd(object sender, EventArgs e)
        {
            //验证码有效时间
            // int time = 600;
            if (EmailVerifyTime > 0)
            {
                button14.BackColor = Color.FloralWhite;
                //设置显示组件
                label48.Text = $"{EmailVerifyTime}s 内有效";
                //设置按钮可用
                button14.Enabled = true;
                //每秒减一
                EmailVerifyTime--;
            }
            else
            {
                label48.Text = "验证码已失效";
                button14.Enabled = false;
                button14.BackColor = Color.Red;
                timer.Stop();
            }
        }

        //右侧修改密码时的提交按钮
        private void button14_Click(object sender, EventArgs e)
        {
            // 创建临时数据
            User user_tmp = user;
            //首先检查原密码是否输入
            if (input9.Text == "")
            {
                //检查是否输入验证码
                if (input10.Text == "")
                {
                    MessageBox.Show("请输入验证码");
                    return;
                }
                //如果使用验证码,则检查验证码是否输入正确
                else if (input10.Text != EmailVerifyCode_right.ToString())
                {
                    MessageBox.Show("验证码错误");
                    return;
                }

                //邮箱验证码验证码成功,可以开始更改密码
                //开始检查密码和确认密码是否相同
                if (input7.Text != input8.Text)
                {
                    MessageBox.Show("密码和确认密码不一致");
                    return;
                }

                //密码一致,则设置密码
                user.upassword = input7.Text;
                //开始更新密码
                goto changePassword;
            }

            //原密码不等于空字符串,说明使用的是原密码更改,则检查原密码是否匹配
            if (input9.Text != user.upassword)
            {
                MessageBox.Show("原密码错误");
                return;
            }

            changePassword:
            //开始更新数据
            try
            {
                //链接数据库
                using (MySqlConnection connection = new MySqlConnection(Form1.ConnectCfg))
                {
                    connection.Open();
                    string sql = "update user_info set " +
                                 "uname = @uname,uphone = @uphone,uemail = @uemail " +
                                 "where uname = @uname_tmp";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@uname", user.uname);
                    command.Parameters.AddWithValue("@uphone", user.uphone);
                    command.Parameters.AddWithValue("@uemail", user.uemail);
                    command.Parameters.AddWithValue("@uname_tmp", user_tmp.uname);
                    command.ExecuteNonQuery();
                    MessageBox.Show("修改成功");
                    //修改成功,返回登录页面
                    MessageBox.Show("修改成功,请重新登录");
                    form1_.RegisterTextBox.Text = user.uname;
                    this.Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("异常" + exception.Message);
                //修改失败,返回以前的数据
                user = user_tmp;
                throw;
            }
        }

        //图书录入界面的提交按钮
        //需要将书名,作者,出版社,分类,ISBN填写完成才可以提交
        private void button15_Click(object sender, EventArgs e)
        {
            //检测图书信息书否完成
            if (input11.Text == "" ||
                input12.Text == "" ||
                input13.Text == "" ||
                input14.Text == "" ||
                input15.Text == "")
            {
                MessageBox.Show("请填写完整图书信息");
                return;
            }

            //图书检测完成,查找图书表中是否有这本书,如果有则提示已经存在,并将图书库存+1,没有则创建
            //链接数据库
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Form1.ConnectCfg))
                {
                    connection.Open();
                    string sql = "select * from book_info where ISBN = @ISBN";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ISBN", input15.Text);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        connection.Close();
                        connection.Open();
                        //图书存在
                        //更新库存
                        sql = "update book_info set Bquantity = Bquantity + 1 where ISBN = @ISBN";
                        command = new MySqlCommand(sql, connection);
                        command.Parameters.AddWithValue("@ISBN", input15.Text);
                        command.ExecuteNonQuery();
                        //如果已经存在,则提示已经存在,并将图书库存+1
                        MessageBox.Show("图书已经存在,库存+1");
                        label16.Text = "图书已经存在,库存+1";
                        return;
                    }
                    else
                    {
                        connection.Close();
                        connection.Open();
                        //如果图书不存在,则创建图书
                        sql =
                            "insert into book_info values(@ISBN,@Bname,@Bauthor,@Bpublisher,@Bcategory,@Bquantity,@Blend)";
                        command = new MySqlCommand(sql, connection);
                        command.Parameters.AddWithValue("@ISBN", input15.Text);
                        command.Parameters.AddWithValue("@Bname", input11.Text);
                        command.Parameters.AddWithValue("@Bauthor", input12.Text);
                        command.Parameters.AddWithValue("@Bpublisher", input13.Text);
                        command.Parameters.AddWithValue("@Bcategory", input14.Text);
                        command.Parameters.AddWithValue("@Bquantity", 1);
                        command.Parameters.AddWithValue("@Blend", 0);
                        command.ExecuteNonQuery();
                        MessageBox.Show("图书录入成功");
                        label16.Text = "图书录入成功";
                        //重新加载数据列表
                        books = Form4Class.GetAllBooks();
                        dataGridView5_load(books);
                        datagridView3_Load(dataGridView3, books);
                        datagridView3_Load(dataGridView4, books);
                        return;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("异常" + exception.Message);
                label16.Text = "图书录入失败";
                return;
            }
        }

        //图书修改界面
        //datagridview5,数据加载
        private void dataGridView5_load(List<Book> books)
        {
            try
            {
                //创建临时书籍列表
                List<Book> books_tmp = new List<Book>();
                foreach (Book book in books)
                {
                    books_tmp.Add(book);
                }

                //清空表
                table4.DataSource = null;
                table4.DataSource = books_tmp;
                table4.Columns = new Column[]
                {
                    new Column("ISBN", "ISBN"),
                    new Column("Bname", "书名"),
                    new Column("Bauthor", "作者"),
                    new Column("Bpublisher", "出版社"),
                    new Column("Bcategory", "分类"),
                    new Column("Bquantity", "库存"),
                    new Column("Blend", "借出")
                };
            }
            catch (Exception exception)
            {
                MessageBox.Show("异常" + exception.Message);
                return;
            }
        }

        //图书管理界面搜索框
        private void Search_TextChanged_Manager(object sender, EventArgs e)
        {
            //判断四个搜索框为空则返回原本的书籍列表
            if (textBox12.Text == "")
            {
                dataGridView5_load(books);
                return;
            }

            //获取搜索的条件
            List<string> search_list = new List<string>()
                { textBox12.Text };
            //创建要展示的书籍列表
            //复制书籍列表
            List<Book> books_tmp = new List<Book>(books);
            List<Book> books_out = new List<Book>();
            books_out.Clear();
            try
            {
                foreach (var item in books_tmp)
                {
                    if (
                        (!string.IsNullOrEmpty(search_list[0]) && item.ISBN.Contains(search_list[0]))
                        // (!string.IsNullOrEmpty(search_list[1]) && item.Bname.Contains(search_list[1])) ||
                        // (!string.IsNullOrEmpty(search_list[2]) && item.Bauthor.Contains(search_list[2])) ||
                        // (!string.IsNullOrEmpty(search_list[3]) && item.Bpublisher.Contains(search_list[3]))
                    )
                    {
                        books_out.Add(item);
                    }
                }

                //显示书籍
                dataGridView5_load(books_out);
            }
            catch (Exception exception)
            {
                return;
            }
        }

        //图书搜索界面显示从表格中点击的图书
        private void table4_CellClick(
            object sender,
            MouseEventArgs args,
            object? record,
            int rowIndex,
            int columnIndex,
            Rectangle rect)
        {
            if (table4.SelectedIndex == -1)
            {
                return;
            }

            try
            {
                //设置书名
                label58.Text = books[table4.SelectedIndex - 1].Bname;
                textBox11.Text = books[table4.SelectedIndex - 1].Bname;
                //设置作者
                label55.Text = books[table4.SelectedIndex - 1].Bauthor;
                textBox10.Text = books[table4.SelectedIndex - 1].Bauthor;
                //设置出版社
                label57.Text = books[table4.SelectedIndex - 1].Bpublisher;
                textBox1.Text = books[table4.SelectedIndex - 1].Bpublisher;
                //设置类别
                label56.Text = books[table4.SelectedIndex - 1].Bcategory;
                textBox17.Text = books[table4.SelectedIndex - 1].Bcategory;
            }
            catch (Exception exception)
            {
                return;
                // Console.WriteLine(exception);
                // throw;
            }
        }

        //图书管理界面button17,图书下架功能
        private void button17_Click(object sender, EventArgs e)
        {
            //从选中的书籍中获取isbn码
            string isbn = books[table4.SelectedIndex - 1].ISBN;
            //链接数据库,将书籍删除,如果遇到外键异常
            try
            {
                using (MySqlConnection Connection = new MySqlConnection(Form1.ConnectCfg))
                {
                    Connection.Open();
                    string sql = "DELETE FROM book_info WHERE ISBN = @ISBN";
                    using (MySqlCommand command = new MySqlCommand(sql, Connection))
                    {
                        command.Parameters.AddWithValue("@ISBN", isbn);
                        command.ExecuteNonQuery();
                        MessageBox.Show("图书下架成功");
                        label16.Text = "图书下架成功";
                        //重新加载数据列表
                        books = Form4Class.GetAllBooks();
                        dataGridView5_load(books);
                        datagridView3_Load(dataGridView3, books);
                        datagridView3_Load(dataGridView4, books);
                        return;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("异常" + exception.Message);
                return;
            }
        }

        //图书管理界面button18,图书修改功能,isbn不能修改
        private void button16_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            book.ISBN = books[table4.SelectedIndex - 1].ISBN;
            book.Bname = textBox11.Text;
            book.Bauthor = textBox10.Text;
            book.Bpublisher = textBox1.Text;
            book.Bcategory = textBox17.Text;
            //链接数据库
            try
            {
                using (MySqlConnection Connection = new MySqlConnection(Form1.ConnectCfg))
                {
                    Connection.Open();
                    string sql =
                        "UPDATE book_info SET Bname = @Bname, Bauthor = @Bauthor, Bpublisher = @Bpublisher, Bcategory = @Bcategory WHERE ISBN = @ISBN";
                    using (MySqlCommand command = new MySqlCommand(sql, Connection))
                    {
                        command.Parameters.AddWithValue("@ISBN", book.ISBN);
                        command.Parameters.AddWithValue("@Bname", book.Bname);
                        command.Parameters.AddWithValue("@Bauthor", book.Bauthor);
                        command.Parameters.AddWithValue("@Bpublisher", book.Bpublisher);
                        command.Parameters.AddWithValue("@Bcategory", book.Bcategory);
                        command.ExecuteNonQuery();
                        MessageBox.Show("图书修改成功");
                        label16.Text = "图书修改成功";
                        //重新加载数据列表
                        books = Form4Class.GetAllBooks();
                        dataGridView5_load(books);
                        datagridView3_Load(dataGridView3, books);
                        datagridView3_Load(dataGridView4, books);
                        return;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("异常" + exception.Message);
                return;
            }
        }

        //用户界面加载用户数据table3
        private void table3_Load(List<User> users)
        {
            //加载数据
            table3.DataSource = null;
            table3.DataSource = users;
            table3.Columns = new Column[]
            {
                new Column("uname", "用户名"),
                new Column("ugroup", "用户组"),
                new Column("uemail", "邮箱"),
                new Column("uphone", "手机号")
            };
        }


        //搜索时加载数据
        private void Search_TextChanged_UsersManager(object sender, EventArgs e)
        {
            //如果为空则返回所有用户列表
            if (textBox16.Text == "")
            {
                table3_Load(users);
                return;
            }

            //获取搜索的条件
            List<string> search_list = new List<string>()
                { textBox16.Text };
            //创建要展示的用户
            //复制用户
            List<User> user_tmp = new List<User>(users);
            List<User> user_out = new List<User>();
            user_out.Clear();
            try
            {
                foreach (var item in user_tmp)
                {
                    if (
                        (!string.IsNullOrEmpty(search_list[0]) && item.uname.Contains(search_list[0]))
                        // (!string.IsNullOrEmpty(search_list[1]) && item.Bname.Contains(search_list[1])) ||
                        // (!string.IsNullOrEmpty(search_list[2]) && item.Bauthor.Contains(search_list[2])) ||
                        // (!string.IsNullOrEmpty(search_list[3]) && item.Bpublisher.Contains(search_list[3]))
                    )
                    {
                        user_out.Add(item);
                    }
                }

                //显示用户
                table3_Load(user_out);
            }
            catch (Exception exception)
            {
                return;
            }
        }

        //用户表table3的数据选中函数
        private void table3_CellClick(
            object sender,
            MouseEventArgs args,
            object? record,
            int rowIndex,
            int columnIndex,
            Rectangle rect
            )
        {
            //设置用户相关信息在展示栏
            label68.Text = users[table3.SelectedIndex - 1].uname;
            label65.Text = users[table3.SelectedIndex - 1].ugroup;
            label67.Text = users[table3.SelectedIndex - 1].uemail;
            label66.Text = users[table3.SelectedIndex - 1].uphone;
            //设置修改栏
            textBox15.Text = users[table3.SelectedIndex - 1].ugroup;
            textBox14.Text = users[table3.SelectedIndex - 1].uemail;
            textBox13.Text = users[table3.SelectedIndex - 1].uphone;
        }
        //用户管理页面修改信息提交按钮button20
        private void button20_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.uname = label68.Text;
            //修改之后的信息
            user.ugroup = textBox15.Text;
            user.uemail = textBox14.Text;
            user.uphone = textBox13.Text;
            //创建数据库
            try
            {
                using (MySqlConnection Connection = new MySqlConnection(Form1.ConnectCfg))
                {
                    Connection.Open();
                    string sql =
                        "UPDATE user_info SET ugroup = @ugroup, uemail = @uemail, uphone = @uphone WHERE uname = @uname";
                    using (MySqlCommand command = new MySqlCommand(sql, Connection))
                    {
                        command.Parameters.AddWithValue("@uname", user.uname);
                        command.Parameters.AddWithValue("@ugroup", user.ugroup);
                        command.Parameters.AddWithValue("@uemail", user.uemail);
                        command.Parameters.AddWithValue("@uphone", user.uphone);
                        command.ExecuteNonQuery();
                        MessageBox.Show("用户修改成功");
                        label16.Text = "用户修改成功";
                        //重新加载数据列表
                        users = Form4Class.GetAllUsers();
                        table3_Load(users);
                        return;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("异常" + exception.Message);
                return;
            }
        }
    }
    
}