using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambdas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InputParameterWithLambda3();
        }

        static void Lambda()
        {
            Func<int, int> square = x => x * x;
            Console.WriteLine(square(5));
        }
        static void StatementLambda()
        {
            Action<string> greet = name =>
            {
                string greeting = $"Hello {name}!";
                Console.WriteLine(greeting);
            };
            greet("World");
        }
        static void InputParameterWithLambda()
        {
            //Func<double, double> cube = x => x * x * x;
            Func<double, double> cube = (double x) => (x * x * x);
            Console.WriteLine(cube(3));
        }
        static void InputParameterWithLambda2()
        {
            Func<int, int, bool> testForEquality = (int x,int y) => (x == y);
            Console.WriteLine(testForEquality(3,3));
        }
        static void InputParameterWithLambda3()
        {
            Func<int, string, bool> isTooLong = (int x, string s) => (s.Length > x);
            Console.WriteLine(isTooLong(3, "yeah"));
        }
        static void InputParameterWithLambda4()
        {
            //for C# 9.0
            //Func<int, int, int> constant = (_, _) => 42;
            //Console.WriteLine(constant(3, 4));
        }

        

    }
}
