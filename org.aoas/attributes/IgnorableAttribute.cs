
/*
 * guid: $GUID$
 * file: IgnorableAttribute
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:15:38
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

namespace org.aoas.attributes
{
    using System;
    using System.Text.RegularExpressions;

    public interface IIgnorable
    {
        // 是否忽略指定的值
        //  value：待忽略的值
        //  valueType：待忽略的值类型
        Boolean IsIgnore(Object value, Type valueType);

        // 排序处理值
        //  R1，正序排列，小值优先
        //  R2，当前属性通常用来处理需要顺序处理的特性
        Int32 Order { get; }
    }

    // 可忽略属性值
    //  R1，在属性值为指定的值时，允许忽略
    //  R2，若指定目标类型时，优先判定是否类型匹配；若目标类型匹配失败，忽略规则无效
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class IgnorableAttribute : Attribute, IIgnorable
    {
        private readonly Object _ignore;
        private readonly Type _targetType;

        // 创建 IgnorableAttribute 的新实例
        //  ignore 可忽略的值
        //  targetType 目标值类型，null 标识匹配所有类型
        public IgnorableAttribute(Object ignore, Type targetType = null)
        {
            _ignore = ignore;
            _targetType = targetType;
            Order = 0;
        }

        // 是否忽略指定的值。true 标识忽略；否则，false
        //  value：指定的值
        public Boolean IsIgnore(Object value, Type valueType)
        {
            // 是否指定目标类型
            if (_targetType.IsNull())
            {
                return Verify(value, _ignore);
            }

            // 目标类型匹配
            if (Type.Equals(_targetType, valueType))
            {
                return Verify(value, _ignore);
            }

            // 其他情况，返回 false
            return false;
        }

        // 忽略值确认。true 忽略当前值，否则，返回 false
        protected virtual Boolean Verify(Object value, Object ignore)
        {
            return Object.Equals(value, ignore);
        }

        // 排序处理值
        //  R1，正序排列，小值优先
        //  R2，当前属性通常用来处理需要顺序处理的特性
        public Int32 Order { get; set; }
    }

    // 忽略值为 null 的属性
    //  R1，当前特性所有类型的值有效
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class IgnorableNullAttribute : IgnorableAttribute
    {
        public IgnorableNullAttribute()
            : base(null)
        { }
    }

    // 忽略字符串的值为 null，或者长度为 0 时的值
    //  R1，当前特性对 String 类型的值有效
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class IgnorableIsNullOrEmptyAttribute : IgnorableAttribute
    {
        public IgnorableIsNullOrEmptyAttribute()
            : base(null, typeof(String))
        { }

        // 忽略字符串的值为 null，或者长度为 0 时，返回 true，否则，返回 false。
        protected override bool Verify(object value, object ignore)
        {
            return ((String)value).IsEmpty();
        }
    }

    // 忽略匹配指定正则表达式的值
    //  R1，当前特性对 String 类型的值有效
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IgnorableRegexMatchAttribute : IgnorableAttribute
    {
        public IgnorableRegexMatchAttribute(String pattern)
            : base(new Regex(pattern), typeof(String))
        { }

        // 正则表达式无效时，当前规则无效
        protected override Boolean Verify(Object value, Object ignore)
        {
            if (value.IsNull()) return false;

            var rgx = ignore as Regex;
            if (rgx.IsNull()) return false;

            return rgx.IsMatch((String)value);
        }
    }

    // 忽略全部为空白字符的字符串的值
    //  R1，当前特性对 String 类型的值有效
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class IgnoreWhitespaceAttribute : IgnorableRegexMatchAttribute
    {
        public IgnoreWhitespaceAttribute()
            : base(@"^\s+$")
        { }
    }
}
