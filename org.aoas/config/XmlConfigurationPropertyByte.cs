
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyByte
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:24:43
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

    public sealed class XmlConfigurationPropertyByte
        : XmlConfigurationProperty<Byte>
    {
        public XmlConfigurationPropertyByte()
            :base()
        { }

        public XmlConfigurationPropertyByte(byte value)
            :base(value)
        { }

        protected override object GetValue(string valueStr, Type valueType)
        {
            return byte.Parse(valueStr);
        }
    }
}
