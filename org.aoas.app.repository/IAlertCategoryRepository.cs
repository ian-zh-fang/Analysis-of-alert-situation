
/*
 * guid: $GUID$
 * file: IAlertCategoryRepository
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 16:35:49
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
    /// 旨在声明一组警情类型数据仓储业务功能
    /// </summary>
    public interface IAlertCategoryRepository : IRepository<entity.AlertCategory> { }

    /// <summary>
    /// 旨在声明一组同步警情类型数据仓储业务功能
    /// </summary>
    public interface ISyncAlertCategoryRepository : IRepository<entity.CaseClasses> { }
}
