using System.Drawing.Imaging;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace _012;

public class Form2Class
{
    
    //注册时密码检查函数
    public static bool checkPasswd(TextBox label,Label outlabel)
    {
        // ErrorProvider errorProvider = new ErrorProvider();
        string passwd = label.Text;
        // 密码长度不能小于8
        if (passwd.Length < 8)
        {
            outlabel.Text = "长度不能小于8";
            return false;
        };
        char[] checkV = { '@', '!', '.', '$', '%' };
        //密码需要包含特殊符号
        if (!checkV.Any(item => passwd.Contains(item))) {
            // MessageBox.Show(label, "密码需要包含@!.$%其中的字符");
            outlabel.Text = "需要包含@!.$%";
            return false;
        };
        //密码需要包含数字
        checkV = new char[]{'1','2','3','4','5','6','7','8','9','0'};
        if (!checkV.Any(item => passwd.Contains(item)))
        {
            // MessageBox.Show(label, "密码需要包含数字");
            outlabel.Text = "需要包含数字";
            return false;
        };
        //test
        // label.Text = "@1aA";
        //密码需要包含大小写字母
        bool haveUpper = false;
        bool haveLower = false;
        foreach (char c in passwd)
        {
            //判断是否是大写字母
            if (c >= 'A' && c <= 'Z')
            {
                // MessageBox.Show("有大写字母");
                haveUpper = true;
                continue;
            }
            //判断是否是小写字母
            if (c >= 'a' && c <= 'z')
            {
                // MessageBox.Show("有小写字母");
                haveLower = true;
            }
        };
        //判断是否包含大小写字母
        //如果不是同时包含大小写字母,则提示
        if (!haveUpper || !haveLower)
        {
            // MessageBox.Show(label, "密码需要包含至少一个大写字母和一个小写字母");
            outlabel.Text = "要有大小写字母";
            return false;
        }
        return true;
    }
    
    //邮箱检查函数
    public static bool checkEmail(TextBox textBox)
    {
        string email = textBox.Text;
        //正则表达式
        string pattern = @"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$";
        //匹配
        Match match = Regex.Match(email, pattern);
        //判断是否匹配
        if (match.Success)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    //电话号码检查 
    public static bool checkPhone(TextBox textBox)
    {
        string phone = textBox.Text;
        //正则表达式
        string pattern = @"^1[3-9]\d{9}$";
        //匹配
        Match match = Regex.Match(phone, pattern);
        //判断是否匹配
        if (match.Success)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    //从图片组件中获取头像数据getImgData
    public static byte[] getImgData(PictureBox pictureBox)
    {
        //获取图片
        Bitmap bitmap = (Bitmap)pictureBox.Image;
        //将图片转换为字节数组
        //创建内存流
        MemoryStream memoryStream = new MemoryStream();
        //将图片保存到内存流中,并保存为png格式但是经过测试,图片依然是jpg格式,原因未知
        bitmap.Save(memoryStream, ImageFormat.Png);
        //将内存流转换为字节数组
        byte[] imgData = memoryStream.ToArray();
        //释放资源
        memoryStream.Close();
        return imgData;
    }
}