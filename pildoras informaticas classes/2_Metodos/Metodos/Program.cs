using System;

namespace Metodos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* METODOS */
            // son funciones que se pueden llamar desde otras funciones
            // el VOID se declara cuando una funcion no devuelve nada

            mensajePantalla();
            Console.WriteLine("Hola desde el Main");
            sumaNumeros(2, 3);
            // el paso de parametros puede ser por valor o por referencia

            int suma = sumaNumeros2(2, 3);
            Console.WriteLine(suma);

            double division = divideNumeros(2, 3);
            Console.WriteLine(division);

            double division2 = divideNumeros2(2, 3);
            Console.WriteLine(division2);

            Console.WriteLine(Suma(2, 3, 5));

            Console.WriteLine(Product(2, 3, 5));

        }

        

        static void mensajePantalla()
        {
            Console.WriteLine("Hola desde el Metodo");
        }

        static void sumaNumeros(int num1, int num2)
        {
            Console.WriteLine(num1 + num2);
        }

        

        static int sumaNumeros2(int num1, int num2)
        {
            return num1 + num2;
        }
        static double divideNumeros(int num1, double num2)
        {
            return num1 / num2;
        }

        /* METODO EN UNA LINEA */
        static double divideNumeros2(double num1, int num2) => num1 / num2;


        /* CONTEXTO, AMBITO Y ALCANCE SON LO MISMO */
        // si las variables se declaran en ambito de clase para que los metodos tengan acceso a ellas (atributos)
        // no se leen de arriba a abajo
        

        int numero2 = 10;
        void primerMetodo()
        {
            Console.WriteLine(numero1);
        }
        int numero1 = 5;        
        void segundoMetodo()
        {
            Console.WriteLine(numero2);
        }

        /* SOBRECARGA DE METODOS */
        // 2 metodos con el mismo nombre pero con distintos parametros
        static int Suma(int num1, int num2) => num1 + num2;
        static double Suma(int num1, double num2) => num1 + num2;
        static int Suma(int num1, int num2, int num3) => num1 + num2 + num3;

        /* QUICK ACTIONS AND REFACTORING */
        private static bool Product(int v1, int v2, int v3)
        {
            throw new NotImplementedException();
        }

        /* PARAMETROS OPCIONALES */
        // uno de los parametro se le asigna un valor por defecto
        void metodoParametroOpcional()
        {
            int num1 = 0;
            int num2 = 20;
            int num3 = 40;
            Console.WriteLine(SumaParametroOpcional(num1,num2));
        }
        
        private int SumaParametroOpcional(int num1, int num2, int num3 = 0) => num1 + num2 + num3;
        private int SumaParametroOpcional(int num1, int num2) => num1 + num2;

    }
}
