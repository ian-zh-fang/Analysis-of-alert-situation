
/*
 * guid: $GUID$
 * file: XmlConfigurationTests
 * clr: 4.0.30319.42000
 * machine: DESKTOP-9902656
 * framework: 4.5
 ***********************************
 *
 * author: fangzhen
 * create: 2017/11/3 15:34:34
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

using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace org.aoas.config.Tests
{
    [TestClass()]
    public class XmlConfigurationTests
    {
        [TestMethod()]
        public void SaveAsTest()
        {
            var cfg = new MyConfig();
            Assert.IsNotNull(cfg);

            cfg.Pro = Guid.NewGuid().GetHashCode();
            foreach (var item in cfg.Pie)
            {
                ((XmlConfigurationItem)item).Name = Guid.NewGuid().ToString("N");
            }

            foreach (var item in cfg.Mom)
            {
                foreach (var tt in ((XmlConfigurationMomItem)item))
                {
                    ((XmlConfigurationItem)tt).Name = Guid.NewGuid().ToString("N");
                }
            }

            cfg.SaveAs();
        }

        private class XmlConfigurationItem : XmlConfigurationElement
        {
            public XmlConfigurationItem()
                :base()
            { }

            public string Name { get; set; }

            public int Id { get; set; }
        }

        private class XmlConfigurationMomItem : XmlConfigurationArray
        {
            public XmlConfigurationMomItem()
                :base()
            { }

            protected override XmlConfigurationElement OnGetChildElement(XmlReader reader)
            {
                return new XmlConfigurationItem();
            }

            [attributes.Alias("name")]
            public string Name { get; set; }
        }

        private class XmlConfigurationMom : XmlConfigurationArray
        {
            public XmlConfigurationMom()
                :base("item")
            { }

            protected override XmlConfigurationElement OnGetChildElement(XmlReader reader)
            {
                return new XmlConfigurationMomItem();
            }
        }

        private class XmlConfigurationPie : XmlConfigurationArray
        {
            public XmlConfigurationPie()
                :base()
            { }

            protected override bool OnDeserializeUnrecognizeElement(string name, XmlReader reader)
            {
                return base.OnDeserializeUnrecognizeElement(name, reader);
            }

            protected override XmlConfigurationElement OnGetChildElement(XmlReader reader)
            {
                return new XmlConfigurationItem();
            }
        }

        private class MyConfig:XmlConfiguration
        {
            public MyConfig()
                :base("sectionName", "configuration", "myconfig.config")
            { }

            [attributes.Alias("pro")]
            public XmlConfigurationProperty<int> Pro { get; set; }

            [attributes.Alias("mom")]
            public XmlConfigurationMom Mom { get; set; }

            [attributes.Alias("pie")]
            public XmlConfigurationPie Pie { get; set; }
        }
    }
}
