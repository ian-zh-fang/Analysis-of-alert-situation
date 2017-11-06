
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyNullableInt16
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/6 11:32:33
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

    public sealed class XmlConfigurationPropertyNullableInt16
        :XmlConfigurationProperty<Int16?>
    {
        public XmlConfigurationPropertyNullableInt16()
            :base()
        { }

        public XmlConfigurationPropertyNullableInt16(short? v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertyNullableInt16(short? v)
        {
            return new XmlConfigurationPropertyNullableInt16(v);
        }

        public static implicit operator XmlConfigurationPropertyNullableInt16(short v)
        {
            return v;
        }

        public static implicit operator short?(XmlConfigurationPropertyNullableInt16 pro)
        {
            return pro?.Value;
        }

        public static implicit operator short(XmlConfigurationPropertyNullableInt16 pro)
        {
            var val = pro?.Value;
            val.ThrowIfNull();

            return val.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            if (valueStr.IsWhitespaces()) { return null; }

            return short.Parse(valueStr);
        }
    }
}
