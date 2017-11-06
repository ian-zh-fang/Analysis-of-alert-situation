
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyNullableUInt64
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/6 12:21:04
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

    public sealed class XmlConfigurationPropertyNullableUInt64
        :XmlConfigurationProperty<UInt64?>
    {
        public XmlConfigurationPropertyNullableUInt64()
            :base()
        { }

        public XmlConfigurationPropertyNullableUInt64(ulong? v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertyNullableUInt64(ulong? v)
        {
            return new XmlConfigurationPropertyNullableUInt64(v);
        }

        public static implicit operator XmlConfigurationPropertyNullableUInt64(ulong v)
        {
            return v;
        }

        public static implicit operator ulong?(XmlConfigurationPropertyNullableUInt64 pro)
        {
            return pro?.Value;
        }

        public static implicit operator ulong(XmlConfigurationPropertyNullableUInt64 pro)
        {
            var val = pro?.Value;
            val.ThrowIfNull();

            return val.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            if (valueStr.IsWhitespaces()) { return null; }

            return ulong.Parse(valueStr);
        }
    }
}
