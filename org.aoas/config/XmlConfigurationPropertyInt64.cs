
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyInt64
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:26:05
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

    public sealed class XmlConfigurationPropertyInt64
        :XmlConfigurationProperty<Int64>
    {
        public XmlConfigurationPropertyInt64()
            :base()
        { }

        public XmlConfigurationPropertyInt64(long v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertyInt64(long v)
        {
            return new XmlConfigurationPropertyInt64(v);
        }

        public static implicit operator long(XmlConfigurationPropertyInt64 pro)
        {
            pro.ThrowIfNull();
            return pro.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            return long.Parse(valueStr);
        }
    }
}
