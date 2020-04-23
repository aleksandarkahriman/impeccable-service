using System;
using Logger.Abstraction;
using Xunit.Abstractions;

namespace Utility.Test
{
    internal class TestLogger<TSource> : ILogger<TSource>
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TestLogger(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public void Info(string message)
        {
            _testOutputHelper.WriteLine($"{DateTime.Now} Info -> {typeof(TSource).Name} -> {message}");
        }

        public void Event(string message)
        {
            _testOutputHelper.WriteLine($"{DateTime.Now} Info -> {typeof(TSource).Name} -> {message}");
        }

        public void Debug(string message)
        {
            _testOutputHelper.WriteLine($"{DateTime.Now} Debug -> {typeof(TSource).Name} -> {message}");
        }

        public void Warning(Exception exception, string message)
        {
            _testOutputHelper.WriteLine($"{DateTime.Now} Warning -> {typeof(TSource).Name} -> {message}");
            _testOutputHelper.WriteLine($"{DateTime.Now} Warning -> {typeof(TSource).Name} -> {exception}");
        }

        public void Warning(string message)
        {
            _testOutputHelper.WriteLine($"{DateTime.Now} Warning -> {typeof(TSource).Name} -> {message}");
        }

        public void Error(Exception exception, string message)
        {
            _testOutputHelper.WriteLine($"{DateTime.Now} Error -> {typeof(TSource).Name} -> {message}");
            _testOutputHelper.WriteLine($"{DateTime.Now} Error -> {typeof(TSource).Name} -> {exception}");
        }

        public void Error(string message)
        {
            _testOutputHelper.WriteLine($"{DateTime.Now} Error -> {typeof(TSource).Name} -> {message}");
        }
    }
}
