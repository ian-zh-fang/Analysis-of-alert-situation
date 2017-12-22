
/*
 * guid: $GUID$
 * file: MyDbConfiguration
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/22 17:30:56
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

namespace org.aoas.app.repository.entityframework
{
    using org.aoas.attributes;
    using org.aoas.config;

    /// <summary>
    /// 旨在实现一种数据库连接配置业务功能
    /// </summary>
    public sealed class MyDbConfiguration : XmlConfiguration
    {
        // 上下文同步对象锁
        private static readonly object _SyncLocker = new object();

        // 单例模式唯一实例
        private static MyDbConfiguration _singleInstance;

        /// <summary>
        /// 创建 <see cref="MyDbConfiguration"/> 类型的新实例
        /// </summary>
        /// <param name="sectionName">配置节点名称，默认为 connectionString .</param>
        /// <param name="rootName">配置根节点名称，默认为 configuration .</param>
        /// <param name="fileName">配置文件名称，默认为 db.config . </param>
        public MyDbConfiguration(string sectionName = "connectionString", string rootName = "configuration", string fileName = "db.config")
            : base(sectionName, rootName, fileName)
        { }
        
        /// <summary>
        /// 数据库默认链接
        /// </summary>
        [Alias("defaultConnectionString")]
        public MyDbConfigurationContext Default { get; private set; }

        /// <summary>
        /// 默认配置
        /// </summary>
        public static MyDbConfiguration Configuration
        {
            get
            {
                if(_singleInstance.IsNull())
                {
                    lock (_SyncLocker)
                    {
                        if (_singleInstance.IsNull()) { _singleInstance = new MyDbConfiguration(); }
                    }
                }

                return _singleInstance;
            }
        }
    }

    /// <summary>
    /// 旨在实现数据库连接配置上下文
    /// </summary>
    public class MyDbConfigurationContext : XmlConfigurationElement, IDbConfiguration
    {
        /// <summary>
        /// 创建 <see cref="MyDbConfigurationContext"/> 类型实例
        /// </summary>
        public MyDbConfigurationContext() : base() { }

        /// <summary>
        /// 数据库连接字符串或者名称
        /// </summary>
        [Alias("connectionStringOrName")]
        public string ConnectionStringOrName { get; private set; }

        /// <summary>
        /// 数据库架构或者所属用户名称
        /// </summary>
        [Alias("ownerOrSchema")]
        public string OwnerOrSchema { get; private set; }
    }
}
