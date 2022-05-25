using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Grupo de hilos */
            // se crea muchos hilas a ejecutar de manera concurrente
            // se vuelve a utilizar los hilos
            for (int i = 0; i < 100; i++)
            {
                //se crea un hilo por cada iteracion
                //Thread t = new Thread(EjecutarTarea);
                //t.Start();

                // pool of threads
                //se puede poner un numero maximo de pools (la cantidad de grupo)
                System.Threading.ThreadPool.QueueUserWorkItem(EjecutarTarea, i);
                
            }
            Console.ReadLine();            
        }
        static void EjecutarTarea(Object obj)
        {
            int nTarea = (int)obj;
            Console.WriteLine($"Thread n°: {Thread.CurrentThread.ManagedThreadId}, ha comenzado su tarea y es la Tarea: {nTarea}");
            Thread.Sleep(1000);
            Console.WriteLine($"Thread n°: {Thread.CurrentThread.ManagedThreadId}, ha terminado su tareay es la Tarea: {nTarea}");
        }
        
    }

}
