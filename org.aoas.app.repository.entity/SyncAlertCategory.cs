
/*
 * guid: $GUID$
 * file: SyncCaseClasses
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 14:52:38
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
    /// <summary>
    /// 案件类型
    /// </summary>
    public sealed class CaseClasses : BaseEntityKeyString
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 预警阈值
        /// </summary>
        public int ThdVal { get; set; }

        /// <summary>
        /// 移除标识，true 标识已移除
        /// </summary>
        public short IsDel { get; set; }

        /// <summary>
        /// 父级主键标识
        /// </summary>
        public string ParentId { get; set; }

        protected override KeyContext Context => KeyContext.TableAlertCategory;
    }
}
