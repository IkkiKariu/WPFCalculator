using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Calculator.Business.Services;

namespace WPFCalculator.ViewModels
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private CalculatorService _service = new CalculatorService();

        public event PropertyChangedEventHandler? PropertyChanged;


        private string[] _operators = new string[] { "+", "-", "*", "/" };

        public string? LastInput { get; set; } = "";


        private string _inputExpresion = "";
        public string InputExpression
        {
            set
            {
                _inputExpresion = value;
                OnPropertyChanged(nameof(InputExpression));
            }
            get
            {
                return _inputExpresion;
            }
        }

        public bool OperationsAreAvaible => double.TryParse(LastInput, out double result) ? true : false;

        //public double? Result { get; set; }

        public void PressDigit (string digit)
        {
            LastInput = digit;
            InputExpression += digit;
        }

        public void PressOperator(string operation)
        {
            if (OperationsAreAvaible)
            {
                LastInput = operation;
                InputExpression += operation;
            }
        }

        public void PressClear()
        {
            LastInput = "";
            InputExpression = "";
        }

        public void PressEquals()
        {
            if (!_operators.Contains(LastInput) && InputExpression != "")
            {
                var result = _service.Calculate(ParseToTokenList());

                InputExpression = result.ToString();
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<string> ParseToTokenList()
        {
            List<string> tokenList = new List<string>();

            var expression = InputExpression;

            string number = "";

            string lastToken = "";

            /*foreach (var token in expression)
            {
                var iteration = expression.IndexOf(token);

                if(expression.IndexOf(token) == 0 && token == '-')
                {
                    number += token;
                    lastToken = token.ToString();

                    continue;
                }

                if (double.TryParse(token.ToString(), out double res))
                {
                    number += token;
                }
                else
                {
                    if (!(_operators.Contains(lastToken)))
                    {
                        if (number != "")
                        {
                            tokenList.Add(number);
                            number = "";
                        }

                        tokenList.Add(token.ToString());
                    }
                    else
                    {
                        number += token;
                    }
                }

                lastToken = token.ToString();
            }
            tokenList.Add(number);*/


            for(int i = 0; i < expression.Length; i++)
            {
                if (i == 0 && expression[i] == '-')
                {
                    number += expression[i];
                    lastToken = expression[i].ToString();

                    continue;
                }

                if (double.TryParse(expression[i].ToString(), out double res))
                {
                    number += expression[i];
                }
                else
                {
                    if (!(_operators.Contains(lastToken)))
                    {
                        if (number != "")
                        {
                            tokenList.Add(number);
                            number = "";
                        }

                        tokenList.Add(expression[i].ToString());
                    }
                    else
                    {
                        number += expression[i];
                    }
                }

                lastToken = expression[i].ToString();
            }

            tokenList.Add(number); 
            
            return tokenList;
        }
    }
}
