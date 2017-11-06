
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyNullableDecimal
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/6 11:18:43
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

    public sealed class XmlConfigurationPropertyNullableDecimal
        :XmlConfigurationProperty<Decimal?>
    {
        public XmlConfigurationPropertyNullableDecimal()
            :base()
        { }

        public XmlConfigurationPropertyNullableDecimal(decimal? value)
            :base(value)
        { }

        public static implicit operator XmlConfigurationPropertyNullableDecimal(decimal? value)
        {
            return new XmlConfigurationPropertyNullableDecimal(value);
        }

        public static implicit operator XmlConfigurationPropertyNullableDecimal(decimal value)
        {
            return value;
        }

        public static implicit operator decimal?(XmlConfigurationPropertyNullableDecimal pro)
        {
            return pro?.Value;
        }

        public static implicit operator decimal(XmlConfigurationPropertyNullableDecimal pro)
        {
            var val = pro?.Value;
            val.ThrowIfNull();

            return val.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            if (valueStr.IsWhitespaces()) { return null; }

            return decimal.Parse(valueStr);
        }
    }
}
