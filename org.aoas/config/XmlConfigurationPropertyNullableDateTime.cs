
/*
 * guid: $GUID$
 * file: XmlConfigurationPropertyNullableDateTime
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/6 11:09:20
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

    public sealed class XmlConfigurationPropertyNullableDateTime
        :XmlConfigurationProperty<DateTime?>
    {
        private readonly IFormatProvider _provider;

        public XmlConfigurationPropertyNullableDateTime(IFormatProvider provider)
            :base()
        {
            _provider = provider;
        }

        public XmlConfigurationPropertyNullableDateTime()
            : this(provider: null)
        { }

        public XmlConfigurationPropertyNullableDateTime(DateTime? value)
            :base(value)
        { }

        public static implicit operator XmlConfigurationPropertyNullableDateTime(DateTime? value)
        {
            return new XmlConfigurationPropertyNullableDateTime(value);
        }

        public static implicit operator XmlConfigurationPropertyNullableDateTime(DateTime value)
        {
            return value;
        }

        public static implicit operator DateTime?(XmlConfigurationPropertyNullableDateTime pro)
        {
            return pro?.Value;
        }

        public static implicit operator DateTime(XmlConfigurationPropertyNullableDateTime pro)
        {
            var val = pro?.Value;
            val.ThrowIfNull();

            return val.Value;
        }
        protected override object GetValue(string valueStr, Type valueType)
        {
            if (valueStr.IsWhitespaces()) { return null; }

            if (_provider.IsNull()) { return DateTime.Parse(valueStr); }

            return DateTime.Parse(valueStr, _provider);
        }
    }
}
