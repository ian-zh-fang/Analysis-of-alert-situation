
/*
 * guid: $GUID$
 * file: IUnitWork
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/18 16:20:34
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
    /// 旨在定义一种工作单元隔离功能约定
    /// </summary>
    public interface IUnitWork
    {
        /// <summary>
        /// 保存当前工作单元的更改记录，返回受影响的总记录数
        /// </summary>
        /// <returns></returns>
        int Save();
    }
}
