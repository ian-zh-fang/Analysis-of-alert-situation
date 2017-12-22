
/*
 * guid: $GUID$
 * file: IRepository
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/15 16:08:44
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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 旨在约定一种数据仓储功能协议
    /// </summary>
    /// <typeparam name="TEntity">实体数据结构</typeparam>
    public interface IRepository<TEntity>
        : IUnitWork
        , IDisposable
    {
        /// <summary>
        /// 向数据仓库中新增指定的实体数据
        /// </summary>
        /// <param name="entity">指定新增的实体对象</param>
        /// <exception cref="ArgumentNullException">entity is null.</exception>
        /// <exception cref="AggregateException">仓库内部错误</exception>
        /// <returns></returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// 更新数据仓库指定的实体数据
        /// </summary>
        /// <param name="entity">指定更新的实体对象</param>
        /// <exception cref="ArgumentNullException">entity is null.</exception>
        /// <exception cref="AggregateException">仓库内部错误</exception>
        /// <returns></returns>
        TEntity Upgrade(TEntity entity);

        /// <summary>
        /// 移除数据仓库指定的实体数据
        /// </summary>
        /// <param name="entity">指定删除的实体对象</param>
        /// <exception cref="ArgumentNullException">entity is null.</exception>
        /// <exception cref="AggregateException">仓库内部错误</exception>
        /// <returns></returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// 批量移除数据仓库指定条件下的实体数据
        /// </summary>
        /// <param name="predicate">指定筛选条件</param>
        /// <exception cref="ArgumentNullException">predicate is null.</exception>
        /// <exception cref="AggregateException">仓库内部错误</exception>
        /// <returns></returns>
        IEnumerable<TEntity> Delete(Func<TEntity, Boolean> predicate);

        /// <summary>
        /// 筛选数据仓库中指定条件下的实体数据，并返回查询对象
        /// </summary>
        /// <param name="predicate">指定筛选条件</param>
        /// <returns></returns>
        IQueryable<TEntity> Fetch(Func<TEntity, Boolean> predicate);
    }
}
