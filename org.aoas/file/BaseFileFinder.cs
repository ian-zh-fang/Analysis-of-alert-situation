
/*
 * guid: $GUID$
 * file: BaseFileFinder
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:06:36
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
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// 基础文件查找器，IFileFinder 的基本实现，定义了从一组有序文件路径集合中查找指定文件功能的基本业务流程和规则
    /// </summary>
    public abstract class BaseFileFinder
        : DisposeDependancy
        , IFileFinder
        , IDisposable
    {
        private List<string> _paths;

        /// <summary>
        /// 一种保护机制的构造函数
        /// </summary>
        /// <param name="findPaths">文件查找的一组物理路径内容</param>
        protected BaseFileFinder(params string[] findPaths)
            : base()
        {
            _paths = new List<string>();
            AppendPath(findPaths);
        }

        /// <summary>
        /// 查找指定名称的文件，若查找成功，返回文件的物理路径；否则，返回 null
        /// </summary>
        /// <param name="fileName">需要查找的文件名称</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ry.common.exceptions.FileNotFoundException"></exception>
        /// <returns></returns>
        public string Find(string fileName)
        {
            fileName.ThrowIfWhitespace("fileName");

            var founds = new List<string>();
            string vPath = null;
            for (int i = 0; i < _paths.Count; i++)
            {
                // 当前路径
                string path = _paths[i];

                // 确认文件是否存在
                if (TryGetIfFileExists(path, fileName, out vPath)) { break; }

                // 文件不存在，将当前路径添加到已查寻路径中
                if (vPath.IsWhitespaces()) { continue; }
                founds.Add(vPath);
                vPath = null;
            }

            // 查询文件失败，抛出文件查询失败错误
            if (vPath.IsWhitespaces())
            {
                throw new exceptions.FileNotFoundException(fileName, founds);
            }

            return vPath;
        }

        /// <summary>
        /// 追加指定的查找路径信息到有序路径的集合
        /// </summary>
        /// <param name="paths">一组需要添加的查找路径</param>
        public void AppendPath(params string[] paths)
        {
            var arr = paths.Select(t => t.ToAbsolutePath()).Where(t => !t.IsWhitespaces());
            _paths.AddRange(arr);
        }

        // 尝试确认文件，若文件存在，返回 true；否则，返回 false
        //  path：需要确认文件路径
        //  fileName：需要文件名称
        //  filePath：文件物理路径全路径信息，若不存在，返回 null
        private bool TryGetIfFileExists(string path, string fileName, out string filePath)
        {
            filePath = string.Concat(path, "/", fileName);
            try
            {
                return File.Exists(filePath);
            }
            catch (Exception) { return false; }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_paths.IsNull()) { return; }
            _paths.Clear();
            _paths = null;
        }

        /// <summary>
        /// 有效的文件查找路径信息
        /// </summary>
        public IEnumerable<string> ValidPaths
        {
            get { return _paths; }
        }
    }
}
