
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyNullableInt64
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/6 11:41:58
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

    public sealed class XmlConfigurationPropertyNullableInt64
        :XmlConfigurationProperty<Int64?>
    {
        public XmlConfigurationPropertyNullableInt64()
            :base()
        { }

        public XmlConfigurationPropertyNullableInt64(long? v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertyNullableInt64(long? v)
        {
            return new XmlConfigurationPropertyNullableInt64(v);
        }

        public static implicit operator XmlConfigurationPropertyNullableInt64(long v)
        {
            return v;
        }

        public static implicit operator long?(XmlConfigurationPropertyNullableInt64 pro)
        {
            return pro?.Value;
        }

        public static implicit operator long(XmlConfigurationPropertyNullableInt64 pro)
        {
            var val = pro?.Value;
            val.ThrowIfNull();

            return val.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            if (valueStr.IsWhitespaces()) { return null; }

            return long.Parse(valueStr);
        }
    }
}
