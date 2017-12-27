
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

        /// <summary>
        /// 将指定的 <see cref="TElement"/> XML 节点元素添加到上下文中
        /// </summary>
        /// <param name="item"><see cref="TElement"/> XML 节点元素实例</param>
        public void Add(TElement item)
        {
            _collection.Add(item);
        }

        /// <summary>
        /// 将一组指定的 <see cref="TElement"/> XML 节点元素集合添加到上下文中
        /// </summary>
        /// <param name="items">一组 <see cref="TElement"/> XML 节点元素集合</param>
        public void AddRange(IEnumerable<TElement> items)
        {
            _collection.AddRange(items);
        }

        /// <summary>
        /// 将匹配指定谓词条件的元素进行指定的操作，
        /// 若发生操作并操作成功，则返回 true；否则，返回 false .
        /// </summary>
        /// <param name="predicate">指定的谓词筛选条件</param>
        /// <param name="expres">指定的元素操作</param>
        /// <returns></returns>
        public bool Set(Action<TElement> expres, Func<TElement, Boolean> predicate)
        {
            var sta = false;
            foreach (var item in _collection)
            {
                if (predicate.Invoke(item))
                {
                    // 匹配对象成功，更新用户
                    expres.Invoke(item);
                    sta = true;
                    break;
                }
            }
            return sta;
        }

        /// <summary>
        /// 从上下文中移除匹配指定谓词条件的元素，并返回一组被移除的元素集合
        /// </summary>
        /// <param name="predicate">指定的谓词筛选条件</param>
        public IEnumerable<TElement> Remove(Func<TElement, Boolean> predicate)
        {
            var changes = new List<TElement>();
            if (predicate.IsNull()) { return changes; }

            // 非空条件处理
            var newArr = new List<TElement>();
            foreach (var item in _collection)
            {
                // 筛选出需要移除的元素
                if(predicate.Invoke(item))
                {
                    changes.Add(item);
                    continue;
                }

                // 使用新的集合保存不需要移除的元素
                newArr.Add(item);
            }

            // 移除发生的变更
            if(0 < changes.Count)
            {
                _collection.Clear();
                _collection.AddRange(newArr);
            }

            return changes;
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
