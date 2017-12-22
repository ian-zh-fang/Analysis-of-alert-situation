
/*
 * guid: $GUID$
 * file: SyncCaseClassesStatistics
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 14:53:28
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

namespace org.aoas.app.repository.entity
{
    using System;

    public class CaseClassesStatistics : BaseEntityKeyString
    {
        protected override KeyContext Context => KeyContext.TableAlertTotalForArea;
    }
}
