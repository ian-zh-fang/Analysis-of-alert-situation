
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyBoolean
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:24:27
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

    public sealed class XmlConfigurationPropertyBoolean
        : XmlConfigurationProperty<Boolean>
    {
        public XmlConfigurationPropertyBoolean()
            :base()
        { }

        public XmlConfigurationPropertyBoolean(bool value)
            :base(value)
        { }

        public static implicit operator XmlConfigurationPropertyBoolean(bool value)
        {
            return new XmlConfigurationPropertyBoolean(value);
        }

        public static implicit operator bool(XmlConfigurationPropertyBoolean pro)
        {
            pro.ThrowIfNull();
            return pro.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            return bool.Parse(valueStr);
        }
    }
}
