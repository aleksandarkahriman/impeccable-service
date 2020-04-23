using System;

namespace Logger.Abstraction
{
    public interface ILogger<TSource>
    {
        void Info(string message);

        void Event(string message);

        void Debug(string message);

        void Warning(Exception exception, string message);

        void Warning(string message);

        void Error(Exception exception, string message);

        void Error(string message);
    }
}
