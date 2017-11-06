
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyNullableInt32
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:57:10
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

    public sealed class XmlConfigurationPropertyNullableInt32
        : XmlConfigurationProperty<Int32?>
    {
        public XmlConfigurationPropertyNullableInt32()
            :base()
        { }

        public XmlConfigurationPropertyNullableInt32(int? value)
            :base(value)
        { }

        public static implicit operator XmlConfigurationPropertyNullableInt32(int? value)
        {
            return new XmlConfigurationPropertyNullableInt32(value);
        }

        public static implicit operator XmlConfigurationPropertyNullableInt32(int value)
        {
            return value;
        }

        public static implicit operator int?(XmlConfigurationPropertyNullableInt32 pro)
        {
            return pro?.Value;
        }

        public static implicit operator int(XmlConfigurationPropertyNullableInt32 pro)
        {
            var val = pro?.Value;
            val.ThrowIfNull();

            return val.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            if (valueStr.IsWhitespaces()) { return null; }

            return int.Parse(valueStr);
        }
    }
}
