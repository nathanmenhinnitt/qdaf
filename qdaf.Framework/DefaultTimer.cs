namespace qdaf.Framework
{
    using System;
    using System.Timers;

    public class DefaultTimer : ITimer
    {
        private readonly Lazy<Timer> _timer;
        private bool _stopped;

        public DefaultTimer()
        {
            _timer = new Lazy<Timer>(() => new Timer { AutoReset = false });
            _stopped = false;
        }

        public int Timeout { get; set; }

        public void Start()
        {
            _timer.Value.Interval = Timeout;
            _timer.Value.Elapsed += delegate
                {
                    if (_stopped)
                    {
                        return;
                    }

                    Stop(true);
                };

            _timer.Value.Start();

        }

        public void Stop()
        {
            Stop(false);
        }

        private void Stop(bool raiseEvent)
        {
            var thisStopped = !_stopped;
            _stopped = true;
            if (!_timer.IsValueCreated)
            {
                return;
            }

            if (raiseEvent && thisStopped)
            {
                var handler = TimeoutOccurred;
                if (handler != null)
                {
                    handler(this, new TransactionException(TransactionStaus.Timeout));
                }
            }

            _timer.Value.Stop();
            _timer.Value.Dispose();

        }

        public event EventHandler<TransactionException> TimeoutOccurred;
    }
}
