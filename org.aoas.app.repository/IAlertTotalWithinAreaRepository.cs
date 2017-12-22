
/*
 * guid: $GUID$
 * file: IAlertTotalWithinAreaRepository
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 16:36:37
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

namespace org.aoas.app.repository
{
    /// <summary>
    /// 旨在声明一组辖区内警情统计数据仓储业务功能
    /// </summary>
    public interface IAlertTotalWithinAreaRepository :IRepository<entity.AlertTotalWithinArea> { }

    /// <summary>
    /// 旨在声明一组同步辖区内警情统计数据仓储业务功能
    /// </summary>
    public interface ISyncAlertTotalWithinAreaRepository : IRepository<entity.CaseClassesStatistics> { }
}
