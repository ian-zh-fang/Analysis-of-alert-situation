
/*
 * guid: $GUID$
 * file: FileFinder
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:08:27
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

namespace org.aoas.file
{
    using System;

    /// <summary>
    /// 文件查找器，
    /// 定义了从包含应用程序根目录的一组有序文件查找路径的文件查找功能器，
    /// 根目录将在构造函数时指定的有序组合之后查询
    /// </summary>
    public class FileFinder : BaseFileFinder
    {
        /// <summary>
        /// 创建 FileFinder 的新实例，并返回创建的对象
        /// </summary>
        /// <param name="paths">一组需要查找的文件路径信息</param>
        public FileFinder(params string[] paths)
            : base(paths)
        {
            AppendPath(Environment.CurrentDirectory);
        }
    }
}
