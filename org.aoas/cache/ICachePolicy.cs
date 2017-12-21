
/*
 * guid: $GUID$
 * file: ICachePolicy
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/21 15:15:54
 * desc: 一组缓存策略功能约定
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
    /// 旨在声明一组缓存策略功能约定
    /// </summary>
    public interface ICachePolicy : IDisposable
    {
        /// <summary>
        /// 缓存失效时，触发当前事件
        /// </summary>
        event Action OnInvalid;

        /// <summary>
        /// 缓存上下文
        /// </summary>
        ICacheContext Context { get; set; }

        /// <summary>
        /// 校验缓存是否失效，若缓存失效，返回 true；否则，返回 false .
        /// </summary>
        Boolean Check();
    }
}
