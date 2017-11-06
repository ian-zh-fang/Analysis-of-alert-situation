
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyUInt64
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:27:22
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

    public sealed class XmlConfigurationPropertyUInt64
        :XmlConfigurationProperty<UInt64>
    {
        public XmlConfigurationPropertyUInt64()
            :base()
        { }

        public XmlConfigurationPropertyUInt64(ulong v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertyUInt64(ulong v)
        {
            return new XmlConfigurationPropertyUInt64(v);
        }

        public static implicit operator ulong(XmlConfigurationPropertyUInt64 pro)
        {
            pro.ThrowIfNull();
            return pro.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            return ulong.Parse(valueStr);
        }
    }
}
