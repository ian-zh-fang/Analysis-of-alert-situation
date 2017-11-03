
/*
 * guid: $GUID$
 * file: Helper
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:04:13
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

    public static class FilePathEx
    {
        /// <summary>
        /// 将目标字文件路径转换为文件路径上下文对象，若目标文件路径为 null 或者零长度，或者全部为空字符时，返回 null
        /// </summary>
        /// <param name="source">需要处理的目标字文件路径</param>
        /// <returns></returns>
        public static FilePath ToFilePath(this string source)
        {
            if (source.IsWhitespaces()) { return null; }
            return new FilePath(source);
        }

        /// <summary>
        /// 尝试解析目标文件路径，若目标文件路径是有效的文件路径，返回有效的路径信息；否则，返回 null
        /// </summary>
        /// <param name="source">需要处理的目标字文件路径</param>
        /// <returns></returns>
        public static FilePath ToFilePathIfValid(this string source)
        {
            var path = source.ToFilePath();

            // 路径不存在
            if (path.IsNull()) { return null; }

            // 有效的文件路径
            if (path.IsValid) { return path; }

            // 无效的文件路径
            return null;
        }

        /// <summary>
        /// 尝试解析目标文件路径，若目标文件路径符合有效的文件路径，返回有效的文件路径；否则，返回 null
        /// </summary>
        /// <param name="source">需要处理的目标字文件路径</param>
        /// <returns></returns>
        public static string ToPath(this string source)
        {
            var path = source.ToFilePathIfValid();
            if (path.IsNull()) { return null; }

            return path.ValidPath;
        }

        /// <summary>
        /// 尝试解析目标文件路径，
        /// 若目标文件路径不符合有效的文件路径，返回 null；
        /// 若目标文件路径符合有效的绝对路径，返回当前绝对路径；
        /// 若目标文件路径符合有效的相对路径，返回当前应用程序工作目录为根的绝对路径
        /// </summary>
        /// <param name="source">需要处理的目标字文件路径</param>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <returns></returns>
        public static string ToAbsolutePath(this string source)
        {
            var path = source.ToFilePathIfValid();
            if (path.IsNull()) { return null; }

            if (path.PathType == FilePath.FilePathType.Absolute) { return path.ValidPath; }

            if (path.PathType == FilePath.FilePathType.Relative)
            {
                return string.Concat(Environment.CurrentDirectory, path.ValidPath);
            }

            return null;
        }
    }
}
