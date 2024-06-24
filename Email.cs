using System.Net;
using System.Net.Mail;

namespace _012;

public class Email
{
    //注册成功发送验证码,
    public static void WellcomeEmail(string email,string uname = "user")
    {
        try
        {
            MailMessage mail = new MailMessage();
            //发件人地址
            mail.From = new MailAddress(UserInterface.emailAccount);
            //收件人地址
            mail.To.Add(email);
            mail.Subject = "欢迎注册-嶳鬱™科技 ©";
            mail.Body = $"尊敬的用户{uname}您好！\n\n感谢您注册我们的网站，我们希望您能 enjoy our website。";
            mail.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.qq.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(UserInterface.emailAccount,UserInterface.emailPassword);
            smtp.Send(mail);
            // return true;
        }catch (Exception e)
        {
            Console.WriteLine(e.Message);
            // return false;
        }
    }
    //发送邮箱验证码函数
    public static int SendVerificationCode(string email,string uname = "user")
    {
        //生成验证码
        Random random = new Random();
        int code = random.Next(100000, 999999);
        //发送邮件
        MailMessage mail = new MailMessage();
        //发件人地址
        mail.From = new MailAddress(UserInterface.emailAccount);
        //收件人地址
        mail.To.Add(email);
        mail.Subject = "验证码-嶳鬱™科技 ©";
        mail.Body = $"尊敬的用户您好！\n\n您的验证码是:{code}，请在 5 分钟内进行验证。如果该验证码不为您本人申请，请无视。";
        mail.IsBodyHtml = false;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.qq.com";
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(UserInterface.emailAccount,UserInterface.emailPassword);
        smtp.Send(mail);
        MessageBox.Show("验证码已发送,请前往邮箱查看");
        //返回验证码
        return code;

    }
    
}