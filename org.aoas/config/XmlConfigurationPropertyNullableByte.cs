﻿
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyNullableByte
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:38:03
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

    public sealed class XmlConfigurationPropertyNullableByte
        :XmlConfigurationProperty<Byte?>
    {
        public XmlConfigurationPropertyNullableByte()
            :base()
        { }

        public XmlConfigurationPropertyNullableByte(byte? value)
            :base(value)
        { }

        public static implicit operator XmlConfigurationPropertyNullableByte(byte? value)
        {
            return new XmlConfigurationPropertyNullableByte(value);
        }

        public static implicit operator XmlConfigurationPropertyNullableByte(byte value)
        {
            return value;
        }

        public static implicit operator byte?(XmlConfigurationPropertyNullableByte pro)
        {
            return pro?.Value;
        }

        public static implicit operator byte(XmlConfigurationPropertyNullableByte pro)
        {
            var val = pro?.Value;
            val.ThrowIfNull();

            return val.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            if (valueStr.IsWhitespaces()) { return null; }

            return byte.Parse(valueStr);
        }
    }
}
