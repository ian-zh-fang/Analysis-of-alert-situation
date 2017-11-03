
/*
 * guid: $GUID$
 * file: SerializationDependancy
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:19:21
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
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    // 实现 ISerializable 的依赖
    public abstract class SerializationDependancy
        : PropertySerializationDependancy<SerializationInfo>
        , ISerializable
    {
        protected SerializationDependancy()
            : base()
        { }

        protected SerializationDependancy(SerializationInfo info, StreamingContext context)
            : base(info)
        { }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            var arr = Serialize();
            foreach (var d in arr)
            {
                info.AddValue(d.Name, d.Value, d.Type);
            }
        }

        protected override IEnumerable<DataContext> GetCollection(SerializationInfo source)
        {
            var lts = new List<DataContext>();
            foreach (var item in source)
            {
                var e = new DataContext(item.Name, item.Value, item.ObjectType);
                lts.Add(e);
            }

            return lts;
        }
    }
}
