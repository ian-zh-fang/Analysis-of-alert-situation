
/*
 * guid: $GUID$
 * file: IEqualityDependancy
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:12:00
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

    /// <summary>
    /// 旨在支持相等和比较处理的依赖约定
    /// </summary>
    /// <typeparam name="TSource">支持相等和比较处理的对象类型，当前类型派生自 ry.common.IEqualityDependancy&lt;TSource> .</typeparam>
    public interface IEqualityDependancy<TSource>
        : IEquatable<TSource>
        , IComparable<TSource>
        where TSource : IEqualityDependancy<TSource>
    { }
}
