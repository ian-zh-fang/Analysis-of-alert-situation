
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyNullableUInt32
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/6 12:06:54
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

    public sealed class XmlConfigurationPropertyNullableUInt32
        :XmlConfigurationProperty<uint?>
    {
        public XmlConfigurationPropertyNullableUInt32()
            :base()
        { }

        public XmlConfigurationPropertyNullableUInt32(uint? v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertyNullableUInt32(uint? v)
        {
            return new XmlConfigurationPropertyNullableUInt32(v);
        }

        public static implicit operator XmlConfigurationPropertyNullableUInt32(uint v)
        {
            return v;
        }

        public static implicit operator uint?(XmlConfigurationPropertyNullableUInt32 pro)
        {
            return pro?.Value;
        }

        public static implicit operator uint(XmlConfigurationPropertyNullableUInt32 pro)
        {
            var val = pro?.Value;
            val.ThrowIfNull();

            return val.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            if (valueStr.IsWhitespaces()) { return null; }

            return uint.Parse(valueStr);
        }
    }
}
