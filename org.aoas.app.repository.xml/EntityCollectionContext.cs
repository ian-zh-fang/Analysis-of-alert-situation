
/*
 * guid: $GUID$
 * file: EntityCollectionContext
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/25 15:56:06
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
    using System.Collections;
    using System.Collections.Generic;
    using org.aoas.attributes;
    using org.aoas.config;
    using org.aoas.file;

    /// <summary>
    /// 旨在实现数据集合上下文存取数据对象的业务功能
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public sealed class EntityCollectionContext<TCollection> 
        : XmlConfiguration
        , IEnumerable<TCollection>
        , IDisposable
        where TCollection : XmlConfigurationElement
    {
        // 当前对象释放标识，true 标识已释放；否则，标识尚未释放
        private bool _isDisposed;

        /// <summary>
        /// 创建 <see cref="EntityCollectionContext{TKey, TCollection}"/> 类型的数据集合上下文对象
        /// </summary>
        /// <param name="fileName">数据文件名称</param>
        /// <param name="section">数据正文配置节点名称</param>
        /// <param name="root">数据配置根节点名称</param>
        /// <param name="dirs">数据文件可能存在的目录</param>
        public EntityCollectionContext(string fileName, string section = "data", string root = "context", params string[] dirs)
            : base(section, root, fileName, new FileFinder(dirs))
        {
            _isDisposed = false;
        }

        //DOM 结构为：
        // <context>
        //  <data>
        //      <entities>
        //          <entity id="" name="" code="" pid="" syncId="" desc="">
        //              <entity id="" name="" code="" pid="" syncId="" desc=""></entity>
        //              <entity id="" name="" code="" pid="" syncId="" desc=""></entity>
        //          </entity>
        //          <entity id="" name="" code="" pid="" syncId="" desc="">
        //              <entity id="" name="" code="" pid="" syncId="" desc=""></entity>
        //              <entity id="" name="" code="" pid="" syncId="" desc=""></entity>
        //          </entity>
        //      </entities>
        //  </data>
        // </context>

        /// <summary>
        /// 数据对象集合
        /// </summary>
        [Alias("entities")]
        public EntityCollection<TCollection> Collection { get; set; }

        public void Dispose()
        {
            Disposed(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void Disposed(bool disposing)
        {
            if (_isDisposed) { return; }
            _isDisposed = true;

            Collection = null;
        }

        /// <summary>
        /// 重新加载数据上下文
        /// </summary>
        public void ReLoad()
        {
            Init();
        }

        IEnumerator<TCollection> IEnumerable<TCollection>.GetEnumerator()
        {
            return ((IEnumerable<TCollection>)Collection).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<TCollection>)this).GetEnumerator();
        }

        ~EntityCollectionContext()
        {
            Disposed(disposing: false);
        }
    }
}
