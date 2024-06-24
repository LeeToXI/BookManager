using System.Net;
using System.Net.Mail;
using System.Speech.Synthesis;
using AntdUI;
using MySql.Data.MySqlClient;
namespace _012;

public class Form1Class
{
    //用户名和密码框重置按钮
    public static void ReSet(Input label1, Input label2)
    {
        label1.Text = "";
        label2.Text = "";
    }
    
    //获取数据库中的用户名
    public static List<string> GetUserAccount()
    {
        //创建列表保存数据
        List<string> userAccount = new List<string>();
        try
        {
            using (MySqlConnection connect = new MySqlConnection(Form1.ConnectCfg)){
                connect.Open();
                MySqlCommand command = new MySqlCommand("select uname from user_info", connect);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    userAccount.Add(reader.GetString("uname"));
                }
            }
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
            MessageBox.Show("服务器连接异常");
        }
        return userAccount;
    }
    
    //语音播报
    public static void Speak(string text)
    {
        //创建语音对象
        SpeechSynthesizer synth = new SpeechSynthesizer();
        //设置语音播报速度
        synth.Rate = 0;
        //设置语音播报音量
        synth.Volume = 100;
        //设置语音播报语言
        synth.SetOutputToDefaultAudioDevice();
        //语音播报
        synth.Speak(text);
    }
    
    
}

