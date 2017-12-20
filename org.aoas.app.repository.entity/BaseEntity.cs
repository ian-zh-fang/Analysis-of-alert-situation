
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
    using org.aoas.app.common;

    /// <summary>
    /// 旨在定义基础实体数据结构
    /// </summary>
    /// <typeparam name="TKey">主键类型，通常应该是基元类型。如：int16,int32,int64 ...</typeparam>
    public abstract class BaseEntity<TKey>
        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    {
        /// <summary>
        /// 创建 <see cref="BaseEntity{TKey}"/> 的新实例，这是一种受保护机制的构造函数
        /// </summary>
        protected BaseEntity()
        {
            Context.ThrowIfNull(nameof(Context));
            Id = NewKey(Context);
        }

        /// <summary>
        /// 主键标识
        /// </summary>
        [Key()]
        public TKey Id { get; set; }

        /// <summary>
        /// 当前实体上下文
        /// </summary>
        protected abstract KeyContext Context { get; }

        /// <summary>
        /// 创建一个新的 Key 值，并返回一个新建的 Key
        /// </summary>
        /// <returns></returns>    dccddc  c  
        protected abstract TKey NewKey(KeyContext context);
    }

    /// <summary>
    /// 基于 String 类型的 Key
    /// </summary>
    public abstract class BaseEntityKeyString:BaseEntity<String>
    {
        protected override string NewKey(KeyContext context)
        {
            var timestamp = DateTime.Now.ToUnixTimeSecond();
            var code = (ushort)context.Code;
            var random = CaptchaGenerator.Default.GenerateNumberCaptcha();

            var key = string.Concat(timestamp, code, random);
            return key;
        }
    }

    /// <summary>
    /// 基于 Int64 类型的 Key
    /// </summary>
    public abstract class BaseEntityKeyInt64:BaseEntity<Int64>
    {
        protected override long NewKey(KeyContext context)
        {
            var timestamp = DateTime.Now.ToUnixTimeSecond();
            var code = (ushort)context.Code;
            var random = CaptchaGenerator.Default.GenerateNumberCaptcha();

            var keyStr = string.Concat(timestamp, code, random);
            var key = long.Parse(keyStr);

            return key;
        }
    }
}
