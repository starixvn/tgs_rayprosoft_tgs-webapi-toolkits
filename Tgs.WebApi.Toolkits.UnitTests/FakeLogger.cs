using System.Reactive.Disposables;
using Microsoft.Extensions.Logging;

namespace Tgs.WebApi.Toolkits.UnitTests;

internal class FakeLogger : ILogger
{
    public string? StateText { get; set; }

    public string? StateType { get; set; }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => Disposable.Empty;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        StateType = typeof(TState).Name;
        StateText = state?.ToString();
    }
}
