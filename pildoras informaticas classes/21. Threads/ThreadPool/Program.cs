using System;
using System.Threading;

using static System.Net.Mime.MediaTypeNames;


namespace PoolOfThreads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Grupo de hilos */
            // se crea muchos hilas a ejecutar de manera concurrente
            // se vuelve a utilizar los hilos


            //Threads();
            PoolThreads();


        }
        private static void Threads()
        {

            for (int i = 0; i < 500; i++)
            {
                //se crea un hilo por cada iteracion
                Thread t = new Thread(ExecuteThreads);
                t.Start();
            }
        }

        private static void ExecuteThreads()
        {
            Console.WriteLine($"Thread n°: {Thread.CurrentThread.ManagedThreadId}, ha comenzado su tarea");
            Thread.Sleep(1000);
            Console.WriteLine($"Thread n°: {Thread.CurrentThread.ManagedThreadId}, ha terminado su tarea");
        }

        private static void PoolThreads()
        {
            /* Grupo de hilos */
            // se crea muchos hilas a ejecutar de manera concurrente
            // se vuelve a utilizar los hilos
            for (int i = 0; i < 10000000; i++)
            {
                // Pool of Threads
                //se puede poner un numero maximo de pools (la cantidad de grupo)
                System.Threading.ThreadPool.QueueUserWorkItem(ExecutePoolThreads, i);
            }
            Console.WriteLine();
        }



        private static void ExecutePoolThreads(Object obj)
        {
            int nTarea = (int)obj;
            Console.WriteLine($"Thread n°: {Thread.CurrentThread.ManagedThreadId}, ha comenzado su tarea y es la Tarea: {nTarea}");
            Thread.Sleep(5000);
            Console.WriteLine($"Thread n°: {Thread.CurrentThread.ManagedThreadId}, ha terminado su tarea y es la Tarea: {nTarea}");
        }
        
    }

}
