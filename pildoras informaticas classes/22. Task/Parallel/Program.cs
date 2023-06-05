
namespace TaskIntroduction;

class Program
{
    private static int acumulador = 0;
    static void Main(string[] args)
    {
        //for (int i = 0; i < 100; i++)
        //{
        //    DoAllTasks(i);
        //    Console.WriteLine($"Acumulador vale {acumulador} y el hilo es {Thread.CurrentThread.ManagedThreadId}");
        //}

        /*Parallel*/
        //ayuda  aque podamos realizar iteraciones de forma paralela

        //Parallel.For(0, 100, i =>
        //{
        //    DoAllTasks(i);
        //    Console.WriteLine($"Acumulador vale {acumulador} y el hilo es {Thread.CurrentThread.ManagedThreadId}");
        //});

        Parallel.For(0, 100, DoAllTasks);


    }

    static void DoAllTasks(int data)
    {
       if ((acumulador % 2) == 0)
        {
            acumulador += data;
            Thread.Sleep(100);
        }
        else
        {
            acumulador -= data;
            Thread.Sleep(100);
        }

    }
    
}