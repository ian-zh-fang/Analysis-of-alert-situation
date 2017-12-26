﻿
/*
 * guid: $GUID$
 * file: XmlConfigurationArray
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:49:24
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

namespace org.aoas.config
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Xml;

    public abstract class XmlConfigurationArray<TElement>
        : XmlConfigurationElement
        , IEnumerable<TElement>
        where TElement : XmlConfigurationElement
    {
        // add 节点名称
        private readonly string _addElementName;
        // remove 节点名称
        private readonly string _removeElementName;
        // clear 节点名称
        private readonly string _clearElementName;

        private readonly List<TElement> _collection;

        protected XmlConfigurationArray(string addElementName, string removeElementName = "remove", string clearElementName = "clear")
        {
            addElementName.ThrowIfWhitespace(nameof(addElementName));
            removeElementName.ThrowIfWhitespace(nameof(removeElementName));
            clearElementName.ThrowIfWhitespace(nameof(clearElementName));

            _addElementName = addElementName;
            _removeElementName = removeElementName;
            _clearElementName = clearElementName;
            _collection = new List<TElement>();
        }

        protected XmlConfigurationArray() : this("add") { }

        IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<TElement>)this).GetEnumerator();
        }

        public void Add(TElement item)
        {
            _collection.Add(item);
        }

        public void AddRange(IEnumerable<TElement> items)
        {
            _collection.AddRange(items);
        }

        protected override void OnSerialize(XmlWriter writer)
        {
            base.OnSerialize(writer);
            _collection.ForEach(t =>
            {
                writer.WriteStartElement(_addElementName);
                t.Serialize(writer);
                writer.WriteEndElement();
            });
        }

        protected override void OnDeserializeElement(XmlReader reader, PropertyInfo[] properties)
        {
            var name = reader.Name;
            if (_addElementName == name)
            {
                OnDeserializeAddElement(reader);
                return;
            }

            if (_removeElementName == name)
            {
                OnDeserializeRemoveElement(reader);
                return;
            }

            if (_clearElementName == name)
            {
                OnDeserializeClearElement(reader);
                return;
            }

            if (OnDeserializeUnrecognizeElement(name, reader)) { return; }
            throw new Exception("Unrecognize element dose not processed .");
        }

        protected virtual void OnDeserializeAddElement(XmlReader reader)
        {
            var element = OnGetChildElement(reader);
            element.Deserialize(reader);
            _collection.Add(element);
        }

        protected virtual void OnDeserializeRemoveElement(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnDeserializeClearElement(XmlReader reader)
        {
            _collection.Clear();
        }

        /// <summary>
        /// 获取当前节点对应实体对象
        /// </summary>
        /// <returns></returns>
        protected abstract TElement OnGetChildElement(XmlReader reader);
    }

    /// <summary>
    /// 旨在定义一种集合配置项的基础内容结构
    /// </summary>
    public abstract class XmlConfigurationArray
        : XmlConfigurationArray<XmlConfigurationElement>
        , IEnumerable<XmlConfigurationElement>
    {
        protected XmlConfigurationArray(string addElementName, string removeElementName = "remove", string clearElementName = "clear")
            : base(addElementName, removeElementName, clearElementName)
        { }

        protected XmlConfigurationArray() : this("add") { }
    }
}
