using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Business.Models
{
    public class CalculatorModel
    {

        public Dictionary<string, uint> OperatorPriorities = new Dictionary<string, uint>
        {
            { "+",  0 },
            { "-", 0 },
            { "*", 0 },
            { "/", 0 },
        };

        private enum Operator
        {
            Addition,
            Subtraction,
            Multiplication,
            Division
        }

        private string[] _operators = new string[] {"+", "-", "*", "/"};

        private string? LastInput { get; set; }

        public List<string> InputExpression = new List<string>();

        public bool OperationsAreAvaible => double.TryParse(LastInput, out double result) ? true : false;

        public double? Result { get; set; }

    }
}
