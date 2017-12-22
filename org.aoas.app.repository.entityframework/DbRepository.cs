
/*
 * guid: $GUID$
 * file: EntityFrameworkRepository
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 16:45:02
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
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public abstract class DbRepository<TEntity> : Repository<TEntity>
        where TEntity : class
    {
        // 数据库上下文
        private readonly DbContext _db;

        /// <summary>
        /// 创建 <see cref="DbRepository{TEntity}"/> 类型的新实例
        /// </summary>
        /// <param name="context">数据库上下文 <see cref="DbContext"/> 类型的实例</param>
        protected DbRepository(DbContext context)
            :base()
        {
            context.ThrowIfNull(nameof(context));
            _db = context;
        }

        /// <summary>
        /// 创建 <see cref="DbRepository{TEntity}"/> 类型的新实例
        /// </summary>
        /// <param name="connectionStringOrName">数据库连接字符串或者名称</param>
        /// <param name="ownerOrSchema">数据库架构或者所属用户名称</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="AggregateException"></exception>
        protected DbRepository(string connectionStringOrName, string ownerOrSchema)
            :this(DbFactory.Default.GetDb(connectionStringOrName, ownerOrSchema))
        { }

        /// <summary>
        /// 创建 <see cref="DbRepository{TEntity}"/> 类型的新实例
        /// </summary>
        /// <param name="cfg">一组数据库配置 <see cref="IDbConfiguration"/> 实例</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="AggregateException"></exception>
        protected DbRepository(IDbConfiguration cfg)
            :this(cfg?.ConnectionStringOrName, cfg?.OwnerOrSchema)
        { }

        protected override TEntity OnInsert(TEntity entity)
        {
            var st = _db.Set<TEntity>();
            return st.Add(entity);
        }

        protected override TEntity OnDelete(TEntity entity)
        {
            var st = _db.Set<TEntity>();
            return st.Remove(entity);
        }

        protected override IEnumerable<TEntity> OnDelete(Func<TEntity, bool> predicate)
        {
            var st = _db.Set<TEntity>();
            return st.RemoveRange(OnFetch(predicate));
        }

        protected override TEntity OnUpgrade(TEntity entity)
        {
            var st = _db.Set<TEntity>();
            var e = st.Attach(entity);
            var t = _db.Entry<TEntity>(entity);
            t.State = EntityState.Modified;

            return e;
        }

        protected override IQueryable<TEntity> OnFetch(Func<TEntity, bool> predicate)
        {
            return
                _db.Set<TEntity>().Where(predicate).AsQueryable();
        }

        protected override int OnSave()
        {
            return _db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _db.Dispose();
        }
    }
}
