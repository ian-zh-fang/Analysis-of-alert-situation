
/*
 * guid: $GUID$
 * file: EntityContext
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/19 10:29:47
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
    using System;

    /// <summary>
    /// 旨在定义一种实体上下文结构信息
    /// </summary>
    public sealed class KeyContext
    {
        private string _desc;

        /// <summary>
        /// 私有构造函数，创建一个 <see cref="KeyContext"/> 类型的新实例
        /// </summary>
        /// <param name="code">实体代码</param>
        /// <param name="name">实体名称</param>
        /// <param name="desc">描述内容</param>
        private KeyContext(byte code, string name, string desc = null)
        {
            name.ThrowIfWhitespace(nameof(name));

            Code = code;
            Name = name;
            _desc = desc;
        }

        /// <summary>
        /// 实体名称
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// 实体代码
        /// </summary>
        public Byte Code { get; private set; }

        /// <summary>
        /// 描述内容
        /// </summary>
        public String Desc
        {
            get { return _desc ?? Name; }
        }

        /// <summary>
        /// 返回 Code 属性的 16 进制字符串
        /// </summary>
        /// <returns></returns>
        public String CodeHexString()
        {
            return string.Format("{0:x2}", Code);
        }

        /// <summary>
        /// 辖区
        /// </summary>
        public static readonly KeyContext TableArea = new KeyContext(0x00, "t_area", "辖区");

        /// <summary>
        /// 警情类型
        /// </summary>
        public static readonly KeyContext TableAlertCate = new KeyContext(0x01, "t_alertcate", "警情类型");

        /// <summary>
        /// 辖区警情类型统计
        /// </summary>
        public static readonly KeyContext TableAlertTotalForArea = new KeyContext(0x02, "t_totalfor", "辖区警情类型统计");
    }
}
