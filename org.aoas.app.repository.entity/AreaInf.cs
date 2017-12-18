
/*
 * guid: $GUID$
 * file: Area
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/18 14:44:11
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
    /// <summary>
    /// 辖区信息
    /// </summary>
    public sealed class AreaInf
    {
        /// <summary>
        /// 辖区名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 辖区描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 结构编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 上一级辖区标识
        /// </summary>
        public int Pid { get; set; }
    }
}
