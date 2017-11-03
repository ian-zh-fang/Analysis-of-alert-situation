
/*
 * guid: $GUID$
 * file: ILogContext
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:20:16
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
    /// 旨在约定日志内容上下文基础结构
    /// </summary>
    public interface ILogContext : ISerializable
    {
        /// <summary>
        /// 当前上下文 GUID
        /// </summary>
        String Id { get; }

        /// <summary>
        /// 当前上下文日志内容
        /// </summary>
        String Message { get; }

        /// <summary>
        /// 当前上下文错误内容
        /// </summary>
        Exception Error { get; }

        /// <summary>
        /// 当前上下文错误内容生产者
        /// </summary>
        Object Owner { get; }

        /// <summary>
        /// 当前上下文创建时间戳（毫秒）
        /// </summary>
        Int64 Timestamp { get; }
    }
}
