using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (var item in list)
            {
                Console.WriteLine($"{item} saludo desde el thread (hilo) 1");
                Thread.Sleep(500);
            }

            Thread hilo2 = new Thread(start: MetodoSaludo2);
            hilo2.Start();
            hilo2.Join(); //Join ayuda a determinar el orden de ejecucion de los hilos

            Thread hilo3 = new Thread(start: MetodoSaludo3);
            hilo3.Start();
            hilo3.Join(); //Join ayuda a determinar el orden de ejecucion de los hilos

        }
        static void MetodoSaludo2()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (var item in list)
            {
                Console.WriteLine($"{item} saludo desde el thread (hilo) 2");
                Thread.Sleep(500);

            }
        }
        static void MetodoSaludo3()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (var item in list)
            {
                Console.WriteLine($"{item} saludo desde el thread (hilo) 3");
                Thread.Sleep(500);

            }
        }
    }
}
