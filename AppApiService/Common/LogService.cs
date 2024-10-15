using System.Collections.Concurrent;

namespace AppApiService.Common
{
    public class LogService
    {
        private readonly ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();
        private readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(0);
        public LogService()
        {
            Task.Run(ProcessLogQueueAsync); // 启动异步日志处理
        }

        public void EnqueueLog(string logEntry)
        {
            var currentThreadId = Thread.CurrentThread.ManagedThreadId;
            logQueue.Enqueue(logEntry);
            Console.WriteLine($"current Log is {logEntry}, currentThreadId is {currentThreadId}");
            Console.WriteLine($"current Queue length is {logQueue.Count()}");
            semaphoreSlim.Release(); // 通知有新的日志加入队列
            Console.WriteLine($"release semaphore!");

        }

        private async Task ProcessLogQueueAsync()
        {
            while (true)
            {
                Console.WriteLine($"wait semaphore!");
                var currentCount = semaphoreSlim.CurrentCount;
                Console.WriteLine($"currentCount is {currentCount} !!!");

                await semaphoreSlim.WaitAsync(); // 等待信号
                if (logQueue.TryDequeue(out var logEntry))
                {
                    Console.WriteLine($"after release semaphore current Queue length is {logQueue.Count()}");
                    try
                    {
                        Console.WriteLine(logEntry);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

    }
}
