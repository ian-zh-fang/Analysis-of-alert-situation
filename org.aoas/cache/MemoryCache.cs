
/*
 * guid: $GUID$
 * file: MemoryCache
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/21 17:58:30
 * desc: 一组内存缓存数据对象的基础功能实现
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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 旨在实现一组内存缓存数据对象的基础功能
    /// </summary>
    /// <typeparam name="TKey">缓存数据对象 key 类型</typeparam>
    /// <typeparam name="TValue">缓存数据对象 value 类型</typeparam>
    public abstract class MemoryCache<TKey, TValue> : Cache<TKey, TValue>
    {
        /// <summary>
        /// 数据对象缓存队列
        /// </summary>
        private ConcurrentBag<TValue> _cacheList;

        /// <summary>
        /// 创建 <see cref="MemoryCache{TKey, TValue}"/> 类型的新实例
        /// </summary>
        /// <param name="context">缓存上下文 <see cref="ICacheContext"/> 实例 .</param>
        protected MemoryCache(ICacheContext context)
            :base(context)
        {
            _cacheList = new ConcurrentBag<TValue>();
        }

        /// <summary>
        /// 创建 <see cref="MemoryCache{TKey, TValue}"/> 类型的新实例
        /// </summary>
        /// <param name="items">初始化时缓存数据对象</param>
        /// <param name="context">缓存上下文 <see cref="ICacheContext"/> 实例 .</param>
        /// <exception cref="ArgumentNullException">items 是 null .</exception>
        protected MemoryCache(IEnumerable<TValue> items, ICacheContext context)
            :base(context)
        {
            items.ThrowIfNull(nameof(items));
            _cacheList = new ConcurrentBag<TValue>(items);
        }

        protected override void OnCacheInvalidCallback()
        {
            var arr = Load();
            _cacheList = new ConcurrentBag<TValue>(arr);
        }

        protected override void OnAdd(TValue value)
        {
            _cacheList.Add(value);
        }

        protected override IEnumerable<TValue> OnRemove(Func<TValue, bool> predicate)
        {
            var arr = _cacheList.ToArray();
            var newArr = new List<TValue>();
            var rmArr = new List<TValue>();
            foreach (var value in arr)
            {
                if (predicate.Invoke(value))
                {
                    rmArr.Add(value);
                    continue;
                }

                newArr.Add(value);
            }

            _cacheList = new ConcurrentBag<TValue>(newArr);
            return rmArr;
        }

        protected override IEnumerable<TValue> OnGet(Func<TValue, bool> predicate)
        {
            return _cacheList.Where(predicate);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _cacheList = null;
        }

        protected override IEnumerator<TValue> GetEnumerator()
        {
            return _cacheList.GetEnumerator();
        }

        /// <summary>
        /// 加载数据对象，并返回一组加载的数据对象集合
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<TValue> Load();
    }
}
