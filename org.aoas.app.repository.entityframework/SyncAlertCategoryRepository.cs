
/*
 * guid: $GUID$
 * file: SyncAlertCategoryRepository
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 18:06:32
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
    public sealed class SyncAlertCategoryRepository : DbRepository<entity.CaseClasses>
    {
        public SyncAlertCategoryRepository(IDbConfiguration cfg)
            : base(cfg)
        { }

        public SyncAlertCategoryRepository()
            : this(MyDbConfiguration.Configuration.Default)
        { }
    }
}
