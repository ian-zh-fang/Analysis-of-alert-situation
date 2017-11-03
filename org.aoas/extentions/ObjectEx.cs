
/*
 * guid: $GUID$
 * file: ObjectEx
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/1 13:46:17
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

    public static class ObjectEx
    {
        /// <summary>
        /// 目标对象不存在时，返回 true ; 否则，返回 false .
        /// </summary>
        public static Boolean IsNull(this Object input)
        {
            return Object.Equals(input, null);
        }

        /// <summary>
        /// 当前值是否大于指定的最小值，在当前值存在，并且大于最小值时，返回 true；否则，返回 false
        /// </summary>
        /// <typeparam name="TSource">当前值类型，一种可比较的类型</typeparam>
        /// <param name="source">当前值</param>
        /// <param name="minVal">最小值</param>
        /// <returns></returns>
        public static Boolean IsMoreThen<TSource>(this TSource source, TSource minVal)
            where TSource : IComparable<TSource>
        {
            if (source.IsNull()) return false;

            return 0 < source.CompareTo(minVal);
        }

        /// <summary>
        /// 当前值是否小于指定的最大值，在当前值存在，并且小于最大值时，返回 true；否则，返回 false
        /// </summary>
        /// <typeparam name="TSource">当前值类型，一种可比较的类型</typeparam>
        /// <param name="source">当前值</param>
        /// <param name="maxVal">最大值</param>
        /// <returns></returns>
        public static Boolean IsLessThen<TSource>(this TSource source, TSource maxVal)
            where TSource : IComparable<TSource>
        {
            if (source.IsNull()) return false;
            return 0 > source.CompareTo(maxVal);
        }

        /// <summary>
        /// 当前值是否大于或者等于指定的最小值，在当前值存在，并且大于或者等于最小值时，返回 true；否则，返回 false
        /// </summary>
        /// <typeparam name="TSource">当前值类型，一种可比较的类型</typeparam>
        /// <param name="source">当前值</param>
        /// <param name="minVal">最小值</param>
        /// <returns></returns>
        public static Boolean IsMoreThenOrEqual<TSource>(this TSource source, TSource minVal)
            where TSource : IComparable<TSource>
        {
            if (source.IsNull()) return false;
            return 0 <= source.CompareTo(minVal);
        }

        /// <summary>
        /// 当前值是否小于或者等于指定的最大值，在当前值存在，并且小于或者等于最大值时，返回 true；否则，返回 false
        /// </summary>
        /// <typeparam name="TSource">当前值类型，一种可比较的类型</typeparam>
        /// <param name="source">当前值</param>
        /// <param name="maxVal">最大值</param>
        /// <returns></returns>
        public static Boolean IsLessThenOrEqual<TSource>(this TSource source, TSource maxVal)
            where TSource : IComparable<TSource>
        {
            if (source.IsNull()) return false;
            return 0 >= source.CompareTo(maxVal);
        }
    }
}
