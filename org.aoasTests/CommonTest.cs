using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.aoas;

namespace org.aoasTests
{
    [TestClass]
    public class CommonTest
    {
        [TestMethod]
        public void TestIdentityGen()
        {
            var str = "171218155900000000";
            var rawVal = long.Parse(str);
            var planVal = 171218155900000000L;

            Assert.AreEqual<long>(planVal, rawVal);
        }

        [TestMethod]
        public void TestTimestampIdentityGen()
        {
            // ID 生成规则：当前秒级时间戳+类型+4位随机实数

            const long planTimestamp = 1513584523L;
            const string planType = "00";
            const string planRandom = "1234";
            const long planValue = 1513584523001234L;

            var rawStr = string.Concat(planTimestamp, planType, planRandom);
            var rawVal = long.Parse(rawStr);

            Assert.AreEqual<long>(planValue, rawVal);
        }
    }
}
