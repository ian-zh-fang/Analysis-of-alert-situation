
/*
 * guid: $GUID$
 * file: ILogger
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:21:49
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
    /// <summary>
    /// 旨在支持日志处理的一组功能约定
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 一般性的日志处理
        /// </summary>
        /// <param name="context">需要处理的日志上下问对象</param>
        void Normal(ILogContext context);

        /// <summary>
        /// 警告性的日志处理
        /// </summary>
        /// <param name="context">需要处理的日志上下问对象</param>
        void Warn(ILogContext context);

        /// <summary>
        /// 一般性错误的日志处理
        /// </summary>
        /// <param name="context">需要处理的日志上下问对象</param>
        void Error(ILogContext context);

        /// <summary>
        /// 严重错误的日志处理
        /// </summary>
        /// <param name="context">需要处理的日志上下问对象</param>
        void HighError(ILogContext context);
    }
}
