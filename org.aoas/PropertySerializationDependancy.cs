
/*
 * guid: $GUID$
 * file: PropertySerializationDependancy
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:18:30
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

namespace org.aoas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using attributes;

    // 属性序列化处理依赖
    //  将当前对象转换为指定 PropertySerializationDependancy<TSource>.DataContext 数据集合
    //  或者将 PropertySerializationDependancy<TSource>.DataContext 数据集合中的数据映射到当前对象的相应属性
    //
    //  依赖特性：
    //      attributes.AliasAttribute
    //      attributes.IgnorableAttribute
    //
    //  attributes.AliasAttribute：
    //      R1，别名特性
    //      R2，若属性存在 AliasAttribute 的特性，并且 AliasAttribute 的 Name 值存在，并且非空字符串，那么取 AliasAttribute.Name ；否则，取 PropertyInfo.Name 值
    //      R3，支持派生类重写
    //
    //  attributes.IgnorableAttribute：
    //      R1，可忽略值特性
    //      R2，序列化对象时，若属性存在 IgnorableAttribute 的特性，并且属性值存在可忽略的值时，将使当前属性不被序列化
    //      R3，支持派生类重写
    //
    //  TSource：数据元类型

    public abstract class PropertySerializationDependancy<TSource>
    {
        protected PropertySerializationDependancy() { }

        protected PropertySerializationDependancy(TSource source)
        {
            var arr = GetCollection(source);
            Deserialize(arr);
        }

        // 获取当前对象
        protected virtual IEnumerable<DataContext> Serialize()
        {
            var lts = new List<DataContext>();
            var pros = GetProperties();
            foreach (var pro in pros)
            {
                Object val = null;
                if (IsCanbeIgnore(pro, out val)) continue;

                var name = GetPropertyName(pro);

                var d = new DataContext(name, val, pro.PropertyType);
                lts.Add(d);
            }

            return lts;
        }

        // 是否忽略当前属性
        //  true 标识可以忽略当前属性；否则，返回 false
        protected virtual Boolean IsCanbeIgnore(PropertyInfo pro, out Object value)
        {
            if (pro.GetMethod.IsNull())
            {
                value = null;
                return true;
            }

            var val = value = GetPropertyValue(pro);

            return
                pro.GetCustomAttributes<IgnorableAttribute>()
                    .OrderBy(t => t.Order) // 正序排列，小值优先
                    .Any(t => t.IsIgnore(val, pro.PropertyType));
        }

        // 获取当前对象指定属性的值
        //  如果获取失败，返回 null；否则，返回属性原本的值
        protected virtual Object GetPropertyValue(PropertyInfo pro)
        {
            try
            {
                return pro.GetMethod.Invoke(this, null);
            }
            catch (Exception) { return null; }
        }

        // 将元数据映射到当前对象的相应属性
        protected virtual void Deserialize(IEnumerable<DataContext> cols)
        {
            var pros = GetProperties();
            foreach (var pro in pros)
            {
                var md = GetSetMethod(pro);
                if (md.IsNull()) continue;

                var val = GetValue(pro, cols);
                if (val.IsNull()) continue;

                md.Invoke(this, new Object[] { val });
            }
        }

        // 获取指定类型的公共并且可实例化对象的属性
        //  type：指定的类型，若为 null，那么取当前对象的类型
        private PropertyInfo[] GetProperties(Type type = null)
        {
            type = type ?? GetType();
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        // 获取当前属性的 SetMethod 方法对象
        //  若当前属性的 SetMethod 对象不存在，那么将从当前属性的 DeclaringType （声明类型）属性中重新获取当前的属性
        //  若当前属性的 DeclaringType（声明类型） 属性和 ReflectedType（反射类型）一直，那么直接返回当前属性的 SetMethod 属性值
        protected MethodInfo GetSetMethod(PropertyInfo pro)
        {
            // 反射类型与声明类型一致，因此直接返回
            if (pro.DeclaringType == pro.ReflectedType)
            {
                return pro.SetMethod;
            }

            // 反射类型与声明类型不一致，如果当前
            if (pro.SetMethod.IsNull())
            {
                pro = GetProperties(pro.DeclaringType).First(t => t.Name == pro.Name);
            }

            return pro.SetMethod;
        }

        // 获取属性名称
        //  R，若当前属性存在 AliasAttribute 的特性，并且 AliasAttribute 的 Name 值存在，并且非空字符串，那么取 AliasAttribute.Name ；否则，取 PropertyInfo.Name 值.
        protected virtual String GetPropertyName(PropertyInfo pro)
        {
            IAlias attr = pro.GetCustomAttributes<AliasAttribute>().FirstOrDefault();

            if (attr.IsNull())
            {
                return pro.Name;
            }

            if (attr.Name.IsWhitespaces())
            {
                return pro.Name;
            }

            return attr.Name;
        }

        // 从数据集中获取匹配指定属性的数据
        //  若不存在，则返回 null。
        protected virtual Object GetValue(PropertyInfo pro, IEnumerable<DataContext> cols)
        {
            var name = GetPropertyName(pro);
            return GetValue(name, pro.PropertyType, cols);
        }

        // 从数据集中获取指定名称的指定类型的数据
        //  若不存在，则返回 null。
        protected virtual Object GetValue(String name, Type valueType, IEnumerable<DataContext> cols)
        {
            // 匹配失败，返回 null
            var dat = cols.FirstOrDefault(t => t.Name == name);
            if (dat.IsNull()) return null;

            // 值不存在，返回 null
            if (dat.Value.IsNull()) return null;

            // 数据类型与值类型一致，直接返回数据
            if (valueType == dat.Type)
            {
                return dat.Value;
            }

            // 数据类型与值类型不一致，将数据转换为指定的值类型
            return ConvertType(valueType, dat);
        }

        // 更改数据的对象为指定的值类型
        //  成功，返回转换后的值；否则，返回 null
        //  valueType：目标值类型
        //  context：数据上下文
        protected virtual Object ConvertType(Type valueType, DataContext context)
        {
            try
            {
                return Convert.ChangeType(context.Value, valueType);
            }
            catch (Exception) { return null; }
        }

        public override string ToString()
        {
            var arr = Serialize();
            return string.Join(", ", arr.Select(t => string.Concat(t.Name, ":", t.Value)));
        }

        // 将数据源转换为数据上下文集合
        protected abstract IEnumerable<DataContext> GetCollection(TSource source);

        // 数据上下文
        //  说明 TSource 中的数据上下文内容
        protected sealed class DataContext
        {
            public DataContext(String name, Object value, Type valueType)
            {
                Name = name;
                Value = value;
                Type = valueType;
            }

            // 名称
            public String Name { get; private set; }

            // 值
            public Object Value { get; private set; }

            // 值类型
            public Type Type { get; private set; }
        }
    }
}
