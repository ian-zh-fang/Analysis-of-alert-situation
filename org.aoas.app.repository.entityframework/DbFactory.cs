
/*
 * guid: $GUID$
 * file: DbFactory
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 15:43:36
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
    using System.Data.Entity;

    /// <summary>
    /// 旨在实现一种统一的数据上下文处理工厂
    /// </summary>
    public sealed class DbFactory
    {
        // 上下文同步锁对象
        private static readonly object _SyncLocker = new object();

        // 单实例模式对象
        private static DbFactory _singleInstance;

        // 当前 DB 上下文
        private DbContext _db;

        // 单实例模式保护构造函数
        private DbFactory() { }

        /// <summary>
        /// 获取一个 <see cref="DbContext"/> 类型的实例
        /// </summary>
        /// <param name="connectionStringOrName">数据库连接字符串或者名称</param>
        /// <param name="ownerOrSchema">数据库架构或者所属用户名称</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// connectionStringOrName 是 null 或者
        /// ownerOrSchema 是 null
        /// </exception>
        /// <exception cref="AggregateException">EntityFramework 内部异常</exception>
        public DbContext GetDb(string connectionStringOrName, string ownerOrSchema = "dbo")
        {
            if (_db.IsNull())
            {
                lock (_SyncLocker)
                {
                    if (_db.IsNull()) { _db = NewDb(connectionStringOrName, ownerOrSchema); }
                }
            }

            if (((MyDbContext)_db).IsDisposed)
            {
                lock (_SyncLocker)
                {
                    if (((MyDbContext)_db).IsDisposed) { _db = NewDb(connectionStringOrName, ownerOrSchema); }
                }
            }

            return _db;
        }

        /// <summary>
        /// 获取一个 <see cref="DbContext"/> 类型的实例，当前过程总是返回一个 <see cref="DbContext"/> 类型的新实例
        /// </summary>
        /// <param name="connectionStringOrName">数据库连接字符串或者名称</param>
        /// <param name="ownerOrSchema">数据库架构或者所属用户名称</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// connectionStringOrName 是 null 或者
        /// ownerOrSchema 是 null
        /// </exception>
        /// <exception cref="AggregateException">EntityFramework 内部异常</exception>
        public DbContext NewDb(string connectionStringOrName, string ownerOrSchema = "dbo")
        {
            connectionStringOrName.ThrowIfWhitespace(nameof(connectionStringOrName));
            ownerOrSchema.ThrowIfWhitespace(nameof(ownerOrSchema));

            DbContext db;

            try
            {
                db = new MyDbContext(connectionStringOrName, ownerOrSchema);
            }
            catch (Exception e)
            {
                throw new AggregateException(e);
            }

            return db;
        }

        /// <summary>
        /// 默认工厂
        /// </summary>
        public static DbFactory Default
        {
            get
            {
                if (_singleInstance.IsNull())
                {
                    lock (_SyncLocker)
                    {
                        if (_singleInstance.IsNull()) { _singleInstance = new DbFactory(); }
                    }
                }

                return _singleInstance;
            }
        }

        // 析构函数
        ~DbFactory()
        {
            if (_db.IsNull()) { return; }
            _db.Dispose();
        }
    }
}
