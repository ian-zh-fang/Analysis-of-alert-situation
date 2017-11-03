
/*
 * guid: $GUID$
 * file: LogContext
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:21:03
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

namespace org.aoas.log
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 日志上下文，ILogContext 的一种实现
    /// </summary>
    public class LogContext
        : SerializationDependancy
        , ILogContext
    {
        /// <summary>
        /// 创建一个 LogContext 的新实例，并返回创建的对象
        /// </summary>
        /// <param name="message">需要处理的日志内容上下文</param>
        /// <param name="owner">生产日志内容的对象</param>
        /// <param name="error">需要处理的错误信息</param>
        /// <param name="id">当前日志上下文标识码</param>
        /// <exception cref="ArgumentNullException"></exception>
        public LogContext(String message, Object owner, Exception error = null, String id = null)
            : base()
        {
            message.ThrowIfWhitespace("message");
            owner.ThrowIfNull();

            Id = EnsureId(id);
            Owner = owner;
            Message = message;
            Error = error;
            Timestamp = DateTime.Now.ToUnixTimeMillisecond();
        }

        /// <summary>
        /// 提供反序列话机制的构造函数
        /// </summary>
        /// <param name="info">序列内容上下文</param>
        /// <param name="context">序列化内容来源</param>
        protected LogContext(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        // 如果 id 不存在，或者 长度为 0， 或者全部为空字符，创建一个 GUID 的新实例，并赋值给 id
        // 最后，返回 id
        private String EnsureId(String id)
        {
            if (id.IsWhitespaces())
            {
                id = Guid.NewGuid().ToString("N");
            }

            return id;
        }

        /// <summary>
        /// 当前上下文标识码
        /// </summary>
        public String Id { get; private set; }

        /// <summary>
        /// 当前上下文日志内容
        /// </summary>
        public String Message { get; private set; }

        /// <summary>
        /// 当前上下文错误内容
        /// </summary>
        public Exception Error { get; private set; }

        /// <summary>
        /// 当前上下文错误内容生产者
        /// </summary>
        public Object Owner { get; private set; }

        /// <summary>
        /// 当前上下文创建时间戳（毫秒）
        /// </summary>
        public Int64 Timestamp { get; private set; }
    }
}
