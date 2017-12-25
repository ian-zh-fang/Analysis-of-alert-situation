
/*
 * guid: $GUID$
 * file: ICache
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/21 14:43:53
 * desc: 一组缓存处理的约定
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
    using System.Collections.Generic;

    /// <summary>
    /// 旨在声明一组缓存处理的约定
    /// </summary>
    /// <typeparam name="TKey">缓存数据对象 key 类型</typeparam>
    /// <typeparam name="TValue">还出数据对象 value 类型</typeparam>
    public interface ICache<TKey, TValue> : IEnumerable<TValue>, IDisposable
    {
        /// <summary>
        /// 增加指定的数据对象到缓存中
        /// </summary>
        /// <param name="value">指定增加的数据对象</param>
        void Add(TValue value);

        /// <summary>
        /// 设置缓存中指定 key 的数据对象为指定的 value 值 .
        /// 若缓冲中不存在指定的 key，那么增加指定的 value 数据对象到缓存中 .
        /// </summary>
        /// <param name="key">指定的 key .</param>
        /// <param name="value">指定的 value .</param>
        void Set(TKey key, TValue value);

        /// <summary>
        /// 移除缓存中匹配一组指定 key 的所有数据对象，并返回被移除的数据对象
        /// </summary>
        /// <param name="keys">一组指定可移除数据对象的 key 值</param>
        /// <returns></returns>
        IEnumerable<TValue> Remove(params TKey[] keys);

        /// <summary>
        /// 移除满足指定谓词条件的所有数据对象，并返回一个被移除的数据对象集合
        /// </summary>
        /// <param name="predicate">筛选数据对象的谓词条件</param>
        /// <returns></returns>
        IEnumerable<TValue> Remove(Func<TValue, Boolean> predicate);

        /// <summary>
        /// 获取满足指定谓词条件的所有数据对象集合，若不存在，返回一组长度为 0 的数据集合
        /// </summary>
        /// <param name="predicate">筛选数据对象的谓词条件</param>
        /// <returns></returns>
        IEnumerable<TValue> Get(Func<TValue, Boolean> predicate);
    }
}
