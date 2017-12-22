
/*
 * guid: $GUID$
 * file: SyncCaseClasses
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 14:52:38
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

    public sealed class CaseClasses : BaseEntityKeyString
    {


        protected override KeyContext Context => KeyContext.TableAlertCategory;
    }
}
