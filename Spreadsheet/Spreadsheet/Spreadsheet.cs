///<summary>
///     Author: Samuel Hancock - u0966409
///     
///     Date:2/17/2020
///<para>
///     This Library was designed to work implement the AbstractSpreadsheet API by referencing the existing
///     DepenencyGraph and Formula libraries.
///</para>     
///    
/// <para>
///     This library is built with a cell structure that can hold data as a string, double, or a Formula
///     object.
/// </para>
/// 
///     I pledge that I did this work myself.
///</summary>
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using SpreadsheetUtilities;
using System.Xml;
using System.IO;

namespace SS
{
    public class Spreadsheet : AbstractSpreadsheet
    {

        protected class Cell
        {
            public object Contents { get; set; }
            public string Name { get; private set; }
            public Cell(string name, object content)
            {

                Contents = content;
                Name = name;

            }



        }
        private DependencyGraph Graph;
        private Dictionary<string, Cell> namesToCells;

        public override bool Changed { get; protected set; }

        public Spreadsheet() : this((name) => true, (word) => word, "default")
        {


        }
        public Spreadsheet(Func<string, bool> ValidityFunction, Func<string, string> NormalizingFunction, string VersionName)
            : base(ValidityFunction, NormalizingFunction, VersionName)
        {

            Graph = new DependencyGraph();
            namesToCells = new Dictionary<string, Cell>();

        }
        public Spreadsheet(string PathToFile, Func<string, bool> ValidityFunction, Func<string, string> NormalizingFunction, string VersionName)
            : this(ValidityFunction, NormalizingFunction, VersionName)
        {
            try
            {
                ReadXML(PathToFile);

            }
            catch (Exception)
            {
                throw new SpreadsheetReadWriteException($"{PathToFile} does not exist");
            }


        }

        public override object GetCellContents(string name)
        {
            if (CheckNameValidity(Normalize(name)) && IsValid(Normalize(name)))
            {
                if (namesToCells.ContainsKey(Normalize(name)))
                    return namesToCells[Normalize(name)].Contents;
                else
                    return "";
            }

            throw new InvalidNameException();
        }

        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            return namesToCells.Keys;
        }

        protected override IList<string> SetCellContents(string name, double number)
        {


            if (namesToCells.ContainsKey(name))
            {
                namesToCells[name].Contents = number;
            }
            else
                namesToCells.Add(name, new Cell(name, number));
            if (Graph.HasDependees(name))
            {
                //A string will not contain variables and will be 
                //void of dependees.
                Graph.ReplaceDependees(name, new HashSet<string>());
            }
            //All Cells dependent, directly and indirectly.
            return new List<string>(GetCellsToRecalculate(name));

        }

        protected override IList<string> SetCellContents(string name, string text)
        {

            if (namesToCells.ContainsKey(name))
            {
                namesToCells[name].Contents = text;
            }
            else
                namesToCells.Add(name, new Cell(name, text));
            if (Graph.HasDependees(name))
            {
                //A string will not contain variables and will be 
                //void of dependees.
                Graph.ReplaceDependees(name, new HashSet<string>());
            }
            //All Cells dependent, directly and indirectly.
            return new List<string>(GetCellsToRecalculate(name));

        }

        protected override IList<string> SetCellContents(string name, Formula formula)
        {
            /*
             * ToDo: Get Cells to recalculate and throw its circular exception before
             * Setting the contents of the cell(no change before Circular validity is checked)
             */
            if (!CheckNameValidity(name))
                throw new InvalidNameException();
            if (formula is null)
                throw new ArgumentNullException();
            if (namesToCells.ContainsKey(name))
            {
                namesToCells[name].Contents = formula;
            }
            else
                namesToCells.Add(name, new Cell(name, formula));

            Graph.ReplaceDependees(name, formula.GetVariables());

            //All Cells dependent, directly and indirectly.
            return new List<string>(GetCellsToRecalculate(name));
        }

        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            if (name is null)
                throw new ArgumentNullException();
            if (!CheckNameValidity(name))
                throw new InvalidNameException();
            return Graph.GetDependents(name);
        }
        /// <summary>
        /// <para>This Method checks the validity of a string based on the AbstractSpreadsheet API specification.
        /// A name must begin with an underscore or a letter. A name may only contain letters, undercores and numbers.
        /// A name must end in with a number.</para>
        /// <para>Additionally, a the name must have a length greater than 0 and not equal a null. These conditions will throw
        /// a InvalidNameException</para>
        /// </summary>
        /// <exception cref = "InvalidNameException">Thrown when the name does not meet the requirements.</exception>
        /// <param name="name">The name to be checked against the rules.</param>
        /// <returns>Boolean referencing the validity of the name.</returns>
        private bool CheckNameValidity(string name)
        {
            //if not null, empty, and the first character is letter or underscore, return true
            if (name is null || name.Length == 0)
                return false;
            if (char.IsDigit(name[0]))
                return false;
            if (!char.IsLetter(name[0]) && name[0] != '_')
                return false;
            //last character must be letter, digit or underscore
            return char.IsLetterOrDigit(name[^1]) || name[^1].Equals("_");
        }
        public override IList<string> SetContentsOfCell(string name, string content)
        {
            string normalizeName = Normalize(name);
            if (CheckNameValidity(normalizeName) == false)
            {
                throw new InvalidNameException();
            }
            if (IsValid(normalizeName) == false)
                throw new InvalidNameException();

            //TODO: if cell exists and content is empty, the cell should be deleted.
            //      Group with other public methods.
            if (content is null)
                throw new ArgumentNullException($"{content} is not valid content or it is null.");
            if (content == string.Empty)
            {
                if (Graph.HasDependees(normalizeName))
                {
                    Graph.ReplaceDependees(normalizeName, new HashSet<string>());
                }
                namesToCells.Remove(normalizeName);
                List<string> changedCells = new List<string>(GetCellsToRecalculate(normalizeName));
                return changedCells;
            }

            if (double.TryParse(content, out double cellValue))
            {
                Changed = true;
                return SetCellContents(normalizeName, cellValue);
            }
            else if (content.StartsWith("="))
            {
                try
                {
                    //Formula Set needs to catch circular exception before returning its list
                    Formula f = new Formula(content.Substring(1), Normalize, IsValid);
                    List<string> AffectedCells = new List<string>(SetCellContents(normalizeName, f));
                    Changed = true;
                    return AffectedCells;

                }
                catch (Exception)
                {
                    throw;
                }


            }
            else
            {
                Changed = true;
                return SetCellContents(normalizeName, content);
            }



        }

        public override string GetSavedVersion(string filename)
        {
            using (XmlReader reader = XmlReader.Create(filename))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "spreadsheet":
                                return reader.GetAttribute("version");
                        }

                    }
                }

            }
            throw new SpreadsheetReadWriteException("Could not find Version info");
        }

        public override void Save(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new SpreadsheetReadWriteException("Filename is not supported");
            try
            {
                WriteXML(filename);
            }
            catch (Exception e)
            {
                throw new SpreadsheetReadWriteException("There was a problem writing to this file. Please check'" +
                    "your file name and version information. " + e.Message);
            }
            Changed = false;
        }

        public override object GetCellValue(string name)
        {
            if (!namesToCells.ContainsKey(name))
                return "";
            object contents = GetCellContents(name);
            if (contents is Formula)
            {
                Formula element = (Formula)contents;
                return element.Evaluate(s => (double)GetCellValue(s));
                //Dictionary<string, double> cellsToValues = new Dictionary<string, double>();
                //foreach (string variable in Graph.GetDependees(name))
                //{
                //    object dependeeContents = GetCellValue(variable);

                //    if (double.TryParse(dependeeContents.ToString(), out double cellValue))
                //        cellsToValues.Add(variable, cellValue);
                //    else return new FormulaError($"A dependee in {element} could not be parsed to a double and could not be evaluated.");
                //}
                //return element.Evaluate(s => cellsToValues[s]);

            }
            else if (double.TryParse(contents.ToString(), out double ContentValue))
                return ContentValue;
            else
                return contents.ToString();

        }
        private void ReadXML(string filename)
        {
            StringBuilder thisfile = new StringBuilder();
            using (XmlReader reader = XmlReader.Create(filename))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "spreadsheet":
                                if (reader.GetAttribute("version") != Version)
                                    throw new SpreadsheetReadWriteException($"Versions did not match. This file is" +
                                        $"of version {reader.GetAttribute("version")}");
                                break;

                            case "cell":

                                break;

                            case "name":
                                reader.Read();
                                string name = reader.Value;
                                while (!reader.IsStartElement())
                                    reader.Read();
                                reader.Read();
                                string contents = reader.Value;
                                SetContentsOfCell(name, contents);
                                break;

                        }

                    }
                }
            }

        }
        private void WriteXML(string filename)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";

            using (XmlWriter writer = XmlWriter.Create(filename, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", Version);
                foreach (string name in GetNamesOfAllNonemptyCells())
                {
                    var contents = GetCellContents(name);
                    writer.WriteStartElement("cell");
                    writer.WriteStartElement("name");
                    writer.WriteString(name);
                    writer.WriteEndElement();
                    writer.WriteStartElement("contents");
                    if (contents is Formula)
                        writer.WriteString($"={contents.ToString()}");
                    else if (contents is double)
                        writer.WriteString(contents.ToString());
                    else if (contents is string)
                        writer.WriteString(contents.ToString());
                    writer.WriteEndElement();
                    writer.WriteEndElement();

                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }




        }
    }


}
