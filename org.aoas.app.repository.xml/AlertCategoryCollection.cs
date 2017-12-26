
/*
 * guid: $GUID$
 * file: AlertCategoryCollection
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/25 17:03:17
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

namespace org.aoas.app.repository.xml
{
    using System;
    using System.Xml;

    /// <summary>
    /// 警情类型数据对象结构实现
    /// </summary>
    internal sealed class AlertCategoryCollection : ParentEntityCollection<Int64, AlertCategoryCollection>
    {
        /// <summary>
        /// 创建 <see cref="AlertCategoryCollection"/> 警情类型数据对象集合实例
        /// </summary>
        public AlertCategoryCollection() : base() { }

        protected override AlertCategoryCollection OnGetChildElement(XmlReader reader)
        {
            return new AlertCategoryCollection();
        }

        public static explicit operator AlertCategoryCollection(entity.AlertCategory category)
        {
            return new AlertCategoryCollection
            {
                Id = category.Id,
                Pid = category.Pid,
                SyncId = category.SyncId,
                Name = category.Name,
                Code = category.Code,
                Desc = category.Desc,
                IsDel = category.IsDel
            };
        }

        public static explicit operator entity.AlertCategory(AlertCategoryCollection category)
        {
            return new entity.AlertCategory
            {
                Code = category.Code,
                Desc = category.Desc,
                Id = category.Id,
                IsDel = category.IsDel,
                Name = category.Name,
                Pid = category.Pid,
                SyncId = category.SyncId
            };
        }

        [attributes.Alias("code")]
        public string Code { get; set; }

        [attributes.Alias("name")]
        public string Name { get; set; }

        [attributes.Alias("desc")]
        public string Desc { get; set; }

        [attributes.Alias("isDel")]
        public byte IsDel { get; set; }

        [attributes.Alias("syncId")]
        public string SyncId { get; set; }
    }
}
