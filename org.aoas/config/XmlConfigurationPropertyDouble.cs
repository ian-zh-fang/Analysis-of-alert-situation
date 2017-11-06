
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyDouble
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:25:30
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

    public sealed class XmlConfigurationPropertyDouble
        :XmlConfigurationProperty<Double>
    {
        public XmlConfigurationPropertyDouble()
            :base()
        { }

        public XmlConfigurationPropertyDouble(double v)
            :base(v)
        { }

        public static implicit operator XmlConfigurationPropertyDouble(double v)
        {
            return new XmlConfigurationPropertyDouble(v);
        }

        public static implicit operator double(XmlConfigurationPropertyDouble pro)
        {
            pro.ThrowIfNull();
            return pro.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            return double.Parse(valueStr);
        }
    }
}
