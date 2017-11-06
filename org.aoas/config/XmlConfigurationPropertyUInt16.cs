
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyUInt16
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:26:58
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

    public sealed class XmlConfigurationPropertyUInt16
        :XmlConfigurationProperty<UInt16>
    {
        public XmlConfigurationPropertyUInt16()
            :base()
        { }

        public XmlConfigurationPropertyUInt16(ushort v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertyUInt16(ushort v)
        {
            return new XmlConfigurationPropertyUInt16(v);
        }

        public static implicit operator ushort(XmlConfigurationPropertyUInt16 pro)
        {
            pro.ThrowIfNull();
            return pro.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            return ushort.Parse(valueStr);
        }
    }
}
