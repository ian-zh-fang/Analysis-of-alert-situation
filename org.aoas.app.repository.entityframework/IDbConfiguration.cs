
/*
 * guid: $GUID$
 * file: IDbConfiguration
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 17:13:59
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

namespace org.aoas.app.repository.entityframework
{
    using System;

    /// <summary>
    /// 旨在约定一组数据库配置业务功能
    /// </summary>
    public interface IDbConfiguration
    {
        /// <summary>
        /// 数据库链接字符串或者名称
        /// </summary>
        String ConnectionStringOrName { get; }

        /// <summary>
        /// 数据库架构或者所属用户名称
        /// </summary>
        String OwnerOrSchema { get; }
    }
}
