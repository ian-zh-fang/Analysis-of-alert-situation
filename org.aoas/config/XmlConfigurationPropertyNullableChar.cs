
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyNullableChar
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/6 10:56:08
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

    public sealed class XmlConfigurationPropertyNullableChar:XmlConfigurationProperty<char?>
    {
        public XmlConfigurationPropertyNullableChar()
            : base()
        { }

        public XmlConfigurationPropertyNullableChar(char? value)
            :base(value)
        { }

        public static implicit operator XmlConfigurationPropertyNullableChar(char? value)
        {
            return new XmlConfigurationPropertyNullableChar(value);
        }

        public static implicit operator XmlConfigurationPropertyNullableChar(char value)
        {
            return value;
        }

        public static implicit operator char?(XmlConfigurationPropertyNullableChar pro)
        {
            return pro?.Value;
        }

        public static implicit operator char(XmlConfigurationPropertyNullableChar pro)
        {
            var val = pro?.Value;
            val.ThrowIfNull();

            return val.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            if (valueStr.IsWhitespaces()) { return null; }

            return char.Parse(valueStr);
        }
    }
}
