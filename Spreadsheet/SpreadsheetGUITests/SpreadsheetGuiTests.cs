using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpreadsheetGUITests
{
    [TestClass]
    public class SpreadsheetGuiTests
    {
 

        [TestMethod]
        public void TestGetCol()
        {
            //Tests that the uppercase characters give an appriate column number

            Assert.AreEqual(0, new SpreadsheetGUI.SimpleSpreadsheetGUI().GetCol('A'));
            Assert.AreEqual(25, new SpreadsheetGUI.SimpleSpreadsheetGUI().GetCol('Z'));

        }
    }
}
