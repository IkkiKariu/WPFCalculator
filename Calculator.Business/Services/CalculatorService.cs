using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Calculator.Business.Models;

namespace Calculator.Business.Services
{
    public class CalculatorService
    {
        private CalculatorModel _model = new CalculatorModel();

        public double? Calculate(List<string> tokenList)
        {
            List<string> polNotationExp = ToPolishNotation(tokenList);


            Stack<double> numberStack = new Stack<double>();

            foreach (string token in polNotationExp)
            {

                if (double.TryParse(token, out double result))
                {
                    numberStack.Push(double.Parse(token));
                    continue;
                }

                double operand1;
                double operand2;

                switch (token)
                {
                    case "+":
                        operand1 = numberStack.Pop();
                        operand2 = numberStack.Pop();

                        numberStack.Push(operand2 + operand1);
                        break;
                    case "-":
                        operand1 = numberStack.Pop();
                        operand2 = numberStack.Pop();

                        numberStack.Push(operand2 - operand1);
                        break;
                    case "*":
                        operand1 = numberStack.Pop();
                        operand2 = numberStack.Pop();

                        numberStack.Push(operand2 * operand1);
                        break;
                    case "/":
                        operand1 = numberStack.Pop();
                        operand2 = numberStack.Pop();

                        numberStack.Push(operand2 / operand1);
                        break;
                    /*case "mod":
                        operand1 = numberStack.Pop();
                        operand2 = numberStack.Pop();

                        numberStack.Push(operand2 % operand1);
                        break;*/
                    default:
                        Console.WriteLine("Unexpected token");
                        return null;
                }
            }
            return numberStack.Pop();
        }

        public List<string>? ToPolishNotation(List<string> tokenList)
        {
            Stack<string> _operatorStack = new Stack<string>();

            _operatorStack.Clear();

            List<string> output = new List<string>();

            foreach (string token in tokenList)
            {
                if (double.TryParse(token, out double res))
                {
                    output.Add(token);
                    continue;
                }

                if (_operatorStack.Count == 0)
                {
                    _operatorStack.Push(token);
                    continue;
                }

                switch (token)
                {
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                        while (_operatorStack.Count != 0)
                        {
                            /*if (_operatorStack.Peek() == "(")
                            {
                                break;
                            }*/

                            if (_model.OperatorPriorities[token] > _model.OperatorPriorities[_operatorStack.Peek()])
                            {
                                break;
                            }
                            else
                            {
                                output.Add(_operatorStack.Pop());
                            }
                        }

                        _operatorStack.Push(token);
                        break;
                    /*case "(":
                        _operatorStack.Push(token);
                        break;
                    case ")":
                        while (_operatorStack.Peek() != "(")
                        {
                            output.Add(_operatorStack.Pop());
                        }
                        _operatorStack.Pop();
                        break;*/
                    default:
                        Console.WriteLine("Unexpected token");
                        return null;
                }

            }
            while (_operatorStack.Count != 0)
            {
                output.Add(_operatorStack.Pop());
            }
            return output;

        }
    }
}
