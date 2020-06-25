using System;

namespace CSharpReference
{
    class Program
    {
        static void Main(string[] args)
        {
            var thousand = 1_000;   // numeric literal
            var binary = 0b0000_0000_0000_0000_0000_1111;   // numeric literal

            ParseTwoStrings(
                "8",
                "2",
                "3",
                "Div",
                out int var1,
                out int var2,
                out _,
                out CalcOperation operation);    // out variables and discards

            var operationObj = new Operation(operation);
            operationObj.Execute(var1, var2, 0);
            var (operationCopy, lastResult) = operationObj;    // deconstructor
            Console.WriteLine($"last operation: {operationCopy}, last result:{lastResult}");
        }

        public static void ParseTwoStrings(
            object o1,
            object o2,
            object o3,
            string s4,
            out int i1,
            out int i2,
            out int i3,
            out CalcOperation op)
        {
            i1 = 0;
            if (o1 is string s1)    // pattern matching with is
                int.TryParse(s1, out i1);

            i2 = 0;
            switch (o2)
            {
                case int i:    // pattern Matching
                    i2 = i;
                    break;
                case bool b when b == true:    // pattern matching with when
                    i2 = 1;
                    break;
                case string s2:
                    int.TryParse(s2, out i2);
                    break;
                case null:
                    i2 = 0;
                    break;
            }
            i2 = i2 != 0 ? i2 : throw new ArgumentException("can't be 0");

            i3 = 0;
            if (o3 is string s3)    // pattern matching with is
                int.TryParse(s3, out i3);

            if (!Enum.TryParse(s4, true, out op))
                op = CalcOperation.Unknown;
        }

        public class Operation
        {
            private CalcOperation _operation;
            private double _lastResult;
            public Operation(CalcOperation operation) => _operation = operation;    // expression-bodied members
            ~Operation() => Console.WriteLine("finalize");    // expression-bodied members
            public string OperationStringName => _operation.ToString();    // expression-bodied members
            public void Deconstruct(out CalcOperation operation, out double lastResult)    // deconstructors
            {
                operation = _operation;
                lastResult = _lastResult;
            }

            public double Execute(int operand1, int operand2, int op3)
            {
                double r = 0;
                var operands = (op1: operand1, op2: operand2);
                var namedOperands = (FirstOperand: operand1, SecondOperand: operand2);    // named tuples;
                (int someOp1, int someOp2) = GenerateOperandsTuple(operand1, operand2);    // implicitly deconstruction pattern

                switch (_operation)
                {
                    case CalcOperation.Div:
                        r = Div(operand1, operand2);
                        break;
                }

                Console.WriteLine($"Operands: {operands.Item1} and {namedOperands.SecondOperand}");
                return _lastResult = r;
                int Div(int v1, int v2) => v1 / v2;    //Local methods
            }
        }

        static (int operand1, int operand2) GenerateOperandsTuple(int operand1, int operand2) => (operand1, operand2);    // return multiple values without resorting to out parameters

        public enum CalcOperation
        {
            Unknown,
            Div,
        }
    }
}
