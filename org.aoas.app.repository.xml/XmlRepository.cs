
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
    using System.Linq;

    /// <summary>
    /// 旨在实现使用 XML 文件进行数据存取的业务功能 .
    /// 1，支持将指定类型的数据保存到指定的文件中
    /// 2，支持从指定的数据文件中取出指定的数据
    /// </summary>
    public abstract class XmlRepository<TEntity, TData> : Repository<TEntity>
        where TData: EntityCollection<TData>
    {
        // 数据文件默认目录
        private static readonly string[] _DefaultDataDirs = new string[] { @"/data", @"/shared/data" };

        // 数据对象集合上下文
        private readonly EntityCollectionContext<TData> _collection;

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
            
            var dirArr = dirs.Where(t => !t.IsWhitespaces()).Concat(_DefaultDataDirs).ToArray();
            _collection = InitCollection(section, root, fileName, dirArr);
            OnInit(_collection);
        }

        /// <summary>
        /// 初始化 <see cref="XmlRepository{TEntity, TData}"/> XML 数据仓储对象内核
        /// </summary>
        protected virtual void OnInit(EntityCollectionContext<TData> collection) { }

        // 初始化数据对象集合上下文
        private EntityCollectionContext<TData> InitCollection(string section, string root, string fileName, string[] dirs)
        {
            var collection =  OnRead(section, root, fileName, dirs);
            collection.ThrowIfNull(nameof(collection));

            return collection;
        }

        /// <summary>
        /// 读取 XML 数据文件的数据内容，并返回数据集合上下文对象
        /// </summary>
        /// <param name="section">数据节点名称</param>
        /// <param name="root">数据根节点名称</param>
        /// <param name="fileName">数据文件名称</param>
        /// <param name="dirs">数据文件可能存放路径</param>
        protected virtual EntityCollectionContext<TData> OnRead(string section, string root, string fileName, string[] dirs)
        {
            return new EntityCollectionContext<TData>(fileName, section, root, dirs);
        }
    }
}
