
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyNullableBoolean
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:31:21
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

    public sealed class XmlConfigurationPropertyNullableBoolean
        :XmlConfigurationProperty<Boolean?>
    {
        public XmlConfigurationPropertyNullableBoolean()
            :base()
        { }

        public XmlConfigurationPropertyNullableBoolean(bool? value)
            :base(value)
        { }

        public static implicit operator XmlConfigurationPropertyNullableBoolean(bool? value)
        {
            return new XmlConfigurationPropertyNullableBoolean(value);
        }

        public static implicit operator XmlConfigurationPropertyNullableBoolean(bool value)
        {
            return value;
        }

        public static implicit operator bool?(XmlConfigurationPropertyNullableBoolean pro)
        {
            return pro?.Value;
        }

        public static implicit operator bool(XmlConfigurationPropertyNullableBoolean pro)
        {
            var val = pro?.Value;
            val.ThrowIfNull();

            return val.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            if (valueStr.IsWhitespaces()) { return null; }

            return bool.Parse(valueStr.ToLower());
        }
    }
}
