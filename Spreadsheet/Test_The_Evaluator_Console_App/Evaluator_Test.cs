///
///<summary>
///     Author: Samuel Hancock - u0966409
///     
///     Date:1/17/2020
///     
///     This is an app for testing the utility of the FormulaEvaluator library.   
///     I pledge that I did this work myself.
///</summary>
using System;
using SpreadsheetUtilities;

namespace Test_The_Evaluator_Console_App
{
    class Evaluator_Test
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">This variable remains unused.</param>
        static void Main(string[] args)
        {

            //Test take in value, return same value
            TestAndPrint("1", 1);
            TestAndPrint("(1)", 1);
            //Test addition
            TestAndPrint("1+1", 2);
            TestAndPrint("(1+1)", 2);
            //Test subtraction
            TestAndPrint("1-1", 0);
            TestAndPrint("(1-1)", 0);
            //Test Division
            TestAndPrint("4/2", 2);
            TestAndPrint("(4/2)", 2);
            //Test Multiplication
            TestAndPrint("6*8", 48);
            TestAndPrint("1*1", 1);
            //Compound formula
            TestAndPrint("(1+1)/(4/2)", 1);
            //Test the exclusion of operator or operand
            TestAndPrint("1+", 1);
            TestAndPrint("1+1)", 2);
            TestAndPrint("1 + 1", 2);//Can my evaluator handle white space?

        }
        private static void TestAndPrint(string equation, int expected)
        {
            try
            {
                if (Evaluator.Evaluate(equation, (name) => 0) == expected)
                    Console.WriteLine($"'{equation}' successfully returned {expected}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"{equation} did not evaluate to the expected: {expected} \n {e.Message}");
            }
        }
    }
}
