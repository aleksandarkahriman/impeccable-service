using System;
using Logger.Abstraction;
using Xunit.Abstractions;

namespace Utility.Test
{
    internal class TestLogger : ILogger
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TestLogger(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public void Info<TSource>(string message)
        {
            _testOutputHelper.WriteLine($"{DateTime.Now} Info -> {typeof(TSource).Name} -> {message}");
        }

        public void Event<TSource>(string message)
        {
            _testOutputHelper.WriteLine($"{DateTime.Now} Info -> {typeof(TSource).Name} -> {message}");
        }

        public void Debug<TSource>(string message)
        {
            _testOutputHelper.WriteLine($"{DateTime.Now} Debug -> {typeof(TSource).Name} -> {message}");
        }

        public void Warning<TSource>(Exception exception, string message)
        {
            _testOutputHelper.WriteLine($"{DateTime.Now} Warning -> {typeof(TSource).Name} -> {message}");
            _testOutputHelper.WriteLine($"{DateTime.Now} Warning -> {typeof(TSource).Name} -> {exception}");
        }

        public void Warning<TSource>(string message)
        {
            _testOutputHelper.WriteLine($"{DateTime.Now} Warning -> {typeof(TSource).Name} -> {message}");
        }

        public void Error<TSource>(Exception exception, string message)
        {
            _testOutputHelper.WriteLine($"{DateTime.Now} Error -> {typeof(TSource).Name} -> {message}");
            _testOutputHelper.WriteLine($"{DateTime.Now} Error -> {typeof(TSource).Name} -> {exception}");
        }

        public void Error<TSource>(string message)
        {
            _testOutputHelper.WriteLine($"{DateTime.Now} Error -> {typeof(TSource).Name} -> {message}");
        }
    }
}
