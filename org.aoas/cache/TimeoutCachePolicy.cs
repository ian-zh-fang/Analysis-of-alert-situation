
/*
 * guid: $GUID$
 * file: TimeoutCachePolicy
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 11:17:50
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

namespace org.aoas.cache
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class TimeoutCachePolicy : CachePolicy
    {
        /// <summary>
        /// 上下文内容同步锁，用于多线程安全处理
        /// </summary>
        private static readonly object _SyncLocker = new object();

        /// <summary>
        /// 缓存有效时间长度，-1 时标识永不过期
        /// </summary>
        private readonly long _interval;

        /// <summary>
        /// 缓存下一次失效时间戳，单位：毫秒
        /// </summary>
        private long _nextInvokeTimestamp;

        /// <summary>
        /// 创建 <see cref="TimeoutCachePolicy"/> 类型的新实例
        /// </summary>
        /// <param name="milliseconds">缓存有效毫秒数，默认为 1 h .</param>
        public TimeoutCachePolicy(long milliseconds = 3600000L)
            : base()
        {
            _nextInvokeTimestamp = DateTime.Now.ToUnixTimeMillisecond();
            _interval = milliseconds;
        }

        /// <summary>
        /// 创建 <see cref="TimeoutCachePolicy"/> 类型的新实例
        /// </summary>
        /// <param name="timespan">缓存有效时间长度</param>
        public TimeoutCachePolicy(TimeSpan timespan) : this((long)timespan.TotalMilliseconds) { }

        protected override bool OnCheck()
        {
            // 永不过期处理
            if (-1L == _interval) { return false; }

            // 缓存过期失效处理
            var now = DateTime.Now.ToUnixTimeMillisecond();
            if (now >= _nextInvokeTimestamp)
            {
                lock (_SyncLocker)
                {
                    if (now >= _nextInvokeTimestamp)
                    {
                        _nextInvokeTimestamp = now + _interval;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
