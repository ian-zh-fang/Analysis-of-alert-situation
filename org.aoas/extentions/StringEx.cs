
/*
 * guid: $GUID$
 * file: StringEx
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/1 13:42:15
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
    using System.Text.RegularExpressions;

    public static class StringEx
    {
        /// <summary>
        /// 目标字符串不存在时，返回 true ; 否则，返回 false .
        /// </summary>
        public static Boolean IsNull(this String input)
        {
            return null == input;
        }

        /// <summary>
        /// 目标字符串不存在，或者长度为 0 时，返回 true ; 否则，返回 false .
        /// </summary>
        public static Boolean IsEmpty(this String input)
        {
            return string.IsNullOrEmpty(input);
        }

        /// <summary>
        /// 目标字符串不存在，或者长度为 0，或者全部为空白字符时，返回 true ; 否则，返回 false .
        /// </summary>
        public static Boolean IsWhitespaces(this String input)
        {
            return string.IsNullOrWhiteSpace(input);
        }
    }
}
