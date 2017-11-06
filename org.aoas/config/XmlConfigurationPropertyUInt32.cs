
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyUInt32
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:27:11
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

    public sealed class XmlConfigurationPropertyUInt32
        :XmlConfigurationProperty<UInt32>
    {
        public XmlConfigurationPropertyUInt32()
            :base()
        { }

        public XmlConfigurationPropertyUInt32(uint v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertyUInt32(uint v)
        {
            return new XmlConfigurationPropertyUInt32(v);
        }

        public static implicit operator uint(XmlConfigurationPropertyUInt32 pro)
        {
            pro.ThrowIfNull();
            return pro.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            return uint.Parse(valueStr);
        }
    }
}
