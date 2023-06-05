
namespace TaskIntroduction;

class Program
{
    static void Main(string[] args)
    {
        DoAllTasks();

    }

    static void DoAllTasks()
    {
        var task1 = Task.Run(() => 
        {
            ExecuteTask();
        });
        Console.WriteLine();

        task1.Wait();

        var task2 = Task.Run(() =>
        {
            ExecuteTask2();
        });
        Console.WriteLine();

        //Task.WaitAll(task1, task2); // wait for all tasks to finish
        //Task.WaitAny(task1, task2); // wait for any task to finish and then task3 can continue between task 1 and task 2

        task2.Wait();

        var task3 = Task.Run(() =>
        {
            ExecuteTask3();
        });
        Console.WriteLine();

        



    }

    static void ExecuteTask()
    {
        for (int i = 0; i < 5; i++)
        {
            var myThread = Thread.CurrentThread.ManagedThreadId;
            Thread.Sleep(1000);
            Console.WriteLine($"Thread {myThread} is running in Task 1.");
        }
    }

    static void ExecuteTask2()
    {
        for (int i = 0; i < 5; i++)
        {
            var myThread = Thread.CurrentThread.ManagedThreadId;
            Thread.Sleep(500);
            Console.WriteLine($"Thread {myThread} is running in task 2.");
        }
    }

    static void ExecuteTask3()
    {
        for (int i = 0; i < 5; i++)
        {
            var myThread = Thread.CurrentThread.ManagedThreadId;
            Thread.Sleep(500);
            Console.WriteLine($"Thread {myThread} is running in task 3.");
        }
    }
}