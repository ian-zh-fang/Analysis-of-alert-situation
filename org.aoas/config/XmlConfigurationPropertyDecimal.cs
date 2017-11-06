
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyDecimal
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:25:20
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

    public sealed class XmlConfigurationPropertyDecimal
        :XmlConfigurationProperty<Decimal>
    {
        public XmlConfigurationPropertyDecimal()
            :base()
        { }

        public XmlConfigurationPropertyDecimal(decimal value)
            :base(value)
        { }

        public static implicit operator XmlConfigurationPropertyDecimal(decimal value)
        {
            return new XmlConfigurationPropertyDecimal(value);
        }

        public static implicit operator decimal(XmlConfigurationPropertyDecimal pro)
        {
            pro.ThrowIfNull();
            return pro.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            return decimal.Parse(valueStr);
        }
    }
}
