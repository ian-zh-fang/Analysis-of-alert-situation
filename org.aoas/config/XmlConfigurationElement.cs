
/*
 * guid: $GUID$
 * file: XmlConfigurationElement
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:44:34
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
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    public abstract class XmlConfigurationElement
    {
        private const BindingFlags FLAGS = BindingFlags.Public | BindingFlags.Instance;

        protected virtual string GetPropertyName(PropertyInfo property)
        {
            var name = property.Name.ToLower();
            var attr = property.GetCustomAttribute<attributes.AliasAttribute>();
            if (attr.IsNull()) { return name; }

            return attr.Name.ToLower();
        }

        internal void Serialize(XmlWriter writer)
        {
            OnSerialize(writer);
        }

        internal void Deserialize(XmlReader reader)
        {
            var arr = GetType().GetProperties(FLAGS);
            DeserializeAttribute(reader, arr);
            DeserializeElement(reader, arr);
        }

        protected virtual void OnSerialize(XmlWriter writer)
        {
            var arr = GetType().GetProperties(FLAGS);
            var elementType = typeof(XmlConfigurationElement);
            foreach (var pro in arr)
            {
                var value = pro.GetValue(this, null);
                if (value.IsNull()) { continue; }

                var name = GetPropertyName(pro);
                if (elementType.IsAssignableFrom(pro.PropertyType))
                {
                    OnSerializeElement(name, value as XmlConfigurationElement, writer, pro);
                    continue;
                }

                OnSerializeAttribute(name, value, writer, pro);
            }
        }

        protected virtual void OnSerializeAttribute(string elementName, object value, XmlWriter writer, PropertyInfo property)
        {
            writer.WriteAttributeString(elementName, value.ToString());
        }

        protected virtual void OnSerializeElement(string elementName, XmlConfigurationElement value, XmlWriter writer, PropertyInfo property)
        {
            writer.WriteStartElement(elementName);
            value.OnSerialize(writer);
            //writer.WriteFullEndElement();
            writer.WriteEndElement();
        }
        
        private void SetProperty(object value, PropertyInfo property)
        {
            if (value.IsNull()) { return; }
            var method =
                property.SetMethod ??
                property.DeclaringType.GetProperty(property.Name, FLAGS).SetMethod;
            if (method.IsNull()) { return; }

            method.Invoke(this, new object[] { value });
        }

        protected Type GetElementType(XmlReader reader, Type defaultType)
        {
            var rawType = defaultType;
            const string __Attr = "type";
            if (reader.MoveToAttribute(__Attr))
            {
                var typeStr = reader.Value;
                rawType = Type.GetType(typeStr);
                reader.MoveToElement();
            }

            return rawType;
        }

        protected virtual XmlConfigurationElement CreateElement(XmlReader reader, PropertyInfo property)
        {
            var type = GetElementType(reader, property.PropertyType);
            if (type.IsSubclassOf(typeof(XmlConfigurationElement)))
            {
                return Activator.CreateInstance(type) as XmlConfigurationElement;
            }

            throw new NotSupportedException();
        }

        protected virtual object GetValue(string valueStr, Type valueType)
        {
            if (valueType == typeof(string)) { return valueStr; }
            if (valueType.IsPrimitive) { return ToPrimitiveValue(valueStr, valueType); }
            if (valueType.IsNullable()) { return ToNullableValue(valueStr, valueType); }

            return Convert.ChangeType(valueStr, valueType);
        }

        private object ToPrimitiveValue(string valueStr, Type valueType)
        {
            const string _Name = "Parse";
            var method = valueType.GetMethod(_Name,
                BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, new Type[] { typeof(string) }, new ParameterModifier[] { new ParameterModifier(1) });
            if (null == method) { return null; }

            return method.Invoke(null, new object[] { valueStr });
        }

        private object ToNullableValue(string valueStr, Type valueType)
        {
            var rawType = valueType.GenericTypeArguments.First();
            var rawValue = GetValue(valueStr, rawType);

            var actor = valueType.GetConstructor(BindingFlags.Public, Type.DefaultBinder, new Type[] { rawType }, new ParameterModifier[] { new ParameterModifier(1) });
            if (null == actor) { return null; }

            return actor.Invoke(new object[] { rawValue });
        }

        private PropertyInfo GetProperty(string name, PropertyInfo[] properties)
        {
            name = name.ToLower();
            return
                properties.FirstOrDefault(t => name == GetPropertyName(t));
        }

        private void DeserializeElement(XmlReader reader, PropertyInfo[] properties)
        {
            // 空元素停止处理
            if (reader.IsEmptyElement) { return; }

            // 处理非空元素
            var currentRoot = reader.Name;
            while (reader.Read())
            {
                var ndType = reader.NodeType;
                if (currentRoot == reader.Name && ndType == XmlNodeType.EndElement) { break; }
                if (ndType != XmlNodeType.Element) { continue; }
                OnDeserializeElement(reader, properties);
            }
        }

        protected virtual void OnDeserializeElement(XmlReader reader, PropertyInfo[] properties)
        {
            var name = reader.Name;
            var pro = GetProperty(name, properties);
            if (pro.IsNull())
            {
                if (OnDeserializeUnrecognizeElement(name, reader)) { return; }
                throw new Exception("Unrecognize element dose not processed .");
            }

            var element = CreateElement(reader, pro);
            element.Deserialize(reader);
            SetProperty(element, pro);
        }

        private void DeserializeAttribute(XmlReader reader, PropertyInfo[] properties)
        {
            var size = reader.AttributeCount;
            for (int i = 0; i < size; i++)
            {
                reader.MoveToAttribute(i);
                OnDeserializeAttribute(reader, properties);
            }
            if (reader.HasAttributes) { reader.MoveToElement(); }
        }

        protected virtual void OnDeserializeAttribute(XmlReader reader, PropertyInfo[] properties)
        {
            var name = reader.Name;
            var pro = GetProperty(name, properties);
            if (null == pro)
            {
                if (OnDeserializeUnrecognizeAttribute(name, reader)) { return; }
                throw new Exception("Unrecognize attribute dose not processed .");
            }

            OnDeserializeAttribute(reader.Value, pro);
        }

        protected virtual void OnDeserializeAttribute(string valueStr, PropertyInfo property)
        {
            var val = GetValue(valueStr, property.PropertyType);
            if (val.IsNull()) { return; }

            SetProperty(val, property);
        }
        
        protected virtual bool OnDeserializeUnrecognizeAttribute(string name, XmlReader reader) { return false; }

        protected virtual bool OnDeserializeUnrecognizeElement(string name, XmlReader reader) { return false; }
    }
}
