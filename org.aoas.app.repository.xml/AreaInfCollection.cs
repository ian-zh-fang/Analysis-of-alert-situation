
/*
 * guid: $GUID$
 * file: AreaInfCollection
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/25 17:03:52
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
    /// 辖区信息数据对象结构实现
    /// </summary>
    internal sealed class AreaInfCollection : ParentEntityCollection<Int64, AreaInfCollection>
    {
        /// <summary>
        /// 创建 <see cref="AreaInfCollection"/> 辖区信息数据实例对象
        /// </summary>
        public AreaInfCollection() : base() { }

        protected override AreaInfCollection OnGetChildElement(XmlReader reader)
        {
            return new AreaInfCollection();
        }

        public static explicit operator AreaInfCollection(entity.AreaInf inf)
        {
            return new AreaInfCollection
            {
                Code = inf.Code,
                Desc = inf.Desc,
                Id = inf.Id,
                IsDel = inf.IsDel,
                Name = inf.Name,
                Pid = inf.Pid,
                SyncId = inf.SyncId
            };
        }

        public static explicit operator entity.AreaInf(AreaInfCollection inf)
        {
            return new entity.AreaInf
            {
                Code = inf.Code,
                Desc = inf.Desc,
                Id = inf.Id,
                IsDel = inf.IsDel,
                Name = inf.Name,
                Pid = inf.Pid,
                SyncId = inf.SyncId
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
