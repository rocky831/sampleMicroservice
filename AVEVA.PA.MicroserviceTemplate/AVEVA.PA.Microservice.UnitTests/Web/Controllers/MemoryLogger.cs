using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Microservice.UnitTests.Web.Controllers
{
    internal class MemoryLogger<T> : ILogger<T>
    {
        public List<Exception> Exceptions { get; } = new List<Exception>();

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (exception != null)
                Exceptions.Add(exception);
        }
    }
}
