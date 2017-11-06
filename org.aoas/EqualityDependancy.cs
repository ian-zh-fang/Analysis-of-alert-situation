
/*
 * guid: $GUID$
 * file: EqualityDependancy
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:10:37
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

    /// <summary>
    /// 定义对象相等依赖的基础处理，指定的对象应该派生至 EqualityDependancy 。
    /// </summary>
    /// <typeparam name="TSource">源对象类型</typeparam>
    public abstract class EqualityDependancy<TSource>
        : IEqualityDependancy<TSource>
        , IEquatable<TSource>
        , IComparable<TSource>
        where TSource : EqualityDependancy<TSource>
    {
        protected EqualityDependancy() { }

        public Int32 CompareTo(TSource other)
        {
            other.ThrowIfNull();
            other.GetType().ThrowIfNotType(this.GetType());

            return Compare(other);
        }

        Boolean IEquatable<TSource>.Equals(TSource other)
        {
            if (other.IsNull()) { return false; }
            if (other.GetType().IsSubclassOrInterfaceImpl(this.GetType())) { return Equals(other); }

            return false;
        }

        public sealed override bool Equals(object obj)
        {
            var oth = obj as TSource;
            return ((IEquatable<TSource>)this).Equals(oth);
        }

        public sealed override int GetHashCode()
        {
            return GetHash();
        }

        public static Boolean operator ==(EqualityDependancy<TSource> left, EqualityDependancy<TSource> right)
        {
            var nol = left.IsNull();
            if (nol && right.IsNull()) { return true; }
            if (nol) { return false; }

            return left.Equals(right);
        }

        public static Boolean operator !=(EqualityDependancy<TSource> left, EqualityDependancy<TSource> right)
        {
            return !(left == right);
        }

        public static bool operator >=(EqualityDependancy<TSource> left, EqualityDependancy<TSource> right)
        {
            return 0 <= left.CompareTo(right as TSource);
        }

        public static bool operator <=(EqualityDependancy<TSource> left, EqualityDependancy<TSource> right)
        {
            return 0 >= left.CompareTo(right as TSource);
        }

        // 获取当前对象的 HashCode
        protected abstract Int32 GetHash();

        // 比较当前对量是否与指定的对象相等，若相等，返回 true ；否则，返回 false 。
        protected abstract Boolean Equals(TSource other);

        // 比较当前对象与指定对象，小于 0 标识当前对象小于指定的对象；等于 0 标识当前对象等于指定的对象；大于 0 标识当前对象大于指定的对象
        protected abstract Int32 Compare(TSource other);
    }
}
