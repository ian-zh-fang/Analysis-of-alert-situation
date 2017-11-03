
/*
 * guid: $GUID$
 * file: FilePath
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:05:02
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
    using System.Text.RegularExpressions;

    /// <summary>
    /// 文件路径，旨在定义文件路径的上下文内容
    /// </summary>
    public sealed class FilePath
    {
        // 相对地址路径正则表达式规则
        private static readonly Regex _RelativePathRegex = new Regex(@"^(((\\|/)(?!\s+)[^/:*?<>\""|\\]+)+(\\|/)?)$");

        // 绝对地址路径正则表达是规则
        private static readonly Regex _AbsolutePathRegex = new Regex(@"^[a-zA-Z]:((((\\|/)(?!\s+)[^/:*?<>\""|\\]+)+(\\|/)?)|(\\|/))\s*$");

        private FilePath()
        {
            IsValid = false;
            ValidPath = null;
            PathType = FilePathType.None;
        }

        /// <summary>
        /// 创建一个 FilePath 类型的新实例，并返回创建的对象
        /// </summary>
        /// <param name="path">需要处理的原始路径信息</param>
        public FilePath(string path)
            : this()
        {
            RawPath = path;
            Init();
        }

        // 初始化基础信息
        private void Init()
        {
            if (RawPath.IsWhitespaces()) { return; }

            if (TryGetPathIfValid(RawPath, _RelativePathRegex, FilePathType.Relative)) { return; }

            if (TryGetPathIfValid(RawPath, _AbsolutePathRegex, FilePathType.Absolute)) { return; }
        }

        // 尝试解析指定的文件路径，若解析成功，返回 true；否则，返回 false
        private bool TryGetPathIfValid(string path, Regex pattern, FilePathType type)
        {
            try
            {
                if (pattern.IsMatch(path))
                {
                    IsValid = true;
                    PathType = type;
                    ValidPath = path.TrimEnd('\\', '/');
                }

                return false;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// 原始路径
        /// </summary>
        public string RawPath { get; private set; }

        /// <summary>
        /// 原始路径是否是有效的文件路径
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        /// 有效的路径，若原始路径信息不是有效的
        /// </summary>
        public string ValidPath { get; private set; }

        /// <summary>
        /// 当前上下文中有效文件路径类型，若有效文件路径不存在，当前值固定为 FilePathType.None；否则，为有效路径相对应的值
        /// </summary>
        public FilePathType PathType { get; private set; }

        /// <summary>
        /// 文件路径类型
        /// </summary>
        public enum FilePathType : byte
        {
            /// <summary>
            ///没有指定
            /// </summary>
            None = 0x00,
            /// <summary>
            /// 相对路径，没有指明根路径，路径形如：/{dirname}
            /// </summary>
            Relative = 0x01,
            /// <summary>
            /// 绝对路径，指明根路径的物理路径信息，路径形如：A:/{dirname}
            /// </summary>
            Absolute = 0x02,
        }
    }
}
