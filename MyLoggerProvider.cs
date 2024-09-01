using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Shop
{
    public class MyLoggerProvider : ILoggerProvider
    {
        private readonly StreamWriter _writer;

        public MyLoggerProvider()
        {
            
            _writer = new StreamWriter("log.txt", true);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new MyLogger(_writer);
        }

        public void Dispose()
        {
            _writer?.Dispose();
        }

        private class MyLogger : ILogger
        {
            private readonly StreamWriter _writer;

            public MyLogger(StreamWriter writer)
            {
                _writer = writer;
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
            {
                var message = formatter(state, exception);
                Console.WriteLine(message); 
                _writer.WriteLine(message); 
                _writer.Flush(); 
            }
        }
    }
}

