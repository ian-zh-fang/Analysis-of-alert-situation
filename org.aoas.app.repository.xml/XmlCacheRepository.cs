
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
    public abstract class XmlCacheRepository<TEntity, TEntityKey, TData>
        : XmlRepository<TEntity, TEntityKey, TData>
        , ICacheContext
        where TEntityKey : IComparable, IConvertible, IComparable<TEntityKey>, IEquatable<TEntityKey>
        where TData : config.XmlConfigurationArray<TData>, new()
    {
        // 缓存上下文初始化时，发生当前事件
        private event Action _eventInit;

        // 缓存上下文数据发生变化时，发生当前事件
        private event Action _eventChange;

        // 缓存上下文
        private readonly ICache<TEntityKey, TEntity> _cache;

        /// <summary>
        /// 创建 <see cref="XmlCacheRepository{TEntity, TEntityKey, TData}"/> 缓存 XML 文件数据仓储的实例
        /// </summary>
        /// <param name="fileName">数据文件名称</param>
        /// <param name="section">数据节点名称</param>
        /// <param name="root">数据根节点名称</param>
        /// <param name="dirs">数据文件可能存在的目录</param>
        protected XmlCacheRepository(string fileName = "data.xml", string section = "context", string root = "data", params string[] dirs)
            :base(fileName, section, root, dirs)
        { }

        event Action ICacheContext.OnInit
        {
            add
            {
                if (value.IsNull()) { return; }
                _eventInit += value;
            }

            remove
            {
                if (value.IsNull()) { return; }
                _eventInit -= value;
            }
        }

        event Action ICacheContext.OnChange
        {
            add
            {
                if (value.IsNull()) { return; }
                _eventChange += value;
            }

            remove
            {
                if (value.IsNull()) { return; }
                _eventChange -= value;
            }
        }
    }
}
