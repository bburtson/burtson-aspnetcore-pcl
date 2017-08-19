using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;

namespace BurtsonUnitTests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void RemoveAnyRemovesElements()
        {
            var valuesToRemove = new[] { "first", "", "," };

            var result = "first secondthird".RemoveAny(valuesToRemove);

            Assert.AreEqual(" secondthird", result);
        }

        [TestMethod]
        public void RemoveAnyRemovesCaseSensitive()
        {
            var valuesToRemove = new[] { "first", "last, ", "KeEp" };

            var result = "last, keepfirst".RemoveAny(valuesToRemove);


            Assert.AreNotEqual("KEEP", result);

            Assert.AreEqual("keep", result);
        }

        [TestMethod]
        public void RemoveAnyRemovesIgnoreCase()
        {
            var valuesToRemove = new[] { ",....", " ", "ing", "" };

            var result = "UNit TestInG,....Rocks!".RemoveAny(valuesToRemove, StringComparison.CurrentCultureIgnoreCase);
           
            Assert.AreEqual("UNitTestRocks!", result);
        }

        [TestMethod]
        public void ReplaceAnyShouldReplaceAllItemsCaseSensitive()
        {
            var valuesToReplace = new[] { "", "can't", "shouldn't", "won't"};

            var result = "I can't win, I shouldn't try, I won't do something great".ReplaceAny("will", valuesToReplace); 

            Assert.AreEqual("I will win, I will try, I will do something great", result);
        }

        [TestMethod]
        public void ReplaceAnyShouldReplaceWithComparisonOptions()
        {
            var valuesToReplace = new[] { "", "CaN not", "should Not", "will Not" };

            var result = "I can Not win, I should Not try, I will not do something amazing".ReplaceAny("WILL", valuesToReplace, StringComparison.CurrentCultureIgnoreCase);

            Assert.AreEqual("I WILL win, I WILL try, I WILL do something amazing", result);
        }
    }
}
