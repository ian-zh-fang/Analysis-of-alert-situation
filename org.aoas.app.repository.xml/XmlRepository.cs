
/*
 * guid: $GUID$
 * file: XmlRepository
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/25 11:20:52
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

    /// <summary>
    /// 旨在实现使用 XML 文件进行数据存取的业务功能 .
    /// 1，支持将指定类型的数据保存到指定的文件中
    /// 2，支持从指定的数据文件中取出指定的数据
    /// </summary>
    public abstract class XmlRepository<TEntity, TEntityKey, TData> : Repository<TEntity>
        where TEntityKey : IComparable, IConvertible, IComparable<TEntityKey>, IEquatable<TEntityKey>
        where TData : config.XmlConfigurationArray<TData>, new()
    {
        // 数据集合上下文节点名称
        private readonly string _section;

        // 数据集合上下文根节点名称
        private readonly string _root;

        // 数据文件名称
        private readonly string _filename;

        // 数据文件可能存放的目录内容
        private readonly string[] _dirs;

        /// <summary>
        /// 创建 <see cref="XmlRepository{TEntity, TEntityKey, TData}"/> XML 文件数据仓储的实例
        /// </summary>
        /// <param name="fileName">数据文件名称</param>
        /// <param name="section">数据节点名称</param>
        /// <param name="root">数据根节点名称</param>
        /// <param name="dirs">数据文件可能存在的目录</param>
        protected XmlRepository(string fileName = "data.xml", string section = "context", string root = "data", params string[] dirs) : base()
        {
            section.ThrowIfWhitespace(nameof(section));
            root.ThrowIfWhitespace(nameof(root));
            fileName.ThrowIfWhitespace(nameof(fileName));

            _section = section;
            _root = root;
            _filename = fileName;
            _dirs = dirs.Where(t => !t.IsWhitespaces()).ToArray();
        }

        protected sealed override TEntity OnInsert(TEntity entity)
        {
            var arr = GetCollection();
            return OnInsert(entity, arr);
        }

        protected sealed override TEntity OnDelete(TEntity entity)
        {
            var arr = GetCollection();
            return OnDelete(entity, arr);
        }

        protected sealed override IEnumerable<TEntity> OnDelete(Func<TEntity, bool> predicate)
        {
            var arr = GetCollection();
            return OnDelete(predicate, arr);
        }

        protected sealed override TEntity OnUpgrade(TEntity entity)
        {
            var arr = GetCollection();
            return OnUpgrade(entity, arr);
        }

        protected sealed override IQueryable<TEntity> OnFetch(Func<TEntity, bool> predicate)
        {
            var arr = GetCollection();
            return OnFetch(predicate, arr);
        }

        protected sealed override int OnSave()
        {
            var arr = GetCollection();
            var sta = OnSave(arr);

            if (sta) { return 1; }

            return -1;
        }

        /// <summary>
        /// 获取一组数据上下文集合，并返回数据集合上下文对象
        /// </summary>
        /// <returns></returns>
        private IEnumerable<TEntity> GetCollection()
        {
            var arr = OnRead(_section, _root, _filename, _dirs);
            return GetCollection(arr);
        }

        /// <summary>
        /// 从指定的数据集合中获取数据对象集合上下文，并返回一个数据对象集合上下文
        /// </summary>
        /// <param name="collection">数据集合上下文</param>
        /// <returns></returns>
        protected virtual IEnumerable<TEntity> GetCollection(IEnumerable<TData> collection)
        {
            return collection.Select(t => ConverFrom(t));
        }

        /// <summary>
        /// 读取 XML 数据文件的数据内容，并返回数据集合上下文对象
        /// </summary>
        /// <param name="section">数据节点名称</param>
        /// <param name="root">数据根节点名称</param>
        /// <param name="fileName">数据文件名称</param>
        /// <param name="dirs">数据文件可能存放路径</param>
        /// <returns></returns>
        protected virtual IEnumerable<TData> OnRead(string section, string root, string fileName, string[] dirs)
        {
            return new EntityCollectionContext<TEntityKey, TData>(fileName, section, root, dirs);
        }

        /// <summary>
        /// 将指定的数据对象添加到数据集合上下文中，并返回被添加的数据对象
        /// </summary>
        /// <param name="entity">新增数据对象</param>
        /// <param name="context">数据集合上下文</param>
        /// <returns></returns>
        protected abstract TEntity OnInsert(TEntity entity, IEnumerable<TEntity> context);

        /// <summary>
        /// 从数据集合上下文中移除指定的数据对象，并返回被移除的数据对象
        /// </summary>
        /// <param name="entity">移除数据对象</param>
        /// <param name="context">数据集合上下文</param>
        /// <returns></returns>
        protected abstract TEntity OnDelete(TEntity entity, IEnumerable<TEntity> context);

        /// <summary>
        /// 从数据集合上下文中移除匹配谓词的数据对象，并返回一组被移除的数据对象集合
        /// </summary>
        /// <param name="predicate">移除对象谓词匹配表达式</param>
        /// <param name="context">数据集合上下文</param>
        /// <returns></returns>
        protected abstract IEnumerable<TEntity> OnDelete(Func<TEntity, Boolean> predicate, IEnumerable<TEntity> context);

        /// <summary>
        /// 更新数据集合上下文中指定的实体对象，并返回更新后的数据对象
        /// </summary>
        /// <param name="entity">更新数据对象</param>
        /// <param name="context">数据集合上下文</param>
        /// <returns></returns>
        protected abstract TEntity OnUpgrade(TEntity entity, IEnumerable<TEntity> context);

        /// <summary>
        /// 从数据集合上下文中筛选匹配谓词表达式的所有数据对象，并返回一组筛选的数据对象集合
        /// </summary>
        /// <param name="predicate">筛选谓词表达式</param>
        /// <returns></returns>
        protected abstract IQueryable<TEntity> OnFetch(Func<TEntity, bool> predicate, IEnumerable<TEntity> context);

        /// <summary>
        /// 保存数据集合上下文，若保存成功，返回 true；否则，返回 false .
        /// </summary>
        /// <param name="context">数据集合上下文</param>
        /// <returns></returns>
        protected abstract bool OnSave(IEnumerable<TEntity> context);

        /// <summary>
        /// 将指定的 <see cref="TData"/> 类型的对象实例转换为 <see cref="TEntity"/> 类型的实例
        /// </summary>
        /// <param name="data">一个 <see cref="TData"/> 类型的实例</param>
        /// <returns></returns>
        protected abstract TEntity ConverFrom(TData data);

        /// <summary>
        /// 将指定的 <see cref="TEntity"/> 类型的对象实例转换为 <see cref="TData"/> 类型的实例
        /// </summary>
        /// <param name="entity">一个 <see cref="TEntity"/> 类型的实例</param>
        /// <param name="collection">一个包含</param>
        /// <returns></returns>
        protected abstract TData ConverTo(TEntity entity, IEnumerable<TEntity> collection);
    }
}
