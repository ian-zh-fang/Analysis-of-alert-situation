
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyChar
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:24:55
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

    public sealed class XmlConfigurationPropertyChar:XmlConfigurationProperty<Char>
    {
        public XmlConfigurationPropertyChar()
            :base()
        { }

        public XmlConfigurationPropertyChar(char value)
            : base(value)
        { }

        public static implicit operator XmlConfigurationPropertyChar(char value)
        {
            return new XmlConfigurationPropertyChar(value);
        }

        public static implicit operator char(XmlConfigurationPropertyChar pro)
        {
            pro.ThrowIfNull();
            return pro.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            return char.Parse(valueStr);
        }
    }
}
