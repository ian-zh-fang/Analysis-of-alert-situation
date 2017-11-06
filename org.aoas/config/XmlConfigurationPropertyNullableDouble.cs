
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyNullableDouble
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/6 11:23:29
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

    public sealed class XmlConfigurationPropertyNullableDouble
        :XmlConfigurationProperty<Double?>
    {
        public XmlConfigurationPropertyNullableDouble()
            :base()
        { }

        public XmlConfigurationPropertyNullableDouble(double? v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertyNullableDouble(double? v)
        {
            return new XmlConfigurationPropertyNullableDouble(v);
        }

        public static implicit operator XmlConfigurationPropertyNullableDouble(double v)
        {
            return v;
        }

        public static implicit operator double?(XmlConfigurationPropertyNullableDouble pro)
        {
            return pro?.Value;
        }

        public static implicit operator double(XmlConfigurationPropertyNullableDouble pro)
        {
            var val = pro?.Value;
            val.ThrowIfNull();

            return val.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            if (valueStr.IsWhitespaces()) { return null; }

            return double.Parse(valueStr);
        }
    }
}
