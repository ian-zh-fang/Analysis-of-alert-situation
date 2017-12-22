
/*
 * guid: $GUID$
 * file: Repository
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 16:18:57
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
    /// 旨在实现一组数据仓储的基础业务功能
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class Repository<TEntity> : DisposeDependancy, IRepository<TEntity>, IUnitWork, IDisposable
    {
        protected Repository() : base() { }

        TEntity IRepository<TEntity>.Insert(TEntity entity)
        {
            entity.ThrowIfNull(nameof(entity));
            return OnInsert(entity);
        }

        TEntity IRepository<TEntity>.Delete(TEntity entity)
        {
            entity.ThrowIfNull(nameof(entity));
            return OnDelete(entity);
        }

        IEnumerable<TEntity> IRepository<TEntity>.Delete(Func<TEntity, Boolean> predicate)
        {
            predicate.ThrowIfNull(nameof(predicate));
            return OnDelete(predicate);
        }

        TEntity IRepository<TEntity>.Upgrade(TEntity entity)
        {
            entity.ThrowIfNull(nameof(entity));
            return OnUpgrade(entity);
        }

        IQueryable<TEntity> IRepository<TEntity>.Fetch(Func<TEntity, Boolean> predicate)
        {
            predicate.ThrowIfNull(nameof(predicate));
            return OnFetch(predicate);
        }

        int IUnitWork.Save()
        {
            return OnSave();
        }

        /// <summary>
        /// 增加一个新的数据对象到数据仓储中，并返回新增的实体对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        protected abstract TEntity OnInsert(TEntity entity);

        /// <summary>
        /// 从数据仓储中移除指定的数据对象，并返回被移除的对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        protected abstract TEntity OnDelete(TEntity entity);

        /// <summary>
        /// 从数据仓储中移除指定谓词条件匹配的数据对象，并返回一组被移除的对象集合
        /// </summary>
        /// <param name="predicate">移除筛选条件</param>
        /// <returns></returns>
        protected abstract IEnumerable<TEntity> OnDelete(Func<TEntity, Boolean> predicate);

        /// <summary>
        /// 更新数据仓储中指定的对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        protected abstract TEntity OnUpgrade(TEntity entity);

        /// <summary>
        /// 从数据仓储中筛选指定谓词条件匹配的数据对象，并返回一组筛选的实体对象集合
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        protected abstract IQueryable<TEntity> OnFetch(Func<TEntity, Boolean> predicate);

        /// <summary>
        /// 保存数据仓储的所有更改记录，并返回总更改数量
        /// </summary>
        /// <returns></returns>
        protected abstract int OnSave();
    }
}
