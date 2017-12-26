
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
    internal sealed class EntityCollectionContext<TKey, TCollection> 
        : XmlConfiguration
        , IEnumerable<TCollection>
        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
        where TCollection : XmlConfigurationArray<TCollection>, new()
    {
        /// <summary>
        /// 创建 <see cref="EntityCollectionContext{TKey, TCollection}"/> 类型的数据集合上下文对象
        /// </summary>
        /// <param name="fileName">数据文件名称</param>
        /// <param name="section">数据正文配置节点名称</param>
        /// <param name="root">数据配置根节点名称</param>
        /// <param name="dirs">数据文件可能存在的目录</param>
        public EntityCollectionContext(string fileName, string section = "data", string root = "context", params string[] dirs)
            : base(section, root, fileName, new FileFinder(dirs))
        { }

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
        public IdentityEntityCollection<TKey, TCollection> Collection { get; set; }

        IEnumerator<TCollection> IEnumerable<TCollection>.GetEnumerator()
        {
            return ((IEnumerable<TCollection>)Collection).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<TCollection>)this).GetEnumerator();
        }
    }
}
