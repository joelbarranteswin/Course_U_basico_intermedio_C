using System;

namespace Primer_proyecto
{
    class Program
    {
        static void Main(string[] args)
        {
            /* INTRO */

            /*
            string name = Console.ReadLine();
            int ascii = Console.Read();
            Console.WriteLine(ascii);
            */


            /* DECLARANDO VARIABLES */
            byte x = 12;
            short y = 13;
            int z = 4;
            long t = 5;
            float k = 6;
            y = x;
            z = y;
   
            
            string s = z.ToString();



            /* COMENTARIOS */
            
            ///comentarios
            /* comentario */

            /* CONVERSIONES */
            double temperature = 17.83;
            int clime;

            //conversion explicita
            clime = (int)temperature;
            
            //converison implicita
            int habitantes = 10000;
            long poblacion = habitantes;
            //Console.WriteLine($"The value is {poblacion}");


            /*CONVERSIONES*/
            int a = 12;
            string b = a.ToString();

            string c = "12";
            int d = Convert.ToInt32(c);
            int e = int.Parse(c);

            //Console.WriteLine($" {b + c}, {a + d + e}");


            /* CONSTANTES */
            // son siempre mayusculas y estos valores no se pueden cambiar
            const int MAX_HABITANTES = 1000000;
            const int MIN_HABITANTES = 0;
            //Console.WriteLine("{0}", MAX_HABITANTES, MIN_HABITANTES);

            const double PI = 3.1415;
            double radio = double.Parse(Console.ReadLine());
            //double area = radio * radio * PI;
            double area = Math.Pow(radio, 2) * Math.PI;
            Console.WriteLine("{0}", area);

        }
    }
}
