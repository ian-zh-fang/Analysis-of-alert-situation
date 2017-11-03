
/*
 * guid: $GUID$
 * file: ExceptionHandleAttribute
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:16:47
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

namespace org.aoas.attributes
{
    using System;

    /// <summary>
    /// 异常处理特性，旨在实现支持异常处理的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public abstract class ExceptionHandleAttribute : Attribute
    {
        // 一种保护机制的构造函数
        protected ExceptionHandleAttribute()
            : base()
        { }

        /// <summary>
        /// 处理指定的错误，处理完成，返回 true；否则，返回 false
        /// </summary>
        /// <param name="error">需要处理的错误信息</param>
        /// <param name="owner">生产错误的对象</param>
        public bool Invoke(Exception error, Object owner)
        {
            if (error.IsNull()) { return true; }

            try
            {
                // 调用核心处理
                return OnInvoke(error, owner);
            }
            catch (Exception err)
            {
                OnInnerExceptionInvoke(err);
                return false;
            }
        }

        // 内部异常处理
        protected virtual void OnInnerExceptionInvoke(Exception error)
        {
            throw error;
        }

        // 错误处理核心，交由具体的派生类对象执行
        //  执行成功，返回 true；否则，返回 false
        protected abstract bool OnInvoke(Exception error, object owner);
    }
}
