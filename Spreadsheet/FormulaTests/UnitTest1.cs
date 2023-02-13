using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SpreadsheetUtilities;

namespace FormulaTests
{
    [TestClass]
    public class FormulaTests
    {
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestNoArgStruct()
        {
            Formula formula = new Formula("");
        }

        [TestMethod]
        public void TestStructsSingleVar()
        {
            Formula f1 = new Formula("5");
            Assert.AreEqual(5.0, f1.Evaluate(s => 0));
            Formula f2 = new Formula("x");
            Assert.AreEqual(5.0, f2.Evaluate(s => 5));

        }

        [TestMethod]
        public void TestAdders()
        {
            Assert.AreEqual(10.0, new Formula("5+5").Evaluate(s => 0));
            Assert.AreEqual(0.0, new Formula("5-5").Evaluate(s => 0));
            Assert.AreEqual(10.0, new Formula(" 5+5").Evaluate(s => 0));
            Assert.AreEqual(0.0, new Formula(" 5-5").Evaluate(s => 0));
        }

        [TestMethod]
        public void TestMultipliers()
        {
            Assert.AreEqual(10.0, new Formula("2 * 5").Evaluate(s => 0));
            Assert.AreEqual(10.0, new Formula("(2 * 5)").Evaluate(s => 0));
        }






    }
}
