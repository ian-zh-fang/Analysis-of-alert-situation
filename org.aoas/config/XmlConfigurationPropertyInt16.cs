
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyInt16
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:25:42
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

    public sealed class XmlConfigurationPropertyInt16
        :XmlConfigurationProperty<Int16>
    {
        public XmlConfigurationPropertyInt16()
            :base()
        { }

        public XmlConfigurationPropertyInt16(short v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertyInt16(short v)
        {
            return new XmlConfigurationPropertyInt16(v);
        }

        public static implicit operator short(XmlConfigurationPropertyInt16 pro)
        {
            pro.ThrowIfNull();
            return pro.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            return short.Parse(valueStr);
        }
    }
}
