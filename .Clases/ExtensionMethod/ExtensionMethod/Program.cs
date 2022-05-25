using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethod
{
    public delegate void Accion();
    public delegate int Funcion(int n1, int n2);
    public delegate bool Predicado(Funcion f);
    internal class Program
    {
        static void Main(string[] args)
        {
            /* METODOS EXTENDIDOS */
            // son metodos que se agregan a una clase o a una interfaz
            // para poder ser utilizados por una clase o interfaz
            // sin tener que heredar de ella
            // se agregan a la clase o interfaz con la palabra
            // static y se agregan con la palabra extension
            String s = "Turkey Baster!";
            s.GobbleGobble();

            /* DELEGADOS PROPIOS */
            // son metodos que se agregan a una clase o a una interfaz
            Accion D1 = Mensaje;
            D1();
            Funcion D2 = Suma;
            Console.WriteLine(D2(n1: 2, n2: 3));
            Predicado D3 = Callback;
            Console.WriteLine(D3(f: D2));

            /* DELEGADOS ACTION FUNC PREDICATE */
            // son metodos que se agregan a una clase o a una interfaz
            Console.WriteLine("DELEGADOS ACTION FUNC PREDICATE");
            Action DaCT = Mensaje;
            DaCT();
            Func<int, int, int> DFunc = Suma;
            Console.WriteLine(DFunc(2, 3));
            Predicate<Func<int, int, int>> DPred = Callback;
            Console.WriteLine(DPred(DFunc));

        }

        static void Mensaje() => Console.WriteLine("Hello World!");
        static int Suma(int n1, int n2) => n1 + n2;
        static bool Callback(Funcion f) => f(1, 2) == 3 ? true : false;
        static bool Callback(Func<int, int, int> f) => f(2, 2) == 4 ? true : false;

    }
    public static class StringExtensions
    {
        public static void GobbleGobble(this string s)
        {
            Console.Out.WriteLine("Gobble Gobble, " + s);
        }
    }

}
