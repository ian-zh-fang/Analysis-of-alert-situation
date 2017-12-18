
/*
 * guid: $GUID$
 * file: BaseEntity
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/15 17:39:10
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

namespace org.aoas.app.repository.entity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    /// <summary>
    /// 旨在定义基础实体数据结构
    /// </summary>
    /// <typeparam name="TKey">主键类型，通常应该是基元类型。如：int16,int32,int64 ...</typeparam>
    public abstract class BaseEntity<TKey>
        where TKey : struct, IComparable, IFormattable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        public TKey Id { get; set; }
    }

    /// <summary>
    /// 旨在定义需要实现自增长主键的实体数据结构
    /// </summary>
    /// <typeparam name="TKey">主键类型，通常应该是基元类型。如：int16,int32,int64 ...</typeparam>
    public abstract class IdentityEntity<TKey>:BaseEntity<TKey>
        where TKey : struct, IComparable, IFormattable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    {
        /// <summary>
        /// 自增长主键标识
        /// </summary>
        [Key(), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new TKey Id { get; set; }
    }

    /// <summary>
    /// Int32 类型自增长主键的基础实体数据结构
    /// </summary>
    public abstract class Int32IdentityEntity : IdentityEntity<int> { }
}
