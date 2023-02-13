///<summary>
///     Author: Samuel Hancock - u0966409
///     
///     Date:1/17/2020
///     
///     This Library is designed to build a formula object represented by operators(+, -, /, *),
///     words or strings representing variables and floating point values. Values can be represented in scientific notation.
///     The Formula can be evaluated at any time using Formula.Evaluate(Func<string, double) where Func is a look up funciton
///     designed to provide the values for any string variables. 
///     
///     I pledge that I did this work myself.
///</summary>
//This class is extremely broken and I owe a great code debt upon it.
// May the holy savior of coding come and save me from this weight.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SpreadsheetUtilities
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  The allowed symbols are non-negative numbers written using double-precision
    /// floating-point syntax (without unary preceeding '-' or '+'); 
    /// variables that consist of a letter or underscore followed by 
    /// zero or more letters, underscores, or digits; parentheses; and the four operator
    /// symbols +, -, *, and /.  
    /// 
    /// Spaces are significant only insofar that they delimit tokens.  For example, 
    ///"xy" is
    /// a single variable, "x y" consists of two variables "x" and y; "x23" is a 
    ///single variable; 
    /// and "x 23" consists of a variable "x" and a number "23".
    /// 
    /// Associated with every formula are two delegates:  a normalizer and a 
    ///validator.The
    /// normalizer is used to convert variables into a canonical form, and the 
    ///validator is used
    /// to add extra restrictions on the validity of a variable (beyond the standard 
    ///requirement
    /// that it consist of a letter or underscore followed by zero or more letters, 
    ///underscores,
    /// or digits.)  Their use is described in detail in the constructor and method 
    ///comments.
    /// </summary>
    public class Formula
    {

        private List<string> tokens;
        private string _formula;
        private readonly List<string> validOperands = new List<string>() { "/", "*", "+", "-" };
        private HashSet<string> variables;
        private Func<string, string> N;
        private Func<string, bool> V;
        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression 
        /// written as
        /// described in the class comment.  If the expression is syntactically invalid,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer is the identity function, and the associated 
        /// validator maps every string to true.  
        /// </summary>
        public Formula(String formula) :
            this(formula, s => s, s => true)
        {
        }
        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically incorrect,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer and validator are the second and third 
        /// parameters, respectively.  
        /// 
        /// If the formula contains a variable v such that normalize(v) is not a legal variable, 
        /// throws a FormulaFormatException with an explanatory message. 
        /// 
        /// If the formula contains a variable v such that isValid(normalize(v)) is false,
        /// throws a FormulaFormatException with an explanatory message.
        /// 
        /// Suppose that N is a method that converts all the letters in a string to upper case, and
        /// that V is a method that returns true only if a string consists of one letter followed
        /// by one digit.  Then:
        /// 
        /// new Formula("x2+y3", N, V) should succeed
        /// new Formula("x+y3", N, V) should throw an exception, since V(N("x")) is false
        /// new Formula("2x+y3", N, V) should throw an exception, since "2x+y3" is syntactically incorrect.
        /// </summary>
        public Formula(String formula, Func<string, string> normalize, Func<string,
                bool> isValid)
        {
            //save the functions
            N = normalize;
            V = isValid;
            _formula = formula;//save the formula string

            int leftP = 0;//a running count for left parentheses

            int rightP = 0;//a running count for right parentheses


            if (formula.Length == 0)//OneTokenRule
                throw new FormulaFormatException("Cannot Evaluate an Empty formula");

            tokens = GetTokens(normalize(formula)).ToList();

            variables = GetVariables().ToHashSet();

            //starting token rule
            if (tokens[0] != "(" && !variables.Contains(N(tokens[0])) && !double.TryParse(tokens[0], out double notcounted))
                throw new FormulaFormatException($"The first object of a formula must be an open parenth, valid variable or valid value.");

            //ending token rule
            if (tokens.Last() != ")" && !variables.Contains(N(tokens.Last())) && !double.TryParse(tokens.Last(), out double erroneous))
                throw new FormulaFormatException($"The last object of a formula must be a close parenth, valid variable or valid value.");

            //make sure every token is a valid token. Also, remove whitespace to keep any funny business at bay.
            for (int i = 0; i < tokens.Count; i++)
            {
                string token = tokens[i];

                //nix the whitespace
                if (token == " ")
                    tokens.RemoveAt(i);//I should not need to update token, this will just make a loop over all the checks with token = " ".

                //maintaining balanced parentheses
                if (token.Equals("("))
                    leftP++;
                if (token.Equals(")"))
                    rightP++;

                //Right parentheses rule
                if (rightP > leftP)
                    throw new FormulaFormatException("Too many closing parentheses, adjust opening parenthese to" +
                        "fit your formula.");

                //specific token rule
                if (!variables.Contains(normalize(token)) && token != "(" && token != ")" && !validOperands.Contains(token)
                    && !double.TryParse(token, out double outvalue))
                {
                    throw new FormulaFormatException($"{token} is not a valid piece of a formula. Refactor the formula.");
                }



                if (i < tokens.Count - 1)//keep from checking the following variable at a null position
                {
                    //following rule
                    if (token == "(" || validOperands.Contains(token))
                    {
                        if (!double.TryParse(tokens[i + 1], out double unnecessary) && tokens[i + 1] != "(" && !variables.Contains(N(tokens[i + 1])))
                        {
                            throw new FormulaFormatException($"Any token following an open parenth, or operator must be a value," +
                                $"variable or another open parenth{tokens[i + 1]} does not meat that requirement.");
                        }
                    }
                    //Extra following rule
                    if (double.TryParse(token, out double unused) || token == ")" || variables.Contains(N(token)))
                    {
                        if (tokens[i + 1] != ")" && !validOperands.Contains(tokens[i + 1]) && !validOperands.Contains(token)
                            && token != "(")
                        {
                            throw new FormulaFormatException($"A value, ')' or a variable must be followed by an operator or " +
                                $"another close parentheses. {tokens[i]} and {tokens[i + 1]} do not meet these requirements.");
                        }
                    }
                }
            }




            if (leftP != rightP)//Balanced parentheses
                throw new FormulaFormatException("There must be Equivalent counts of opening and closing parentheses.");
        }
        /// <summary>
        /// Evaluates this Formula, using the lookup delegate to determine the values of
        /// variables.  When a variable symbol v needs to be determined, it should be looked up
        /// via lookup(normalize(v)). (Here, normalize is the normalizer that was passed to
        /// the constructor.)
        /// 
        /// For example, if L("x") is 2, L("X") is 4, and N is a method that converts all the letters
        /// in a string to upper case:
        /// 
        /// new Formula("x+7", N, s => true).Evaluate(L) is 11
        /// new Formula("x+7").Evaluate(L) is 9
        /// 
        /// Given a variable symbol as its parameter, lookup returns the variable's value
        /// (if it has one) or throws an ArgumentException (otherwise).
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating
        /// this Formula, the value is returned.  Otherwise, a FormulaError is returned.
        /// The Reason property of the FormulaError should have a meaningful explanation.
        ///
        /// This method should never throw an exception.
        /// </summary>
        public object Evaluate(Func<string, double> lookup)
        {
            Stack<double> values = new Stack<double>();
            Stack<string> operators = new Stack<string>();


            foreach (var element in tokens)
            {
                if (!string.IsNullOrEmpty(element))
                {
                    //check if is integer
                    if (double.TryParse(element, out double number))
                    {
                        if (CheckTopOperator(new string[] { "*", "/" }, operators))
                        {
                            try
                            {
                                values.Push(PopOnceAndOperate(number, values, operators));
                            }
                            catch (FormulaFormatException e)
                            {
                                return new FormulaError(e.Message);
                            }
                        }
                        else values.Push(number);
                    }
                    //check if element is variable
                    if (variables.Contains(N(element)))
                    {
                        try
                        {
                            double tokenAsNumber = lookup(element);
                            if (CheckTopOperator(new string[] { "*", "/" }, operators))
                            {

                                values.Push(PopOnceAndOperate(tokenAsNumber, values, operators));
                            }
                            else values.Push(tokenAsNumber);
                        }
                        catch (Exception e)
                        {
                            return new FormulaError(e.Message);
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
                        {
                            try
                            {
                                values.Push(PopTwiceAndOperate(values, operators));
                            }
                            catch (FormulaFormatException e)
                            {
                                return new FormulaError(e.Message);
                            }
                        }

                    }




                }

            }
            //All operations have been performed and variables is exhausted
            if (operators.Count == 0)
            {
                if (values.Count == 1)
                {
                    operators.Clear();
                    return values.Pop();

                }
                else throw new ArgumentException("No solution found, ensure the expression's accuracy");
            }
            //variables is exhausted but there is one more operation
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
        /// Enumerates the normalized versions of all of the variables that occur in this 
        /// formula.  No normalization may appear more than once in the enumeration, even
        /// if it appears more than once in this Formula.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
        /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
        /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
        /// </summary>
        public IEnumerable<String> GetVariables()
        {

            HashSet<string> newVariables = new HashSet<string>();
            foreach (string token in tokens)
            {
                if (!double.TryParse(token, out double number) && !validOperands.Contains(token) && token != "("
                    && token != ")")
                {
                    if (V(token))
                        newVariables.Add(N(token));
                    else
                        throw new FormulaFormatException($"{token} is not a valid variable");
                }

            }
            return newVariables;
        }
        /// <summary>
        /// Returns a string containing no spaces which, if passed to the Formula
        /// constructor, will produce a Formula f such that this.Equals(f).  All of the
        /// variables in the string should be normalized.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
        /// new Formula("x + Y").ToString() should return "x+Y"
        /// </summary>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            //constructor has stripped white space from tokens list
            //constructor has already filled variables hashset
            foreach (string token in tokens)
            {
                if (variables.Contains(N(token)))
                    stringBuilder.Append(N(token));
                else if (double.TryParse(token, out double result))
                {
                    stringBuilder.Append(result.ToString());
                }
                else
                    stringBuilder.Append(token);
            }

            return stringBuilder.ToString();
        }
        /// <summary>
        /// If obj is null or obj is not a Formula, returns false.  Otherwise, reports
        /// whether or not this Formula and obj are equal.
        /// 
        /// Two Formulae are considered equal if they consist of the same tokens in the
        /// same order.  To determine token equality, all tokens are compared as strings 
        /// except for numeric tokens and variable tokens.
        /// Numeric tokens are considered equal if they are equal after being "normalized" 
        /// by C#'s standard conversion from string to double, then back to string. This 
        /// eliminates any inconsistencies due to limited floating point precision.
        /// Variable tokens are considered equal if their normalized forms are equal, as 
        /// defined by the provided normalizer.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        ///  
        /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
        /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
        /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
        /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this is null ^ obj is null)
                return false;
            if (obj is Formula)//only comparable token by token if they are both formulas
            {
                if (this.ToString().GetHashCode() != obj.ToString().GetHashCode())
                    //if the hashcode is note equal, then the formula not equal
                    return false;
                List<string> tokensOfObj = GetTokens(obj.ToString()).ToList();
                for (int i = 0; i < this.tokens.Count; i++)
                {
                    //if both tokens at i are doubles, parse to double and back to string to compare equality
                    if (double.TryParse(tokens[i], out double num))
                    {
                        if (double.TryParse(tokensOfObj[i], out double numOfObj))
                            if (num.ToString() != numOfObj.ToString())
                                return false;
                    }
                    else if (!this.N(tokens[i]).Equals(N(tokensOfObj[i])))
                        return false;
                }
                return true;
            }
            return false;

        }
        //use hashcode for the operators
        /// <summary>
        /// Reports whether f1 == f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return true.  If one is
        /// null and one is not, this method should return false.
        /// </summary>
        public static bool operator ==(Formula f1, Formula f2)
        {
            if (f1 is null && f2 is null)
                return true;
            if (f1 is null ^ f2 is null)
                return false;
            return f1.Equals(f2);
        }
        /// <summary>
        /// Reports whether f1 != f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return false.  If one is
        /// null and one is not, this method should return true.
        /// </summary>
        public static bool operator !=(Formula f1, Formula f2)
        {
            if (f1 is null && f2 is null)
                return false;
            if (f1 is null ^ f2 is null)
                return true;
            return !f1.Equals(f2);
        }
        /// <summary>
        /// Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
        /// case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two
        /// randomly-generated unequal Formulae have the same hash code should be extremely small.
        /// </summary>
        public override int GetHashCode()
        {
            //to string normalizes all of the variables and strips the white space.
            //Any Formula that goes through that process will be equal if it has the same characters in the same order.
            String formula = this.ToString();
            return formula.GetHashCode();
        }
        /// <summary>
        /// Given an expression, enumerates the tokens that compose it.  Tokens are left paren;
        /// right paren; one of the four operator symbols; a string consisting of a letter or underscore
        /// followed by zero or more letters, digits, or underscores; a double literal; and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
            String spacePattern = @"\s+";
            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                      lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);
            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern,
      RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }
        }

        //private helper methods
        /// <summary>
        /// Used when an integer is found in the substring. Pops each stack and operates against the passed and popped value.
        /// Private helper method.
        /// Should only be used to stage values to the values stack.
        /// </summary>
        /// <param name="tokenAsNumber">The integer that was found in the substring immediately after an operator</param>
        /// <returns>Evaluation of the operation</returns>
        private static double PopOnceAndOperate(double tokenAsNumber, Stack<double> values, Stack<string> operators)
        {
            if (values.Count > 0 && operators.Count > 0)
            {
                try
                {
                    var operand = operators.Pop();
                    var value = values.Pop();//what if the value is a variable - variables will be "looked up" before put onto value stack

                    if (operand.Equals("/"))
                    {
                        if (tokenAsNumber == 0)
                            throw new FormulaFormatException("Cannot Divide by Zero.");
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
        private static double PopTwiceAndOperate(Stack<double> values, Stack<string> operators)
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
    /// <summary>
    /// Used to report syntactic errors in the argument to the Formula constructor.
    /// </summary>
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message)
            : base(message)
        {
        }
    }
    /// <summary>
    /// Used as a possible return value of the Formula.Evaluate method.
    /// </summary>
    public struct FormulaError
    {
        /// <summary>
        /// Constructs a FormulaError containing the explanatory reason.
        /// </summary>
        /// <param name="reason"></param>
        public FormulaError(String reason)
            : this()
        {
            Reason = reason;
        }
        /// <summary>
        ///  The reason why this FormulaError was created.
        /// </summary>
        public string Reason { get; private set; }
    }
}