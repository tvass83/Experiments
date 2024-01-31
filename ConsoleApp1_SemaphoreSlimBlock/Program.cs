namespace ConsoleApp1_SemaphoreSlimBlock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var t = new AsyncWorker();

            Console.WriteLine("Press ENTER to call dispose");
            Console.ReadLine();

            t.Dispose();
        }
    }

    public class AsyncWorker : IDisposable
    {
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        private Task _workerTask;

        public AsyncWorker()
        {
            _workerTask = Task.Run(() => WorkerLoop(_cts.Token));
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();

            // Changing the order of these lines fixes a potential (yet very likely) deadlock, as disposing the semaphore changes its internal state
            // see https://github.com/dotnet/runtime/blob/release/6.0/src/libraries/System.Private.CoreLib/src/System/Threading/SemaphoreSlim.cs#L728-L730
            _semaphore.Dispose();
            _workerTask.Wait();
        }

        private async Task WorkerLoop(CancellationToken cts)
        {
            while (!cts.IsCancellationRequested)
            {
                Console.WriteLine("Waiting for semaphore");

                try
                {
                    // Seems cancelling cts is not respected all the time if semaphore is disposed before waiting on the worker task
                    // The reason is there's a race condition for these 2 method calls:
                    // Thread 1: SemaphoreSlim.Dispose
                    // Thread 2: SemaphoreSlim.RemoveAsyncWaiter
                    await _semaphore.WaitAsync(cts);
                }
                catch (OperationCanceledException) { }

                Console.WriteLine("Semaphore let thru");
            }
        }
    }
}
