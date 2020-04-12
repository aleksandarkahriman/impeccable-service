using System;

namespace Logger.Abstraction
{
    public interface ILogger
    {
        void Info<TSource>(string message);

        void Event<TSource>(string message);

        void Debug<TSource>(string message);

        void Warning<TSource>(Exception exception, string message);

        void Warning<TSource>(string message);

        void Error<TSource>(Exception exception, string message);

        void Error<TSource>(string message);
    }
}
