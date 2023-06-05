
namespace CancelationToken;

class Program
{
    private static int acumulador = 0;
    static void Main(string[] args)
    {
        CancellationTokenSource token = new CancellationTokenSource();
        CancellationToken cancelationToken = token.Token;
        
        Task task = Task.Run(() => DoAllTasks(cancelationToken));

        for (int i=0; i<100; i++)
        {
            acumulador += 30;
            Thread.Sleep(1000);
            
            if (acumulador > 100)
            {
                token.Cancel();
                break;
            }
        }
        Thread.Sleep(1000);
        Console.WriteLine("Acumulador vale " + acumulador);
    }
    
    static void Execute()
    {
        
    }

    static void DoAllTasks(CancellationToken token)
    {
        for (int i = 0; i < 100; i++)
        {
            acumulador ++;
            Thread.Sleep(100);
            Console.WriteLine($"el Thread es {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"{acumulador}");

            if (token.IsCancellationRequested)
            {
                Console.WriteLine("Cancelation Requested");
                acumulador = 0;
                break;
            }
        }
    }
}
