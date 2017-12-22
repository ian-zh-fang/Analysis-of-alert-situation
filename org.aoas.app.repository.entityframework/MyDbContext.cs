
/*
 * guid: $GUID$
 * file: MyDbContext
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 15:48:06
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
    using System.Data.Entity;
    using org.aoas.app.repository.entity;

    /// <summary>
    /// 旨在实现一组数据库上下文处理功能
    /// </summary>
    internal class MyDbContext : DbContext
    {
        // 当前数据库架构或者所属用户名称
        private readonly string _ownerOrSchema;

        // 当前对象释放标识
        private bool _isDisposed;

        /// <summary>
        /// 创建 <see cref="OracleContext"/> 类型的新实例
        /// </summary>
        /// <param name="connectionStringName">数据库连接字符串或者名称</param>
        /// <param name="ownerOrSchema">数据库架构或者所属用户名称</param>
        public MyDbContext(string connectionStringName, string ownerOrSchema)
            : base($"name={connectionStringName}")
        {
            _ownerOrSchema = ownerOrSchema;
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema($"{_ownerOrSchema.ToUpper()}");
            modelBuilder.Entity<CaseClasses>().ToTable("tb_CaseClasses");
            modelBuilder.Entity<CaseClassesStatistics>().ToTable("tb_CaseClassesStatistics");
            modelBuilder.Entity<Orgnization>().ToTable("tb_Orgnization");
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed) { return; }
            _isDisposed = true;

            base.Dispose(disposing);
        }

        /// <summary>
        /// 当前对象是否已经释放，
        /// 若已经释放，返回 true；否则，返回 false .
        /// </summary>
        public bool IsDisposed
        {
            get { return _isDisposed; }
        }

        public virtual DbSet<CaseClasses> CaseClassesItems { get; set; }

        public virtual DbSet<Orgnization> OrgnizationItems { get; set; }

        public virtual DbSet<CaseClassesStatistics> CaseClassesStaticsItems { get; set; }
    }
}
