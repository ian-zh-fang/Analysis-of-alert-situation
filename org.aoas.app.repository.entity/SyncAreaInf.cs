
/*
 * guid: $GUID$
 * file: SyncOrgnization
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 14:54:07
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
    public class Orgnization : BaseEntityKeyString
    {
        protected override KeyContext Context => KeyContext.TableAreaInf;
    }
}
