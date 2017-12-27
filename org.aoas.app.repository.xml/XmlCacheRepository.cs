
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
    using System.Collections.Generic;
    using System.Linq;
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
        private ICache<TKey, TData> _cache;

        /// <summary>
        /// 创建 <see cref="XmlCacheRepository{TEntity, TEntityKey, TData}"/> 支持缓存 XML 文件数据仓储的实例
        /// </summary>
        /// <param name="fileName">数据文件名称</param>
        /// <param name="section">数据节点名称</param>
        /// <param name="root">数据根节点名称</param>
        /// <param name="dirs">数据文件可能存在的目录</param>
        protected XmlCacheRepository(string fileName = "data.xml", string section = "context", string root = "data", params string[] dirs)
            : base(fileName, section, root, dirs)
        { }

        protected override void OnInit(EntityCollectionContext<TData> collection)
        {
            base.OnInit(collection);

            // 内核初始化结束，发生 _eventInited 事件
            EventInited_Invoke();
        }

        // 读取 XML 文件数据上下文
        protected override EntityCollectionContext<TData> OnRead(string section, string root, string fileName, string[] dirs)
        {
            // 初始化缓存上下文实例，用来进行查询数据对象的数据集合上下文
            _cache = InitCache(base.OnRead(section, root, fileName, dirs));

            // 返回一个用来进行更新数据的数据对象集合上下文
            return base.OnRead(section, root, fileName, dirs);
        }

        // 初始化缓存上下文实例
        private ICache<TKey, TData> InitCache(EntityCollectionContext<TData> collection)
        {
            var cache = GetCache(collection, this);
            cache.ThrowIfNull(nameof(cache));

            return cache;
        }

        // 发生 _eventInited 事件
        private void EventInited_Invoke()
        {
            if (_eventInited.IsNull()) { return; }
            _eventInited.Invoke();
        }

        // 发生 _eventChanged 事件
        private void EventChanged_Invoke()
        {
            if (_eventChanged.IsNull()) { return; }
            _eventChanged.Invoke();
        }

        protected override bool OnSaveChanges()
        {
            var sta = base.OnSaveChanges();
            if (sta)
            {
                // 数据集合上下文发生变化，发生 _eventChanged 事件
                EventChanged_Invoke();
            }

            return sta;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            // 释放缓存资源
            if (_cache.IsNull()) { return; }
            _cache.Dispose();
            _cache = null;
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
        protected abstract XmlCache GetCache(EntityCollectionContext<TData> collection, ICacheContext context);

        /// <summary>
        /// 旨在实现支持 XML 数据集合上下文业务处理功能模块
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        internal protected abstract class XmlCache : Cache<TKey, TData>
        {
            // 内部数据集合上下文
            private readonly EntityCollectionContext<TData> _innerCollection;

            // 是否正在更新数据
            private bool _isRechanged;

            /// <summary>
            /// 创建 <see cref="XmlCache"/> XML 数据集合上下文缓存实例
            /// </summary>
            /// <param name="collection"><see cref="EntityCollectionContext{TCollection}"/> XML 数据集合上下文</param>
            /// <param name="context"><see cref="ICacheContext"/> 缓存上下文实例</param>
            protected XmlCache(EntityCollectionContext<TData> collection, ICacheContext context)
                :base(context)
            {
                _isRechanged = false;
                collection.ThrowIfNull(nameof(collection));
                _innerCollection = collection;
            }

            /// <summary>
            /// 创建 <see cref="XmlCache"/> XML 数据集合上下文缓存实例
            /// </summary>
            /// <param name="fileName">数据文件名称</param>
            /// <param name="section">数据节点名称</param>
            /// <param name="root">数据根节点名称</param>
            /// <param name="dirs">数据文件可能存在的目录</param>
            /// <param name="context"><see cref="ICacheContext"/> 数据缓存上下文实例</param>
            protected XmlCache(string section, string root, string fileName, string[] dirs, ICacheContext context)
                :this(new EntityCollectionContext<TData>(fileName, section, root, dirs), context)
            { }

            protected override void OnCacheInvalidCallback()
            {
                _isRechanged = true;
                _innerCollection.ReLoad();
                _isRechanged = false;
            }

            /// <summary>
            /// 获取一维数据集合列表，返回一组一维数据对象集合列表
            /// </summary>
            /// <param name="items">一组 <see cref="TData"/> 数据对象集合上下文</param>
            /// <returns></returns>
            protected virtual IEnumerable<TData> GetCollection(IEnumerable<TData> items)
            {
                if (items.IsNull()) { return new TData[0]; }                
                if (0 == items.Count()) { return new TData[0]; }

                // 非空队列
                var rootArr = new List<TData>();
                // 子队列
                IEnumerable<TData> childs = new TData[0];
                foreach (var item in items)
                {
                    rootArr.Add(item);
                    childs = childs.Concat(GetCollection(item));
                }

                // 连接根列表和子列表，并返回
                return
                    rootArr.Concat(childs);
            }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);

                // 释放内部数据集合上下文占用的系统资源
                _innerCollection.Dispose();
            }

            /// <summary>
            /// 从 <see cref="TData"/> 数据对象中获取 <see cref="TEntity"/> 实例
            /// </summary>
            /// <param name="data"><see cref="TData"/> 数据对象实例</param>
            /// <returns></returns>
            protected abstract TEntity ConvertFrom(TData data);
        }
    }
}
