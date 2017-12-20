
/*
 * guid: $GUID$
 * file: ICache
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/20 15:31:48
 * desc: 一组数据缓存处理功能约定
 ************************************
 *
 * upgrade history:
 ************************************
 * author: 
 * update: 
 * ver-desc:
 * 
 */

namespace org.aoas.app.repository
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 旨在定义一组数据缓存处理功能约定
    /// </summary>
    /// <typeparam name="TKey">缓存数据 key 值</typeparam>
    /// <typeparam name="TValue">缓存数据 value 值</typeparam>
    public interface ICache<TKey, TValue>
    {
        /// <summary>
        /// 增加指定 key 的 value 值到缓存对象中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Add(TValue value);

        /// <summary>
        /// 设置指定 key 的数据对象为新的值，若指定的 key 不存在，那么增加当前 key，并设置 key 的数据对象为 value
        /// </summary>
        /// <param name="key">指定数据对象的 key 值</param>
        /// <param name="value">指定 key 被设置的数据对象</param>
        void Set(TKey key, TValue value);

        /// <summary>
        /// 移除一组指定 key 的数据对象，并返回一个被移除的数据对象集合
        /// </summary>
        /// <param name="keys">一组指定可移除数据对象的 key 值</param>
        /// <returns></returns>
        IEnumerable<TValue> Remove(params TKey[] keys);

        /// <summary>
        /// 移除满足指定谓词条件的所有数据对象，并返回一个被移除的数据对象集合
        /// </summary>
        /// <param name="predicate">指定的谓词条件</param>
        /// <returns></returns>
        IEnumerable<TValue> Remove(Func<TValue, bool> predicate);

        /// <summary>
        /// 获取满足指定谓词条件的所有数据对象集合，若不存在，返回 null
        /// </summary>
        /// <param name="predicate">指定的谓词条件</param>
        /// <returns></returns>
        IEnumerable<TValue> Get(Func<TValue, bool> predicate);

        /// <summary>
        /// 清楚缓存
        /// </summary>
        void Clear();
    }
}
