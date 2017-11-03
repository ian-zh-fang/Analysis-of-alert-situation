
/*
 * guid: $GUID$
 * file: IContrainer
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:12:23
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
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// 定义一种容器的基础功能
    /// </summary>
    /// <typeparam name="TElement">元素类型</typeparam>
    /// <typeparam name="TEnumerator">枚举类型</typeparam>
    public interface IContrainer<TElement, TEnumerator>
        where TElement : IEqualityDependancy<TElement>, IEquatable<TElement>, IComparable<TElement>
        where TEnumerator : IEnumerator
    {
        /// <summary>
        /// 将指定的元素添加到容器中
        /// </summary>
        /// <param name="e">需要添加的元素</param>
        /// <exception cref="ArgumentNullException">e 是 null</exception>
        void Add(TElement e);

        /// <summary>
        /// 将指定的元素从容器中移除
        /// </summary>
        /// <param name="e">需要移除的元素</param>
        /// <exception cref="ArgumentNullException">e 是 null</exception>
        void Remove(TElement e);

        /// <summary>
        /// 移除容器中所有的元素
        /// </summary>
        void Clear();

        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回集合中所有匹配的元素
        /// </summary>
        /// <param name="predicate">用于测试每个元素是否满足条件的函数</param>
        /// <returns>若集合中没有匹配元素，返回一个长度为 0 的数组</returns>
        /// <exception cref="ArgumentNullException">predicate 是 null</exception>
        IEnumerable<TElement> FindAll(Func<TElement, Boolean> predicate);

        /// <summary>
        /// 获取容器内元素的数量
        /// </summary>
        Int32 Count();

        /// <summary>
        /// 获取循环访问 IContrainer&lt;TElement, TEnumerator> 的枚举
        /// </summary>
        /// <returns>返回循环访问 IContrainer&lt;TElement, TEnumerator> 的枚举数</returns>
        TEnumerator GetEnumerator();
    }
}
