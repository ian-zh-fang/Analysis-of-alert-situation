
/*
 * guid: $GUID$
 * file: SyncAlertTotalWithinAreaRepository
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 18:09:12
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
    public sealed class SyncAlertTotalWithinAreaRepository : DbRepository<entity.CaseClassesStatistics>
    {
        public SyncAlertTotalWithinAreaRepository(IDbConfiguration cfg)
            : base(cfg)
        { }

        public SyncAlertTotalWithinAreaRepository()
            : this(MyDbConfiguration.Configuration.Default)
        { }
    }
}
