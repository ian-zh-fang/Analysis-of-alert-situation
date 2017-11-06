
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertySByte
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:26:18
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

    public sealed class XmlConfigurationPropertySByte
        :XmlConfigurationProperty<SByte>
    {
        public XmlConfigurationPropertySByte()
            :base()
        { }

        public XmlConfigurationPropertySByte(sbyte v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertySByte(sbyte v)
        {
            return new XmlConfigurationPropertySByte(v);
        }

        public static implicit operator sbyte(XmlConfigurationPropertySByte pro)
        {
            pro.ThrowIfNull();

            return pro.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            return sbyte.Parse(valueStr);
        }
    }
}
