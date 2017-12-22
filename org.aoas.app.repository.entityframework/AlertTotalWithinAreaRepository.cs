
/*
 * guid: $GUID$
 * file: AlertTotalWithinAreaRepository
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 18:10:26
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
    public sealed class AlertTotalWithinAreaRepository : DbRepository<entity.AlertTotalWithinArea>
    {
        public AlertTotalWithinAreaRepository(IDbConfiguration cfg)
            : base(cfg)
        { }

        public AlertTotalWithinAreaRepository()
            : this(MyDbConfiguration.Configuration.Default)
        { }
    }
}
