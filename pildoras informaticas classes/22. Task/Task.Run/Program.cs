
namespace TaskIntroduction;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Main thread is starting.");
        Task task1 = Task.Run(() => ExecuteTask());
        Console.WriteLine();

        Task task2 = task1.ContinueWith(ExecuteOtherTask);

        Console.WriteLine("Main thread is done.");

    }

    static void ExecuteTask()
    {
        for (int i = 0; i < 10000; i++)
        {
            var myThread = Thread.CurrentThread.ManagedThreadId;
            Thread.Sleep(100000);
            Console.WriteLine($"Thread {myThread} is running.");
            Console.WriteLine();
        }
        
    }

    static void ExecuteOtherTask(Task obj)
    {
        for (int i = 0; i < 10000; i++)
        {
            var myThread = Thread.CurrentThread.ManagedThreadId;
            Thread.Sleep(100000);
            Console.WriteLine($"Thread {myThread} is running.");
            Console.WriteLine();
        }
    }
}
