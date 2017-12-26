
/*
 * guid: $GUID$
 * file: XmlCacheRepository
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/26 11:28:23
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

namespace org.aoas.app.repository.xml
{
    using System;
    using org.aoas.cache;

    /// <summary>
    /// 旨在实现一种支持缓存 XML 数据文件的基础业务功能
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityKey"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public abstract class XmlCacheRepository<TKey, TEntity, TData>
        : XmlRepository<TEntity, TData>
        , ICacheContext
        where TData : EntityCollection<TData>
    {
        // 缓存上下文实例初始化时，发生当前事件
        private event Action _eventInited;

        // 缓存上下文内容发生变化时，发生当前事件
        private event Action _eventChanged;

        // 缓存上下文
        private ICache<TKey, TEntity> _cache;

        protected XmlCacheRepository(string fileName = "data.xml", string section = "context", string root = "data", params string[] dirs)
            : base(fileName, section, root, dirs)
        { }

        protected override void OnInit(EntityCollectionContext<TData> collection)
        {
            base.OnInit(collection);
            _cache = InitCache(collection);
        }

        // 初始化缓存上下文实例
        private ICache<TKey, TEntity> InitCache(EntityCollectionContext<TData> collection)
        {
            var cache = GetCache(collection, this);
            cache.ThrowIfNull(nameof(cache));

            return cache;
        }

        event Action ICacheContext.OnInit
        {
            add
            {
                if (value.IsNull()) { return; }
                _eventInited += value;
            }

            remove
            {
                if (value.IsNull()) { return; }
                _eventInited -= value;
            }
        }

        event Action ICacheContext.OnChange
        {
            add
            {
                if (value.IsNull()) { return; }
                _eventChanged += value;
            }

            remove
            {
                if (value.IsNull()) { return; }
                _eventChanged -= value;
            }
        }

        /// <summary>
        /// 获取缓存上下文实例
        /// </summary>
        /// <param name="collection"><see cref="EntityCollectionContext{TCollection}"/> XML 数据集合上下文</param>
        /// <param name="context"><see cref="ICacheContext"/> 缓存上下文实例</param>
        protected abstract XmlCache<TEntity> GetCache(EntityCollectionContext<TData> collection, ICacheContext context);

        /// <summary>
        /// 旨在实现支持 XML 数据集合上下文业务处理功能模块
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        internal protected abstract class XmlCache<TValue> : MemoryCache<TKey, TValue>
        {
            /// <summary>
            /// 创建 <see cref="XmlCache{TValue}"/> XML 数据集合上下文缓存实例
            /// </summary>
            /// <param name="collection"><see cref="EntityCollectionContext{TCollection}"/> XML 数据集合上下文</param>
            /// <param name="context"><see cref="ICacheContext"/> 缓存上下文实例</param>
            protected XmlCache(EntityCollectionContext<TData> collection, ICacheContext context)
                :base(context)
            { }
        }
    }
}
