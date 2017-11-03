
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyNullableBoolean
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:31:21
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

    public sealed class XmlConfigurationPropertyNullableBoolean
        :XmlConfigurationProperty<Boolean?>
    {
        public XmlConfigurationPropertyNullableBoolean()
            :base()
        { }

        public XmlConfigurationPropertyNullableBoolean(bool? value)
            :base(value)
        { }

        protected override object GetValue(string valueStr, Type valueType)
        {
            if (valueStr.IsWhitespaces()) { return null; }

            return bool.Parse(valueStr);
        }
    }
}
