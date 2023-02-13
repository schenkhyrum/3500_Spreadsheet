///
///<summary>
///     Author: Samuel Hancock - u0966409
///     
///     Date:1/17/2020
///     
///     This Library will take in a mathematical expression and return an appropriate value. This Library does not support
///     Expressions that contain floating point variables. Divisors with remainders will not return with the remainder but will return
///     the quotient. All expressions must be complete by including open and close parentheses and no operator may be used without 
///     corresponding integer values.
///     
///     I pledge that I did this work myself.
///</summary>
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SpreadsheetUtilities

{
    /// <summary>
    /// Has methods to evaluate the expression. Dependent on stacks and infix notation.
    /// </summary>
    public class Evaluator
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="variable_name"></param>
        /// <returns></returns>
        public delegate int Lookup(String variable_name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="variableEvaluator"></param>
        /// <returns>The integer value of the expression.</returns>
        /// 
        public static int Evaluate(String expression, Lookup variableEvaluator)
        {
            Stack<int> values = new Stack<int>();
            Stack<string> operators = new Stack<string>();
            string[] substrings = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");

            foreach (var element in substrings)
            {
                if (!string.IsNullOrEmpty(element))
                {
                    //check if is integer
                    if (int.TryParse(element, out int number))
                    {
                        if (CheckTopOperator(new string[] { "*", "/" }, operators))
                        {
                            values.Push(PopOnceAndOperate(number, values, operators));
                        }
                        else values.Push(number);
                    }
                    //check if element is variable
                    if (char.IsLetter(element, 0))
                    {
                        if (char.IsDigit(element, element.Length - 1))
                        {
                            int tokenAsNumber = variableEvaluator(element);
                            if (CheckTopOperator(new string[] { "*", "/" }, operators))
                            {
                                values.Push(PopOnceAndOperate(tokenAsNumber, values, operators));
                            }
                            else values.Push(tokenAsNumber); 
                        }
                    }

                    //Check addition operator type
                    if (element.Equals("+") || element.Equals("-"))
                    {
                        //check top operator for plus or minus then push operator onto operator stack
                        if (CheckTopOperator(new string[] { "-", "+" }, operators))
                            values.Push(PopTwiceAndOperate(values, operators));
                        operators.Push(element);
                    }
                    //single operator push conditionals
                    // every instance only requires a push to the operators stack
                    if (element.Equals("/") || element.Equals("*") || element.Equals("("))
                        operators.Push(element);

                    if (element.Equals(")"))
                    {
                        if (CheckTopOperator(new string[] { "-", "+" }, operators))
                            values.Push(PopTwiceAndOperate(values, operators));
                        if (CheckTopOperator(new string[] { "(" }, operators))
                            operators.Pop();
                        else
                        {
                            operators.Clear();
                            values.Clear();
                            throw new ArgumentException("Add an '(' open parenthesis or remove the extraneous ')' close parenthesis");
                        }
                        if (CheckTopOperator(new string[] { "*", "/" }, operators))
                            values.Push(PopTwiceAndOperate(values, operators));

                    }




                }

            }
            //All operations have been performed and substrings is exhausted
            if (operators.Count == 0)
            {
                if (values.Count == 1)
                {
                    operators.Clear();
                    return values.Pop();

                }
                else throw new ArgumentException("No solution found, ensure the expression's accuracy");
            }
            //Substrings is exhausted but there is one more operation
            else if (operators.Count == 1 && CheckTopOperator(new string[] { "-", "+" }, operators) && values.Count == 2)
            {
                values.Push(PopTwiceAndOperate(values, operators));
                return values.Pop();
            }
            //No value or operators, throw an error to let the user know their expression did not evaluate
            else
            {
                operators.Clear();
                values.Clear();
                throw new ArgumentException("No solution found, ensure the expression's accuracy");
            }


        }
        /// <summary>
        /// Used when an integer is found in the substring. Pops each stack and operates against the passed and popped value.
        /// Private helper method.
        /// Should only be used to stage values to the values stack.
        /// </summary>
        /// <param name="tokenAsNumber">The integer that was found in the substring immediately after an operator</param>
        /// <returns>Evaluation of the operation</returns>
        private static int PopOnceAndOperate(int tokenAsNumber, Stack<int> values, Stack<string> operators)
        {
            if (values.Count > 0 && operators.Count > 0)
            {
                try
                {
                    var operand = operators.Pop();
                    var value = (int)values.Pop();//what if the value is a variable - variables will be "looked up" before put onto value stack

                    if (operand.Equals("/"))
                    {
                        if (tokenAsNumber == 0)
                            throw new ArgumentException("Cannot Divide by Zero.");
                        return value / tokenAsNumber;
                    }
                    else if (operand.Equals("*"))
                        return value * tokenAsNumber;
                    else if (operand.Equals("+"))
                        return value + tokenAsNumber;
                    else
                        return value - tokenAsNumber;

                }
                catch (InvalidOperationException)
                {
                    throw new ArgumentException("Invalid expression. Ensure there are enough variables for the given operators.");
                }
            }
            return tokenAsNumber;

        }
        /// <summary>
        /// Pulls off two values and an operator from the respective stacks. Private helper method that relies on
        /// PopOnceAndOperate
        /// </summary>
        /// <returns>Value of a portion of the expression</returns>
        private static int PopTwiceAndOperate(Stack<int> values, Stack<string> operators)
        {

            if (values.Count < 2)
                throw new ArgumentException("Invalid expression. Ensure that there are enough variables for the given operators.");
            var secondValue = values.Pop();
            return PopOnceAndOperate(secondValue, values, operators);


        }
        /// <summary>
        /// Determines if the top operator matches the passed operator. Conditionals depend on this method to check the operators
        /// </summary>
        /// <param name="checkForTheseOperands">A list of operators in string form e.g. "/", "*"</param>
        /// <returns>True if the top operator matches the passed operator</returns>
        private static Boolean CheckTopOperator(string[] checkForTheseOperands, Stack<string> operators)
        {
            if (operators.Count > 0)
            {
                var topOperator = operators.Peek();
                foreach (string operand in checkForTheseOperands)
                {
                    if (operand.Equals(topOperator)) return true;
                }

            }
            return false;
        }
    }

}
