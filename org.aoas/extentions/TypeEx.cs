
/*
 * guid: $GUID$
 * file: TypeEx
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/1 13:46:57
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

    public static class TypeEx
    {
        /// <summary>
        /// 验证当前类型是否是目标类型的子类或者实现类，满足一下任何条件，返回 true；否则，返回 false。
        /// <para>1，source 与 baseClsOrInterface 是同样的类型 </para>
        /// <para>2，source 是从 baseClsOrInterface 直接或者间接派生的 </para>
        /// <para>3，baseClsOrInterface 是一个接口实例，并且 source 是 baseClsOrInterface 的一个实现 </para>
        /// <para>4，source 是一个泛型参数，并且 baseClsOrInterface 是 source 的约束之一</para>
        /// </summary>
        /// <param name="source">当前类型</param>
        /// <param name="baseClsOrInterface">目标类型</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static bool IsSubclassOrInterfaceImpl(this Type source, Type baseClsOrInterface)
        {
            source.ThrowIfNull();
            baseClsOrInterface.ThrowIfNull();

            return
                source.IsClass &&
                baseClsOrInterface.IsAssignableFrom(source);
        }

        /// <summary>
        /// 验证当前类型是否是目标类型的子类或者实现类，满足一下任何条件，返回 true；否则，返回 false。
        /// <para>1，source 与 baseClsOrInterface 是同样的类型 </para>
        /// <para>2，source 是从 baseClsOrInterface 直接或者间接派生的 </para>
        /// <para>3，baseClsOrInterface 是一个接口实例，并且 source 是 baseClsOrInterface 的一个实现 </para>
        /// <para>4，source 是一个泛型参数，并且 baseClsOrInterface 是 source 的约束之一</para>
        /// </summary>
        /// <typeparam name="TBaseClsOrInterface">目标类型</typeparam>
        /// <param name="source">当前类型</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static bool IsSubclassOrInterfaceImpl<TBaseClsOrInterface>(this Type source)
        {
            return source.IsSubclassOrInterfaceImpl(typeof(TBaseClsOrInterface));
        }

        /// <summary>
        /// 当前对象是否指定类型或者子类型的实例
        /// </summary>
        /// <typeparam name="TSource">当前对象类型</typeparam>
        /// <typeparam name="TType">目标类型</typeparam>
        /// <param name="source"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static bool Is<TSource, TType>(this TSource source)
        {
            return source.Is(typeof(TType));
        }

        /// <summary>
        /// 当前对象是否指定类型或者子类型的实例，
        /// 若当前实例是 null，则比较 TSource 类型。
        /// </summary>
        /// <typeparam name="TSource">当前对象类型</typeparam>
        /// <param name="source"></param>
        /// <param name="target">目标类型</param>
        /// <returns></returns>
        public static bool Is<TSource>(this TSource source, Type target)
        {
            if (source.IsNull())
            {
                return typeof(TSource).IsSubclassOrInterfaceImpl(target);
            }

            var type = (source as Type) ?? source.GetType();
            return type.IsSubclassOrInterfaceImpl(target);
        }

        /// <summary>
        /// 验证当前类型是否类型 System.Nullable&lt;T> 的实例
        /// </summary>
        /// <param name="source"></param>
        /// <exception cref="ArgumentNullException">source 是 null</exception>
        public static bool IsNullable(this Type source)
        {
            source.ThrowIfNull();

            return
                source.IsClass &&
                source.IsGenericType &&
                source.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
