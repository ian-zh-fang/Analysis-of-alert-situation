
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertySingle
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:26:33
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

    public sealed class XmlConfigurationPropertySingle
        :XmlConfigurationProperty<Single>
    {
        public XmlConfigurationPropertySingle()
            :base()
        { }

        public XmlConfigurationPropertySingle(float v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertySingle(float v)
        {
            return new XmlConfigurationPropertySingle(v);
        }

        public static implicit operator float(XmlConfigurationPropertySingle pro)
        {
            pro.ThrowIfNull();
            return pro.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            return float.Parse(valueStr);
        }
    }
}
