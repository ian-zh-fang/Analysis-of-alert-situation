
/*
 * guid: $GUID$
 * file: ThrowEx
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/1 13:48:30
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

    public static class ThrowEx
    {
        /// <summary>
        /// 抛出 System.ArgumentNullException 的实例
        /// </summary>
        /// <param name="sourceName">参数名称</param>
        private static void ThrowNullException(string sourceName)
        {
            if (sourceName.IsWhitespaces()) { throw new ArgumentNullException(); }

            throw new ArgumentNullException(sourceName);
        }

        /// <summary>
        /// 如果当前实例是 null，抛出异常
        /// </summary>
        /// <typeparam name="TSource">当前实例类型</typeparam>
        /// <param name="source">当前实例</param>
        /// <param name="sourceName">当前实例名称</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ThrowIfNull<TSource>(this TSource source, string sourceName = null)
        {
            if (source.IsNull()) { ThrowNullException(sourceName); }
        }

        /// <summary>
        /// 若当前字符串是 null，抛出异常
        /// </summary>
        /// <param name="source">当前实例</param>
        /// <param name="sourceName">当前实例名称</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ThrowIfNull(this string source, string sourceName = null)
        {
            source.ThrowIfNull<string>(sourceName);
        }

        /// <summary>
        /// 若当前字符串是 null，或者是 String.Empty，抛出异常
        /// </summary>
        /// <param name="source">当前实例</param>
        /// <param name="sourceName">当前实例名称</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ThrowIfEmpty(this string source, string sourceName = null)
        {
            if (source.IsEmpty()) { ThrowNullException(sourceName); }
        }

        /// <summary>
        /// 若当前字符串是 null，或者是 String.Empty，或者由空白字符组成，抛出异常
        /// </summary>
        /// <param name="source">当前实例</param>
        /// <param name="sourceName">当前实例名称</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ThrowIfWhitespace(this string source, string sourceName = null)
        {
            if (source.IsWhitespaces()) { ThrowNullException(sourceName); }
        }
        
        /// <summary>
        /// 若当前实例非指定类型或者派生类型实例，抛出异常
        /// </summary>
        /// <typeparam name="TSource">当前实力类型</typeparam>
        /// <param name="source">当前实例</param>
        /// <param name="target">目标类型</param>
        public static void ThrowIfNotType<TSource>(this TSource source, Type target)
        {
            if (source.Is(target)) { return; }
            throw new NotSupportedException();
        }

        /// <summary>
        /// 若当前实例非指定类型或者派生类型实例，抛出异常
        /// </summary>
        /// <typeparam name="TSource">当前实例类型</typeparam>
        /// <typeparam name="TType">目标类型</typeparam>
        /// <param name="source">当前实例</param>
        public static void ThrowIfNotType<TSource, TType>(this TSource source)
        {
            source.ThrowIfNotType(typeof(TType));
        }

        /// <summary>
        /// 若当前值大于指定的最小值，抛出异常
        /// </summary>
        /// <typeparam name="TSource">当前值类型，一种可比较的类型</typeparam>
        /// <param name="source">当前值</param>
        /// <param name="minVal">最小值</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static void ThrowIfMoreThen<TSource>(this TSource source, TSource minVal)
            where TSource : IComparable<TSource>
        {
            source.ThrowIfNull();
            minVal.ThrowIfNull();
            if (source.IsMoreThen<TSource>(minVal))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 若当前值大于或者等于指定的最小值，抛出异常
        /// </summary>
        /// <typeparam name="TSource">当前值类型，一种可比较的类型</typeparam>
        /// <param name="source">当前值</param>
        /// <param name="minVal">最小值</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static void ThrowIfMoreThenOrEqual<TSource>(this TSource source, TSource minVal)
            where TSource : IComparable<TSource>
        {
            source.ThrowIfNull();
            minVal.ThrowIfNull();
            if (source.IsMoreThenOrEqual<TSource>(minVal))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 若当前值小于指定的最大值，抛出异常
        /// </summary>
        /// <typeparam name="TSource">当前值类型，一种可比较的类型</typeparam>
        /// <param name="source">当前值</param>
        /// <param name="maxVal">最大值</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static void ThrowIfLessThen<TSource>(this TSource source, TSource maxVal)
            where TSource : IComparable<TSource>
        {
            source.ThrowIfNull();
            maxVal.ThrowIfNull();
            if (source.IsLessThen<TSource>(maxVal))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 若当前值小于或者等于指定的最大值，抛出异常
        /// </summary>
        /// <typeparam name="TSource">当前值类型，一种可比较的类型</typeparam>
        /// <param name="source">当前值</param>
        /// <param name="maxVal">最大值</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static void ThrowIfLessThenOrEqual<TSource>(this TSource source, TSource maxVal)
            where TSource : IComparable<TSource>
        {
            source.ThrowIfNull();
            maxVal.ThrowIfNull();
            if (source.IsLessThenOrEqual<TSource>(maxVal))
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
