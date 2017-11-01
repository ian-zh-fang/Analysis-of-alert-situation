
/*
 * guid: $GUID$
 * file: DateTimeEx
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/1 13:41:05
 * desc: 
 ************************************
 *
 * upgrade history:
 ************************************
 * author: 
 * update: 
 * ver-desc:
 * 
 */

namespace org.aoas
{
    using System;
    using System.Text.RegularExpressions;

    public static class DateTimeEx
    {
        // yyyyMMddHHmmss 时间格式的正则表达式识别规则
        private const string DATEFORMATPATTERN = @"^(?<y>\d{4})(?<mm>\d{2})(?<d>\d{2})(?<h>\d{2})(?<m>\d{2})(?<s>\d{2})$";
        // yyyyMMddHHmmss 时间格式的正则表达式验证器
        private static readonly Regex FmtDatetimeStrRegex;

        static DateTimeEx()
        {
            FmtDatetimeStrRegex = new Regex(DATEFORMATPATTERN);
        }

        /// <summary>
        /// 将字符串为 yyyyMMddHHmmss 格式的时间转换为 System.DateTime 对象 . 失败将返回 System.DateTime.MinValue
        /// </summary>
        public static DateTime FmtStringDateTime(this String input)
        {
            if (input.IsEmpty())
            {
                return DateTime.MinValue;
            }
            var match = FmtDatetimeStrRegex.Match(input);
            if (!match.Success)
            {
                return DateTime.MinValue;
            }

            var y = int.Parse(match.Groups["y"].Value);
            var mm = int.Parse(match.Groups["mm"].Value);
            var d = int.Parse(match.Groups["d"].Value);
            var h = int.Parse(match.Groups["h"].Value);
            var m = int.Parse(match.Groups["m"].Value);
            var s = int.Parse(match.Groups["s"].Value);

            return new DateTime(y, mm, d, h, m, s);
        }

        /// <summary>
        /// 获取本周第一天的日期
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="seek">每周开始时间节点</param>
        /// <returns></returns>
        public static DateTime FirstDayCurrentweeek(this DateTime datetime, DayOfWeek seek = DayOfWeek.Monday)
        {
            if (seek == datetime.DayOfWeek)
            {
                return datetime.Date;
            }

            var offset = (int)datetime.DayOfWeek;
            if (offset == 0)
            {
                offset = 7;
            }
            return datetime.AddDays((int)seek - offset).Date;
        }

        /// <summary>
        /// 获取当月开始日期
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime FirstDayCurrentMonth(this DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, 1).Date;
        }

        /// <summary>
        /// 获取上一个月的开始日期
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime FirstDayPreviousMonth(this DateTime datetime)
        {
            return datetime.FirstDayCurrentMonth().AddMonths(-1);
        }

        /// <summary>
        /// 转换成指定时区的本地时间，默认本地时区
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static DateTime ToLocalTime(this DateTime datetime, TimeZone timeZone = null)
        {
            timeZone = timeZone ?? TimeZone.CurrentTimeZone;
            return timeZone.ToLocalTime(datetime);
        }

        /// <summary>
        /// 转换成时间戳，精确到微秒
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static long ToUnixTime(this DateTime datetime)
        {
            return (datetime.ToUniversalTime() - UnixTimeSeed().ToUniversalTime()).Ticks;
        }

        /// <summary>
        /// 转换成时间戳，精确到毫秒
        /// </summary>
        public static long ToUnixTimeMillisecond(this DateTime datetime)
        {
            return datetime.ToUnixTime() / TimeSpan.TicksPerMillisecond;
        }

        /// <summary>
        /// 转换成时间戳，精确到秒
        /// </summary>
        public static long ToUnixTimeSecond(this DateTime datetime)
        {
            return datetime.ToUnixTime() / TimeSpan.TicksPerSecond;
        }

        /// <summary>
        /// 秒级时间戳转时间
        /// </summary>
        public static DateTime ToDateTimeSecond(this long timestamp)
        {
            return UnixTimeSeed().AddTicks(timestamp * TimeSpan.TicksPerSecond);
        }

        /// <summary>
        /// 毫秒级时间戳转时间
        /// </summary>
        public static DateTime ToDateTimeMilisecond(this long timestamp)
        {
            return UnixTimeSeed().AddTicks(timestamp * TimeSpan.TicksPerMillisecond);
        }

        /// <summary>
        /// 微秒级时间戳转时间
        /// </summary>
        public static DateTime ToDateTime(this long ticks)
        {
            return UnixTimeSeed().AddTicks(ticks);
        }

        // 创建本地时区格林威治时间 1970 年 1 月 1 日 0 时 0 分 0 秒 时刻的时间
        public static DateTime UnixTimeSeed()
        {
            var stdt = new DateTime(1970, 1, 1, 0, 0, 0);
            return TimeZone.CurrentTimeZone.ToLocalTime(stdt);
        }
    }
}
