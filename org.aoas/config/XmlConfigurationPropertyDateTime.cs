
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyDateTime
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 16:25:07
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

    public sealed class XmlConfigurationPropertyDateTime
        :XmlConfigurationProperty<DateTime>
    {
        private readonly IFormatProvider _provider;

        public XmlConfigurationPropertyDateTime()
            : this(null)
        { }

        public XmlConfigurationPropertyDateTime(IFormatProvider provider)
            :base()
        {
            _provider = provider;
        }

        public XmlConfigurationPropertyDateTime(DateTime value)
            :base(value)
        { }

        public static implicit operator XmlConfigurationPropertyDateTime(DateTime value)
        {
            return new XmlConfigurationPropertyDateTime(value);
        }

        public static implicit operator DateTime(XmlConfigurationPropertyDateTime pro)
        {
            pro.ThrowIfNull();
            return pro.Value;
        }

        protected override object GetValue(string valueStr, Type valueType)
        {
            if (_provider.IsNull()) { return DateTime.Parse(valueStr); }

            return DateTime.Parse(valueStr, _provider);
        }
    }
}
