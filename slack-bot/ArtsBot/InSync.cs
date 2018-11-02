
using System;
using System.Threading;
using System.Runtime.CompilerServices;
namespace ArtsBot
{
    public class InSync : IDisposable
    {
        private readonly TimeSpan _waitTimeout = TimeSpan.FromSeconds(30);

        private readonly ManualResetEventSlim _waiter;
        private readonly string _message;

        public InSync([CallerMemberName] string message = null)
        {
            this._message = message;
            this._waiter = new ManualResetEventSlim();
        }

        public void Proceed()
        {
            this._waiter.Set();
        }

        public void Dispose()
        {
            if(!this._waiter.Wait(this._waitTimeout))
            {
                Console.WriteLine($"Took too long to do '{this._message}'");
            } 
        }
    }
}