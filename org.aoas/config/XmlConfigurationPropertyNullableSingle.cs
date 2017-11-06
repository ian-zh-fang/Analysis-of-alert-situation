
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyNullableSingle
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/6 11:52:27
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

    public sealed class XmlConfigurationPropertyNullableSingle
        :XmlConfigurationProperty<Single?>
    {
        public XmlConfigurationPropertyNullableSingle()
            :base()
        { }

        public XmlConfigurationPropertyNullableSingle(float? v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertyNullableSingle(float? v)
        {
            return new XmlConfigurationPropertyNullableSingle(v);
        }

        public static implicit operator XmlConfigurationPropertyNullableSingle(float v)
        {
            return v;
        }

        public static implicit operator float?(XmlConfigurationPropertyNullableSingle pro)
        {
            return pro?.Value;
        }

        public static implicit operator float(XmlConfigurationPropertyNullableSingle pro)
        {
            var val = pro?.Value;
            val.ThrowIfNull();

            return val.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            if (valueStr.IsWhitespaces()) { return null; }

            return float.Parse(valueStr);
        }
    }
}
