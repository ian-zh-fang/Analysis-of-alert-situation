
/*
 * guid: $GUID$
 * file: EntityCollection
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/25 16:30:02
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
    using System.Xml;
    using org.aoas.attributes;
    using org.aoas.config;

    /// <summary>
    /// 旨在声明一组数据对象上下文基础结构
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    internal abstract class EntityCollection<TCollection> : XmlConfigurationArray
        where TCollection : EntityCollection<TCollection>, new()
    {
        protected EntityCollection(string addElementName = "entity", string removeElementName = "remove", string clearElementName = "clear")
            : base(addElementName, removeElementName, clearElementName)
        { }

        protected override XmlConfigurationElement OnGetChildElement(XmlReader reader)
        {
            return new TCollection();
        }
    }

    /// <summary>
    /// 旨在声明一组具有 ID 标识字段数据对象上下文基础结构
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    internal abstract class IdentityEntityCollection<TKey, TCollection> : EntityCollection<TCollection>
        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
        where TCollection : IdentityEntityCollection<TKey, TCollection>, new()
    {
        protected IdentityEntityCollection(string addElementName = "entity", string removeElementName = "remove", string clearElementName = "clear")
            : base(addElementName, removeElementName, clearElementName)
        { }

        protected override XmlConfigurationElement OnGetChildElement(XmlReader reader)
        {
            return new TCollection();
        }

        [Alias("id")]
        public TKey Id { get; set; }
    }

    /// <summary>
    /// 旨在声明一组具有父子关系数据对象上下文基础结构
    /// </summary>
    /// <typeparam name="TKey">父子标识字段类型</typeparam>
    /// <typeparam name="TCollection">数据对象</typeparam>
    internal abstract class ParentEntityCollection<TKey, TCollection> : IdentityEntityCollection<TKey, TCollection>
        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
        where TCollection : ParentEntityCollection<TKey, TCollection>, new()
    {
        protected ParentEntityCollection(string addElementName = "add", string removeElementName = "remove", string clearElementName = "clear")
            : base(addElementName, removeElementName, clearElementName)
        { }

        [Alias("pid")]
        public TKey Pid { get; set; }
    }
}
