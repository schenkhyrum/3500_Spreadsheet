using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SS;
using SpreadsheetUtilities;
using System.IO;


namespace SpreadsheetTests
{
    [TestClass]
    public class SpreadsheetTests
    {
        [TestMethod()]
        public void TestGetVersion()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.Save("local.txt");
            Assert.AreEqual("default", s.GetSavedVersion("local.txt"));
        }
        [TestMethod()]
        public void TestWriteFile()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "2.0");
            s.Save("local.xml");
            Assert.IsTrue(File.Exists("local.xml"));
        }
        //Tests by not throwing 
        [TestMethod()]
        public void TestConstructFromFile()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "2.0");
            s.Save("local.xml");
            Assert.IsTrue(File.Exists("local.xml"));
            AbstractSpreadsheet fromFile = new Spreadsheet("local.xml", s => true, s => s.ToUpper(), "default");
        }
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestConstructFromWrongVersion()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "2.0");
            s.Save("local.xml");
            Assert.IsTrue(File.Exists("local.xml"));
            AbstractSpreadsheet fromFile = new Spreadsheet("local.xml", s => true, s => s.ToUpper(), "1");
        }

        //Test the changed property
        [TestMethod()]
        public void TestChangedSpreadsheet()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "astring");
            Assert.IsTrue(s.Changed);
            s.SetContentsOfCell("A1", "2.0");
            Assert.IsTrue(s.Changed);
            s.Save("save.txt");
            Assert.IsFalse(s.Changed);
        }
        //The next three tests make sure that GetCellValue returns the correct object types
        [TestMethod()]
        public void TestGetCellValueString()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "Words");
            Assert.IsTrue(s.GetCellContents("A1") is string);

        }
        [TestMethod()]
        public void TestGetCellValueDouble()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "2.0");
            Assert.IsTrue(s.GetCellContents("A1") is double);

        }
        [TestMethod()]
        public void TestGetCellValueFormula()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=_X1");
            Assert.IsTrue(s.GetCellContents("A1") is Formula);

        }
        
        //test that the contents are specifically equal to their values
        [TestMethod]
        public void TestGetCellContentsString()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            
            s.SetContentsOfCell("A1", "This is a string.");
            
            Assert.AreEqual("This is a string.", s.GetCellContents("A1"));
        }
        // a new spreadsheet is a referenceable object
        [TestMethod]
        public void TestNoArgConstructor()
        {
            Spreadsheet s = new Spreadsheet();
            Assert.IsTrue(s != null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetCellContentsNullString()
        {
            String nullString = null;
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", nullString);
        }
        
        [TestMethod]
        public void TestSetCellContentsDouble()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "2.0");
            Assert.AreEqual(2.0, s.GetCellContents("A1"));
        }
        [TestMethod]
        public void TestValuesSavedAsObjects()
        {
            object formula = new Formula("x1");
            Assert.IsTrue(formula is Formula);
            object value = 2.0;
            Assert.IsTrue(value is double);
        }
        [TestMethod]
        public void TestSetCellContentsString()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("X1", "this is a string");
            object contents = s.GetCellContents("X1");
            Assert.AreEqual("this is a string", contents);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidCellNameEmpty()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("", "contents");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidCellNameBeginWithNumbers()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("2x", "contents");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameGetContents()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetCellContents("2");
        }
        [TestMethod]
        public void TestGetNamesOfAllEmptyCells()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "stuff");
            s.SetContentsOfCell("A2", "2.0");
            HashSet<string> cells = new HashSet<string>() { "A1", "A2" };
            HashSet<string> names = new HashSet<string>(s.GetNamesOfAllNonemptyCells());
            foreach (string name in names)
                Assert.IsTrue(cells.Contains(name));
            foreach (string cell in cells)
                Assert.IsTrue(names.Contains(cell));
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetDoubleCellInvalidName()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("2", "2.0");
        }
        [TestMethod]
        public void TestSetExistingCellContents()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "contents");
            s.SetContentsOfCell("A1", "2.0");
            Assert.AreEqual(2.0, s.GetCellContents("A1"));
            s.SetContentsOfCell("A1", "=X1");
            Assert.AreEqual(new Formula("X1"), s.GetCellContents("A1"));
        }
        [TestMethod]
        public void TestReplaceInCellWithDependees()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=X1");
            Assert.IsTrue(s.SetContentsOfCell("X1", "2.0").Contains("A1"));
            s.SetContentsOfCell("A1", "2.0");
            Assert.IsFalse(s.SetContentsOfCell("X1", "2.0").Contains("A1"));
        }
        [TestMethod]
        public void TestSetStringWithDependees()
        {
            //show that changing a dependee of A1 doesn't affect A1 after it doesn't hold a Formula as contents any loger.
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=X1");
            Assert.IsTrue(s.SetContentsOfCell("X1", "2.0").Contains("A1"));
            s.SetContentsOfCell("A1", "change with a string");
            Assert.IsFalse(s.SetContentsOfCell("X1", "2.0").Contains("A1"));
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetFormulaInvalidName()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("*", "s");
        }
    }
}
