namespace _012;

public class Throttle
{
    //创建一个节流器,用于降低用户访问数据库的频率,从而提高性能
    //上次执行函数的时间
    private DateTime LastExecuteTime { get; set; }
    private TimeSpan timeSpan { get; set; }
    //节流器开关
    public static bool switch_bool = false;

    //节流器函数
    public void ThrottledExecute(TimeSpan timeSpan,Action action)
    {
        var now = DateTime.Now;
        //如果当前时间距离上次执行函数的时间大于设置的时间间隔,则执行函数
        if (now - LastExecuteTime > timeSpan && switch_bool)
        {
            action();
            //更新上一次执行函数的时间
            LastExecuteTime = now;
        }
    }
}