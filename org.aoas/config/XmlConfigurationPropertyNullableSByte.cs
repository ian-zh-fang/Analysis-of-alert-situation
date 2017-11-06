
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyNullableSByte
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/6 11:46:40
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

    public sealed class XmlConfigurationPropertyNullableSByte
        :XmlConfigurationProperty<SByte?>
    {
        public XmlConfigurationPropertyNullableSByte()
            :base()
        { }

        public XmlConfigurationPropertyNullableSByte(sbyte? v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertyNullableSByte(sbyte? v)
        {
            return new XmlConfigurationPropertyNullableSByte(v);
        }

        public static implicit operator XmlConfigurationPropertyNullableSByte(sbyte v)
        {
            return v;
        }

        public static implicit operator sbyte?(XmlConfigurationPropertyNullableSByte pro)
        {
            return pro?.Value;
        }

        public static implicit operator sbyte(XmlConfigurationPropertyNullableSByte pro)
        {
            var val = pro?.Value;
            val.ThrowIfNull();

            return val.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            if (valueStr.IsWhitespaces()) { return null; }

            return sbyte.Parse(valueStr);
        }
    }
}
