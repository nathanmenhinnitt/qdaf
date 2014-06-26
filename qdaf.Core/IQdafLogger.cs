namespace qdaf.Core
{
    using System;
    
    public interface IQdafLogger
    {
        void Trace(string message);

        void Error(string message);
        
        void Error(Exception exception);

        void Error(string message, Exception exception);
    }
}
