
/*
 * guid: $GUID$
 * file: MemoryCache
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/20 15:55:46
 * desc: 一组关于内存缓存的基本实现
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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class MemoryCache<TKey, TValue> : ICache<TKey, TValue>
    {
        /// <summary>
        /// 内存缓存数据列表
        /// </summary>
        private ConcurrentBag<TValue> _cacheList;

        /// <summary>
        /// 创建 <see cref="MemoryCache{TKey, TValue}"/> 类型的新实例
        /// </summary>
        protected MemoryCache()
        {
            _cacheList = new ConcurrentBag<TValue>();
        }

        /// <summary>
        /// 使用一组指定数据对象集合创建 <see cref="MemoryCache{TKey, TValue}"/> 类型的新实例
        /// </summary>
        /// <param name="collection">一组指定的数据对象集合</param>
        protected MemoryCache(IEnumerable<TValue> collection)
        {
            _cacheList = new ConcurrentBag<TValue>(collection);
        }


        void ICache<TKey, TValue>.Add(TValue value)
        {
            _cacheList.Add(value);
        }

        void ICache<TKey, TValue>.Clear()
        {
            _cacheList = new ConcurrentBag<TValue>();
        }

        IEnumerable<TValue> ICache<TKey, TValue>.Get(Func<TValue, bool> predicate)
        {
            predicate.ThrowIfNull(nameof(predicate));
            return
                _cacheList.Where(predicate);
        }

        IEnumerable<TValue> ICache<TKey, TValue>.Remove(params TKey[] keys)
        {
            throw new NotImplementedException();
        }

        IEnumerable<TValue> ICache<TKey, TValue>.Remove(Func<TValue, bool> predicate)
        {
            throw new NotImplementedException();
        }

        void ICache<TKey, TValue>.Set(TKey key, TValue value)
        {
            var v = ((ICache<TKey, TValue>)this).Get(t => key.Equals(GetKey(t))).FirstOrDefault();
            if (!v.IsNull())
            {
                // 删除已存在的数据对象
                ((ICache<TKey, TValue>)this).Remove(key);
            }

            // 增加新的数据对象
            ((ICache<TKey, TValue>)this).Add(value);
        }

        /// <summary>
        /// 获取指定数据对象的 key 值
        /// </summary>
        /// <param name="value">指定数据对象</param>
        /// <returns></returns>
        protected abstract TKey GetKey(TValue value);
    }
}
