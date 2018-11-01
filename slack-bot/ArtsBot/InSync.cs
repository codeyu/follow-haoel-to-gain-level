
using System;
using System.Threading;
using System.Runtime.CompilerServices;
namespace ArtsBot
{
    public class InSync : IDisposable
    {
        private readonly TimeSpan WaitTimeout = TimeSpan.FromSeconds(15);

        private readonly ManualResetEventSlim waiter;
        private readonly string message;

        public InSync([CallerMemberName] string message = null)
        {
            this.message = message;
            this.waiter = new ManualResetEventSlim();
        }

        public void Proceed()
        {
            this.waiter.Set();
        }

        public void Dispose()
        {
            if(this.waiter.Wait(this.WaitTimeout))
            {
                Console.WriteLine($"Took too long to do '{this.message}'");
            } 
        }
    }
}