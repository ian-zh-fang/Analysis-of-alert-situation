﻿
/*
 * guid: $GUID$
 * file: SyncAreaInfRepository
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 18:11:48
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
    public sealed class SyncAreaInfRepository : DbRepository<entity.Orgnization>
    {
        public SyncAreaInfRepository(IDbConfiguration cfg)
            : base(cfg)
        { }

        public SyncAreaInfRepository()
            : this(MyDbConfiguration.Configuration.Default)
        { }
    }
}
