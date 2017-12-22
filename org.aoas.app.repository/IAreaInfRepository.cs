
/*
 * guid: $GUID$
 * file: IAreaInfRepository
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 16:36:15
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
    /// 旨在实现一组辖区数据仓储业务功能
    /// </summary>
    public interface IAreaInfRepository : IRepository<entity.AreaInf> { }

    /// <summary>
    /// 旨在实现一组同步辖区数据仓储业务功能
    /// </summary>
    public interface ISyncAreaInfRepository : IRepository<entity.Orgnization> { }
}
