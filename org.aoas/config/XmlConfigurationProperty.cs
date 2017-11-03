
/*
 * guid: $GUID$
 * file: XmlConfigurationProperty
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 15:26:37
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
    using System.Xml;

    /// <summary>
    /// XML 配置文件属性配置节点类型
    /// </summary>
    /// <typeparam name="TValue">配置属性类型</typeparam>
    public class XmlConfigurationProperty<TValue> : XmlConfigurationElement
    {
        /// <summary>
        /// 创建 XmlConfigurationProperty&lt;TValue> 类的新实例
        /// </summary>
        public XmlConfigurationProperty()
            : base()
        { }

        /// <summary>
        /// 创建 XmlConfigurationProperty&lt;TValue> 类的新实例
        /// </summary>
        /// <param name="value">值</param>
        public XmlConfigurationProperty(TValue value)
        {
            Value = value;
        }

        /// <summary>
        /// 属性值
        /// </summary>
        [attributes.Alias("value")]
        public TValue Value { get; private set; }

        protected override bool OnDeserializeUnrecognizeAttribute(string name, XmlReader reader) { return true; }

        protected override bool OnDeserializeUnrecognizeElement(string name, XmlReader reader) { return true; }

        /// <summary>
        /// 创建一个新的 XmlConfigurationProperty&lt;TValue> 对象，并将其初始化为 TValue 类型的值
        /// </summary>
        /// <param name="value">TValue 类型的值</param>
        public static implicit operator XmlConfigurationProperty<TValue>(TValue value)
        {
            return new XmlConfigurationProperty<TValue>(value);
        }

        /// <summary>
        /// 定义 XmlConfigurationProperty&lt;TValue> 实例到其 TValue 类型值的显示转换
        /// </summary>
        /// <param name="pro">XmlConfigurationProperty&lt;TValue> 实例</param>
        public static explicit operator TValue(XmlConfigurationProperty<TValue> pro)
        {
            pro.ThrowIfNull(nameof(pro));
            return pro.Value;
        }
    }
}
