var counter = 0;
var dummy = new Object();

var t1 = Task.Run(Increment);
var t2 = Task.Run(Increment);

Task.WaitAll(t1, t2);

Console.WriteLine($"The value of counter: {counter}");

void Increment()
{
    for (int i = 0; i < 1_000_000_000; i++)
    {
        if (i > 0 && i % 100_000_000 == 0)
            Console.WriteLine($"{Thread.CurrentThread.Name}: {i} was reached");
        lock (dummy)
        {
            counter++;
        }
    }

    Console.WriteLine($"{Thread.CurrentThread.Name}: The job is completed ");
}

