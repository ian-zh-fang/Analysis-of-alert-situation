
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

        protected override object GetValue(string valueStr, Type valueType)
        {
            return bool.Parse(valueStr);
        }
    }
}
