
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyString
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:26:45
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

    public sealed class XmlConfigurationPropertyString
        :XmlConfigurationProperty<String>
    {
        public XmlConfigurationPropertyString()
            :base()
        { }

        public XmlConfigurationPropertyString(string v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertyString(string v)
        {
            return new XmlConfigurationPropertyString(v);
        }

        public static implicit operator string(XmlConfigurationPropertyString pro)
        {
            if (pro.IsNull()) { return null; }
            return pro.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            return valueStr;
        }
    }
}
