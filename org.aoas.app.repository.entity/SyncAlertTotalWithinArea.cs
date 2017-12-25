
/*
 * guid: $GUID$
 * file: SyncCaseClassesStatistics
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 14:53:28
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
    /// 区域分类案件统计
    /// </summary>
    public class CaseClassesStatistics : BaseEntityKeyString
    {
        /// <summary>
        /// 案件类型ID
        /// </summary>
        public string ClassesId { get; set; }

        /// <summary>
        /// 行政区划ID
        /// </summary>
        public string OrgId { get; set; }

        /// <summary>
        /// 案发数量
        /// </summary>
        public int CaseCount { get; set; } = 0;

        /// <summary>
        /// 统计时间
        /// </summary>
        public long TotalDate { get; set; } = DateTime.Now.ToUnixTimeSecond();

        protected override KeyContext Context => KeyContext.TableAlertTotalForArea;
    }
}
