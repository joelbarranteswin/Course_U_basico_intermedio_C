
namespace TaskIntroductory;
class Program
{
    static void Main(string[] args)
    {
        Task task = new Task(ExecuteTask);
        task.Start();
        
        Task task2 = new Task(() => 
        {
            for(int i = 0; i < 10000; i++)
            {
                var myThread = Thread.CurrentThread.ManagedThreadId;
                Thread.Sleep(100000);
                Console.WriteLine($"Thread {myThread} executing from Main.");
            }
        });
    
    
        task2.Start();

        Console.WriteLine("Main thread is done.");
    }


    static void ExecuteTask()
    {
        for (int i = 0; i < 10000; i++)
        {
            var myThread = Thread.CurrentThread.ManagedThreadId;
            Thread.Sleep(100000);
            Console.WriteLine($"Thread {myThread} is running.");
            
        }
    }
}