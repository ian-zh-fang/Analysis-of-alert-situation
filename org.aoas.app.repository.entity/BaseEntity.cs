
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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    /// <summary>
    /// 旨在定义基础实体数据结构
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class BaseEntity<TKey>
    {
        /// <summary>
        /// 自增长主键标识
        /// </summary>
        [Key(), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }
    }
}
