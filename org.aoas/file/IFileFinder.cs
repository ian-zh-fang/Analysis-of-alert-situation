
/*
 * guid: $GUID$
 * file: IFileFinder
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:00:17
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
    /// <summary>
    /// 旨在定义支持从指定的路径查找文件的一组功能约定
    /// </summary>
    public interface IFileFinder
    {
        /// <summary>
        /// 查找指定名称的文件，若查找成功，返回文件的物理路径；否则，返回 null
        /// </summary>
        /// <param name="fileName">需要查找的文件名称</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="org.aoas.exceptions.FileNotFoundException"></exception>
        /// <returns></returns>
        string Find(string fileName);

        /// <summary>
        /// 添加指定的查找路径信息
        /// </summary>
        /// <param name="paths">一组需要添加的查找路径</param>
        void AppendPath(params string[] paths);
    }
}
