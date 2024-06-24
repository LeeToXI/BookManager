using MySql.Data.MySqlClient;

namespace _012;

//创建用户对象
public class User
{
    // uname,ugroup,uemail,uphone
    public string uname { get; set; }
    public string ugroup { get; set; }
    public string uemail { get; set; }
    public string uphone { get; set; }
    public string upassword { get; set; }
    public void GetUserInfo()
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(Form1.ConnectCfg))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("select uname,ugroup,uemail,uphone,upwd from user_info where uname=@uname", conn))
                {
                    cmd.Parameters.AddWithValue("@uname", this.uname);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.uname = reader.GetString("uname");
                            this.ugroup = reader.GetString("ugroup");
                            this.uemail = reader.GetString("uemail");
                            this.uphone = reader.GetString("uphone");
                            this.upassword = reader.GetString("upwd");
                        }
                    }
                }
            }

        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            // Console.WriteLine(e);
            throw;
        }
    }
}

//创建工单对象
public class WorkOrder
{
    public int wid { get; set; }
    public string uname { get; set; }
    public string wdate_submit { get; set; }
    public string wtype { get; set; }
    public string ISBN { get; set; }
    public string wstatus { get; set; }
    public string wpublisher { get; set; }
    public string wdate_process { get; set; }
}



//创建公告对象
public class Notice
{
    // public int Nid { get; set; }
    public string Ndate { get; set; }
    public string Ncontent { get; set; }
    public string Npublisher { get; set; }
}

//创建书籍对象
public class Book
{
    public string ISBN { get; set; }
    public string Bname { get; set; }
    public string Bauthor { get; set; }
    public string Bpublisher { get; set; }
    public string Bcategory { get; set; }
    public int Bquantity { get; set; }
    public int Blend { get; set; }
    
    // 辅助方法，根据索引获取Book对象的属性值
    public string GetPropertyByIndex(Book book, int index)
    {
        switch (index)
        {
            case 0: return book.ISBN;
            case 1: return book.Bname;
            case 2: return book.Bauthor;
            case 3: return book.Bpublisher;
            default: return string.Empty;
        }
    }
}

public class Form4Class
{
    //获取公告列表的数据
    public static List<Notice> GetAllNotices()
    {
        //创建公告列表
        List<Notice> notices = new List<Notice>();
        try
        {
            //连接数据库
            using (MySqlConnection connection = new MySqlConnection(Form1.ConnectCfg))
            {
                connection.Open();
                //公告按照时间降序排序
                MySqlCommand command = new MySqlCommand("select * from notice order by ndate desc", connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //把每条数据都加到notices列表中
                    Notice notice = new Notice();
                    // notice.Nid = reader.GetInt32(0);//id不需要显示
                    notice.Ndate = reader.GetDateTime(1).ToString("yyyy-MM-dd");
                    notice.Ncontent = reader.GetString(2);
                    notice.Npublisher = reader.GetString(3);
                    notices.Add(notice);
                }
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
        return notices;
    }
    
    //从数据库获取书籍列表
    public static List<Book> GetAllBooks()
    {
        List<Book> books = new List<Book>();
        //创建数据库链接
        try
        {
            using (MySqlConnection conntection = new MySqlConnection(Form1.ConnectCfg))
            {
                conntection.Open();
                MySqlCommand command = new MySqlCommand("select * from book_info", conntection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.ISBN = reader[0].ToString();
                    book.Bname = reader.GetString(1);
                    book.Bauthor = reader.GetString(2);
                    book.Bpublisher = reader.GetString(3);
                    book.Bcategory = reader.GetString(4);
                    book.Bquantity = reader.GetInt32(5);
                    book.Blend = reader.GetInt32(6);
                    books.Add(book);
                }
            }
        }catch(Exception e){
            MessageBox.Show(e.Message);
        }
        return books;
    }
    
    //获取所有用户的信息
    public static List<User> GetAllUsers()
    {
        List<User> users = new List<User>();
        try
        {
            using (MySqlConnection connection = new MySqlConnection(Form1.ConnectCfg))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("select uname,ugroup,uemail,uphone from user_info", connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User();
                    user.uname = reader.GetString(0);
                    user.ugroup = reader.GetString(1);
                    user.uemail = reader.GetString(2);
                    user.uphone = reader.GetString(3);
                    user.upassword = "";
                    users.Add(user);
                }
            }
            return users;
        }catch(Exception e){
            MessageBox.Show(e.Message);
            return users;
        }
    }
}