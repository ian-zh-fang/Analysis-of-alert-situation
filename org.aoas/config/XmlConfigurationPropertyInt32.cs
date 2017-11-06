
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyInt32
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:25:54
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

    public sealed class XmlConfigurationPropertyInt32
        :XmlConfigurationProperty<Int32>
    {
        public XmlConfigurationPropertyInt32()
            :base()
        { }

        public XmlConfigurationPropertyInt32(int value)
            :base(value)
        { }

        public static implicit operator XmlConfigurationPropertyInt32(int value)
        {
            return new XmlConfigurationPropertyInt32(value);
        }

        public static implicit operator int(XmlConfigurationPropertyInt32 pro)
        {
            pro.ThrowIfNull(nameof(pro));
            return pro.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            return int.Parse(valueStr);
        }
    }
}
