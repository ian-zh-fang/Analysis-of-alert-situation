
/*
 * guid: $GUID$
 * file: AreaInAlert
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/18 14:51:19
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

    /// <summary>
    /// 区域内警情数量统计
    /// </summary>
    public sealed class AlertTotalWithinArea:BaseEntityKeyInt64
    {
        /// <summary>
        /// 辖区标识
        /// </summary>
        public long AreaId { get; set; }

        /// <summary>
        /// 警情分类标识
        /// </summary>
        public long AlertId { get; set; }
        
        /// <summary>
        /// 警情数量
        /// </summary>
        public uint Count { get; set; }

        /// <summary>
        /// 当前记录创建时间
        /// </summary>
        public long CTime { get; set; }

        /// <summary>
        /// 当前记录最后更新时间
        /// </summary>
        public long UTime { get; set; }

        /// <summary>
        /// 同步主键 ID
        /// </summary>
        public string SyncId { get; set; }

        protected override KeyContext Context => KeyContext.TableAlertTotalForArea;
    }
}
