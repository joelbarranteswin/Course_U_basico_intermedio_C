using System;

namespace Primer_proyecto
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("Hello");
            Console.WriteLine("what is your name?");
            string name = Console.ReadLine();
            Console.WriteLine("Dear " + name + " Welcome here");
            Console.WriteLine("How old are you?");
            int ascii = Console.Read();
            Console.WriteLine("veo que tienes " + ascii + " años");
            Console.ReadKey();
            */

            /*
            byte x = 12;
            short y = 13;
            int z = 4;
            long t = 5;
            float k = 6;
            y = x;
            z = y;
            y = z;
            */

            double x = 24;
            int y = 12;
            //y = (int)x;
            //y = Convert.ToInt32(x)
            float z = 6;
            char t = '+';
            string s = x.ToString();
            
            s = z.ToString();
            s = t.ToString();
            s = y.ToString();
            Console.WriteLine(s);
            Console.ReadLine();

            ///comentarios


        }
    }
}
