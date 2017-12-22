
/*
 * guid: $GUID$
 * file: Cache
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/21 16:55:09
 * desc: 一组缓存的基础功能实现
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
    using System.Linq;

    /// <summary>
    /// 旨在实现一组缓存基础业务
    /// </summary>
    /// <typeparam name="TKey">缓存数据对象 key 类型</typeparam>
    /// <typeparam name="TValue">缓存数据对象 value 类型</typeparam>
    public abstract class Cache<TKey, TValue> : DisposeDependancy, ICache<TKey, TValue>, IDisposable
    {
        /// <summary>
        /// 缓存策略对象实例
        /// </summary>
        private readonly ICachePolicy _policy;

        /// <summary>
        /// 创建 <see cref="Cache{TKey, TValue}"/> 类型的新实例
        /// </summary>
        /// <param name="context">缓存上下文 <see cref="ICacheContext"/> 类型实例</param>
        protected Cache(ICacheContext context)
        {
            context.ThrowIfNull(nameof(context));
            _policy = InitPolicy(context);
        }

        /// <summary>
        /// 初始化缓存策略，并返回缓存策略对象实例
        /// </summary>
        /// <param name="context">缓存上下文实例</param>
        private ICachePolicy InitPolicy(ICacheContext context)
        {
            var policy = GetPolicy();
            policy.Context = context;
            policy.OnInvalid += OnCacheInvalidCallback;

            return policy;
        }
        
        void ICache<TKey, TValue>.Add(TValue value)
        {
            value.ThrowIfNull(nameof(value));
            OnAdd(value);
        }

        IEnumerable<TValue> ICache<TKey, TValue>.Get(Func<TValue, bool> predicate)
        {
            predicate.ThrowIfNull(nameof(predicate));

            _policy.Check();
            return OnGet(predicate);
        }

        IEnumerable<TValue> ICache<TKey, TValue>.Remove(params TKey[] keys)
        {
            var arr = keys.Where(k => !k.IsNull()).Distinct();
            return OnRemove(t => arr.Any(k => k.Equals(GetKey(t))));
        }

        IEnumerable<TValue> ICache<TKey, TValue>.Remove(Func<TValue, bool> predicate)
        {
            predicate.ThrowIfNull(nameof(predicate));
            return OnRemove(predicate);
        }

        void ICache<TKey, TValue>.Set(TKey key, TValue value)
        {
            key.ThrowIfNull(nameof(key));
            value.ThrowIfNull(nameof(key));
            OnSet(key, value);
        }

        /// <summary>
        /// 校验缓存是否失效，
        /// 若缓存失效，返回 true；否则，返回 false .
        /// <para>请注意，在缓存已失效时，将发生缓存失效事件 <see cref="ICachePolicy.OnInvalid"/> .</para>
        /// </summary>
        /// <returns></returns>
        public bool OnCheck()
        {
            return _policy.Check();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _policy.Dispose();
        }
        
        /// <summary>
        /// 设置缓存中指定 key 的数据对象为指定的 value 值 .
        /// </summary>
        /// <param name="key">指定的 key .</param>
        /// <param name="value">指定的 value .</param>
        protected virtual void OnSet(TKey key, TValue value)
        {
            ((ICache<TKey, TValue>)this).Remove(key);
            ((ICache<TKey, TValue>)this).Add(value);
        }

        /// <summary>
        /// 缓存失效回调
        /// </summary>
        protected abstract void OnCacheInvalidCallback();

        /// <summary>
        /// 增加指定的数据对象到缓存中
        /// </summary>
        /// <param name="value">指定增加的数据对象</param>
        protected abstract void OnAdd(TValue value);

        /// <summary>
        /// 移除满足指定谓词条件的所有数据对象，并返回一个被移除的数据对象集合
        /// </summary>
        /// <param name="predicate">筛选数据对象的谓词条件</param>
        /// <returns></returns>
        protected abstract IEnumerable<TValue> OnRemove(Func<TValue, Boolean> predicate);

        /// <summary>
        /// 获取满足指定谓词条件的所有数据对象集合，若不存在，返回一组长度为 0 的数据集合
        /// </summary>
        /// <param name="predicate">筛选数据对象的谓词条件</param>
        /// <returns></returns>
        protected abstract IEnumerable<TValue> OnGet(Func<TValue, Boolean> predicate);
        
        /// <summary>
        /// 获取缓存策略
        /// </summary>
        /// <returns></returns>
        protected abstract ICachePolicy GetPolicy();

        /// <summary>
        /// 获取指定数据对象的 key 值
        /// </summary>
        /// <param name="value">数据对象</param>
        /// <returns></returns>
        protected abstract TKey GetKey(TValue value);
    }
}
