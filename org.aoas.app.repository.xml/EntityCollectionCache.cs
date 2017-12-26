
/*
 * guid: $GUID$
 * file: EntityCollectionCache
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/12/26 14:08:33
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

namespace org.aoas.app.repository.xml
{
    using System;
    using System.Collections.Generic;
    using org.aoas.cache;

    internal abstract class EntityCollectionCache<TKey, TValue> : MemoryCache<TKey, TValue>
        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
        where TValue : EntityCollection<TValue>, new()
    {
        protected EntityCollectionCache(EntityCollectionContext<TValue> collection, ICacheContext context)
            : base(context)
        { }

        protected EntityCollectionCache(IEnumerable<TValue> items, ICacheContext context)
            : base(items, context)
        { }
    }
}
