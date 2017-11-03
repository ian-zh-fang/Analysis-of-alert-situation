
/*
 * guid: $GUID$
 * file: EnumDependancy
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:09:53
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

namespace org.aoas
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;

    /// <summary>
    /// 旨在定义一种可枚举类型的基础结构
    /// </summary>
    /// <typeparam name="TEnum">内部枚举值类型</typeparam>
    public abstract class EnumDependancy<TEnum>
        : EqualityDependancy<EnumDependancy<TEnum>>
        where TEnum : IEquatable<TEnum>, IComparable<TEnum>
    {
        /// <summary>
        /// 一种收保护机制的构造函数
        /// </summary>
        /// <param name="value">枚举值</param>
        protected EnumDependancy(TEnum value)
            : base()
        {
            OnCheck(value);
            Value = value;
        }

        /// <summary>
        /// 指定值的有效性验证
        /// </summary>
        /// <param name="value">需要验证的值</param>
        protected virtual void OnCheck(TEnum value) { }

        protected override int GetHash()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// 枚举值
        /// </summary>
        public TEnum Value { get; private set; }
    }

    /// <summary>
    /// 旨在定义一种可枚举类型的基础结构关系映射
    /// </summary>
    /// <typeparam name="TEnum">内部枚举值类型</typeparam>
    /// <typeparam name="TValue">枚举关系映射类型</typeparam>
    public abstract class EnumDependancy<TEnum, TValue>
        : EnumDependancy<TEnum>
        where TValue : EnumDependancy<TEnum>
        where TEnum : IEquatable<TEnum>, IComparable<TEnum>
    {
        // 线程安全同步锁
        private static readonly object _SyncLocker = new object();

        // 线程安全的枚举值映射对象集合
        private static ConcurrentBag<TValue> _enumValues;

        /// <summary>
        /// 一种收保护机制的构造函数
        /// </summary>
        /// <param name="value">枚举值</param>
        protected EnumDependancy(TEnum value)
            : base(value)
        { }

        /// <summary>
        /// 从枚举值映射对象集合中查询和指定枚举值匹配的枚举上下文实例，并返回匹配的枚举上下文实例。若匹配失败，返回 null。
        /// </summary>
        /// <param name="value">需要匹配的枚举值</param>
        /// <returns></returns>
        protected static TValue GetEnum(TEnum value)
        {
            return EnumValues.FirstOrDefault(t => t.Value.Equals(value));
        }

        // 初始化枚举值映射对象集合
        private static ConcurrentBag<TValue> InitEnum()
        {
            if (_enumValues.IsNull())
            {
                lock (_SyncLocker)
                {
                    if (_enumValues.IsNull())
                    {
                        TValue[] arr;
                        try
                        {
                            arr =
                                typeof(TValue).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                                .Select(t => t.GetValue(null) as TValue)
                                .Where(t => !t.IsNull())
                                .ToArray();
                        }
                        catch (Exception) { arr = new TValue[0]; }

                        _enumValues = new ConcurrentBag<TValue>(arr);
                    }
                }
            }

            return _enumValues;
        }

        /// <summary>
        /// 可枚举值映射对象集合
        /// </summary>
        protected static ConcurrentBag<TValue> EnumValues
        {
            get
            {
                var arr = InitEnum();
                return arr;
            }
        }
    }
}
