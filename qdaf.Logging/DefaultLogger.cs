namespace qdaf.Logging
{
    using Core;
    using NLog;
    using System;

    public class DefaultLogger : IQdafLogger
    {
        private readonly Lazy<Logger> _logger;

        public DefaultLogger()
        {
            _logger = new Lazy<Logger>(LogManager.GetCurrentClassLogger);
        }

        public void Trace(string message)
        {
            _logger.Value.Trace(message);
        }

        public void Error(string message)
        {
            _logger.Value.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            _logger.Value.Error(message, exception);
        }
        
        public void Error(Exception exception)
        {
            _logger.Value.Error(exception);
        }
    }
}
