
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyNullableUInt16
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/6 11:57:13
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

    public sealed class XmlConfigurationPropertyNullableUInt16
        :XmlConfigurationProperty<UInt16?>
    {
        public XmlConfigurationPropertyNullableUInt16()
            :base()
        { }

        public XmlConfigurationPropertyNullableUInt16(ushort? v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertyNullableUInt16(ushort? v)
        {
            return new XmlConfigurationPropertyNullableUInt16(v);
        }

        public static implicit operator XmlConfigurationPropertyNullableUInt16(ushort v)
        {
            return v;
        }

        public static implicit operator ushort?(XmlConfigurationPropertyNullableUInt16 pro)
        {
            return pro?.Value;
        }

        public static implicit operator ushort(XmlConfigurationPropertyNullableUInt16 pro)
        {
            var val = pro?.Value;
            val.ThrowIfNull();

            return val.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            if (valueStr.IsWhitespaces()) { return null; }

            return ushort.Parse(valueStr);
        }
    }
}
