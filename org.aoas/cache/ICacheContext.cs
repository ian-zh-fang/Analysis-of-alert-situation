
/*
 * guid: $GUID$
 * file: ICachePolicyContext
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/21 15:39:10
 * desc: 一组缓存上下文功能约定
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
    /// 旨在声明一组缓存上下文功能约定
    /// </summary>
    public interface ICacheContext : IDisposable
    {
        /// <summary>
        /// 缓存数据发生改变时，触发当前事件
        /// </summary>
        event Action OnChange;
    }
}
