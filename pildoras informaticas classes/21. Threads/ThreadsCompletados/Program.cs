using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadsCompletados
{
    internal class Program
    {   
        static void Main(string[] args)
        {
            var tareaTerminada = new TaskCompletionSource<bool>();
            
            var hilo1 = new Thread(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Hilo 1");
                    Thread.Sleep(1000);
                }
                tareaTerminada.TrySetResult(true); // da por temrinada una tarea
            });


            var hilo2 = new Thread(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Hilo 2");
                    Thread.Sleep(1000);
                }
                tareaTerminada.TrySetResult(true); // da por temrinada una tarea
            });

            var hilo3 = new Thread(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Hilo 3");
                    Thread.Sleep(1000);
                }
            });

            /* TaskCompletionSource */
            //Ejemplo 1: que el hilo1 termine y que el hilo 2  y 3 se ejecuten como el procesador quiera
            //hilo1.Start();
            //var Resultado = tareaTerminada.Task.Result; // verifica que termine la tarea
            //hilo2.Start();
            //hilo3.Start();

            Console.WriteLine("---------------------------------------------");
            //Ejemplo 2: que el hilo 1 y 2 se ejecute como el procesador pueda y luego inicia el hilo 3
            hilo1.Start();
            var Resultado1 = tareaTerminada.Task.Result;
            hilo2.Start();
            var Resultado2 = tareaTerminada.Task.Result; // verifica que termine la tarea
            hilo3.Start();
        }
    }
}
