
/*
 * guid: $GUID$
 * file: FileNotFoundException
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:01:03
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

namespace org.aoas.exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// 文件查找失败异常，旨在支持查询指定位置的指定文件失败时的错误内容
    /// </summary>
    public class FileNotFoundException : Exception
    {
        /// <summary>
        /// 创建 FileNotFoundException 的新实例，并返回创建的对象
        /// </summary>
        /// <param name="fileName">需要查找的文件名称</param>
        /// <param name="foundPaths">需要查找的路径信息</param>
        public FileNotFoundException(string fileName, IEnumerable<string> foundPaths)
            : base(string.Concat("specified file cannot be found from the following path: \n", string.Join("\n", foundPaths.Where(t => !t.IsWhitespaces()))))
        {
            Filename = fileName;
            FoundPaths = foundPaths.Where(t => !t.IsWhitespaces()).ToArray();
        }

        /// <summary>
        /// 反序列化支持构造函数
        /// </summary>
        /// <param name="info">序列化内容上下文</param>
        /// <param name="context">数据源</param>
        protected FileNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// 查找文件名称
        /// </summary>
        public string Filename { get; private set; }

        /// <summary>
        /// 查找路径
        /// </summary>
        public string[] FoundPaths { get; private set; }
    }
}
