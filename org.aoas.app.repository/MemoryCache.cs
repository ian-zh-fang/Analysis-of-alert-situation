
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

    /// <summary>
    /// 旨在实现一种使用内存缓存数据的基本业务处理
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
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
            value.ThrowIfNull(nameof(value));
            Add(value);
        }

        protected virtual void Add(TValue value)
        {
            _cacheList.Add(value);
        }

        void ICache<TKey, TValue>.Clear()
        {
            Clear();
        }

        protected virtual void Clear()
        {
            _cacheList = new ConcurrentBag<TValue>();
        }

        IEnumerable<TValue> ICache<TKey, TValue>.Get(Func<TValue, bool> predicate)
        {
            predicate.ThrowIfNull(nameof(predicate));
            return Get(predicate);
        }

        protected virtual IEnumerable<TValue> Get(Func<TValue, Boolean> predicate)
        {
            predicate.ThrowIfNull(nameof(predicate));
            return _cacheList.Where(predicate);
        }

        IEnumerable<TValue> ICache<TKey, TValue>.Remove(params TKey[] keys)
        {
            var keyArr = keys.Where(k => !k.IsNull());
            if (0 == keyArr.Count()) { return new TValue[0]; }

            return Remove(keyArr);
        }

        protected virtual IEnumerable<TValue> Remove(IEnumerable<TKey> keys)
        {
            var arr = _cacheList.ToArray();
            var rstArr = new List<TValue>();
            var newArr = new List<TValue>();
            foreach (var value in arr)
            {
                if (keys.Any(k => k.Equals(GetKey(value))))
                {
                    rstArr.Add(value);
                    continue;
                }

                newArr.Add(value);
            }

            _cacheList = new ConcurrentBag<TValue>(newArr);
            return rstArr;
        }

        IEnumerable<TValue> ICache<TKey, TValue>.Remove(Func<TValue, bool> predicate)
        {
            predicate.ThrowIfNull(nameof(predicate));
            return Remove(predicate);            
        }

        protected virtual IEnumerable<TValue> Remove(Func<TValue, Boolean> predicate)
        {
            var arr = _cacheList.ToArray();
            var rstArr = new List<TValue>();
            var newArr = new List<TValue>();
            foreach (var value in arr)
            {
                if (predicate.Invoke(value))
                {
                    rstArr.Add(value);
                    continue;
                }

                newArr.Add(value);
            }

            _cacheList = new ConcurrentBag<TValue>(newArr);
            return rstArr;
        }

        void ICache<TKey, TValue>.Set(TKey key, TValue value)
        {
            Set(key, value);
        }

        protected virtual void Set(TKey key, TValue value)
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
