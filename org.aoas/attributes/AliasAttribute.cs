
/*
 * guid: $GUID$
 * file: AliasAttribute
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:14:21
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

namespace org.aoas.attributes
{
    using System;

    public interface IAlias
    {
        String Name { get; }
    }

    /// <summary>
    /// 属性别名
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class AliasAttribute : Attribute, IAlias
    {
        public AliasAttribute(String name)
        {
            name.ThrowIfWhitespace("name");
            Name = name;
        }

        public String Name { get; private set; }
    }
}
