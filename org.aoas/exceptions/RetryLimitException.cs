
/*
 * guid: $GUID$
 * file: RetryLimitException
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 10:03:26
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
    using System.Runtime.Serialization;

    public sealed class RetryLimitException : Exception
    {
        public RetryLimitException(int retry, int limit)
            : base(string.Concat("Try more than limit, limited ", limit, ", retried ", retry, "."))
        {
            Limit = limit;
        }

        private RetryLimitException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        public int Limit { get; private set; }
    }
}
