
/*
 * guid: $GUID$
 * file: XmlConfiguration
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 14:16:54
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

namespace org.aoas.config
{
    using System;
    using System.Xml;
    using org.aoas.file;

    public abstract class XmlConfiguration : XmlConfigurationElement
    {
        // 默认文件查找路径信息
        private static readonly string[] DefaultFinderPaths = new string[] { @"/config", @"/shared/config" };
        // 配置文件
        private readonly string _file;
        // 配置节点名称
        private readonly string _sectionName;
        // 根节点名称
        private readonly string _rootName;

        /// <summary>
        /// 创建 XmlConfiguration 类的新实例
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="rootName"></param>
        /// <param name="fileName"></param>
        /// <param name="finder"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="exceptions.FileNotFoundException">配置文件查找失败</exception>
        protected XmlConfiguration(string sectionName, string rootName, string fileName, file.IFileFinder finder)
        {
            sectionName.ThrowIfWhitespace(nameof(sectionName));
            rootName.ThrowIfWhitespace(nameof(rootName));
            fileName.ThrowIfWhitespace(nameof(fileName));
            finder.ThrowIfNull(nameof(finder));

            _sectionName = sectionName.ToLower();
            _rootName = rootName.ToLower();
            _file = finder.Find(fileName);

            Init();
        }

        // 初始化内部对象
        protected virtual void Init()
        {
            using (var reader = GetReader(_file, _sectionName))
            {
                Deserialize(reader);
            }
        }

        protected virtual XmlReader GetReader(string file, string section)
        {
            var reader = XmlReader.Create(file);
            while (reader.Read())
            {
                if (section == reader.Name.ToLower() && XmlNodeType.Element == reader.NodeType) { break; }
            }
            return reader;
        }

        /// <summary>
        /// 创建 XmlConfiguration 类的新实例
        /// </summary>
        /// <param name="sectionName">配置节点名称</param>
        /// <param name="rootName">根节点名称，默认为 configuration</param>
        /// <param name="fileName">配置文件名称，默认为 app.config</param>
        protected XmlConfiguration(string sectionName, string rootName = "configuration", string fileName = "app.config")
            : this(sectionName, rootName, fileName, new file.FileFinder(DefaultFinderPaths))
        { }

        /// <summary>
        /// 保存当前配置信息到指定的文件，若不指定文件，将保存至当前上下文中指定的文件
        /// </summary>
        /// <param name="file">保存文件全路径信息</param>
        /// <exception cref="ArgumentNullException">无效的保存路径</exception>
        public void SaveAs(string file = null)
        {
            file = file ?? _file;
            var path = file.ToAbsolutePath();
            path.ThrowIfWhitespace(nameof(path));

            using (var writer = XmlWriter.Create(path))
            {
                writer.WriteStartDocument();
                SerializeElement(_rootName, SerializeSection, writer);
                writer.WriteEndDocument();
            }
        }

        private void SerializeSection(XmlWriter writer)
        {
            SerializeElement(_sectionName, Serialize, writer);
        }

        private void SerializeElement(string name, Action<XmlWriter> action, XmlWriter writer)
        {
            writer.WriteStartElement(name);
            action.Invoke(writer);
            writer.WriteFullEndElement();
        }
    }
}
