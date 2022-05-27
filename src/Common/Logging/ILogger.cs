using System;

namespace UexCorpDataRunner.Common.Logging;

public interface ILogger
{
    void Debug(Exception exception, string? messageTemplate);
    void Debug(string? messageTemplate);
    void Debug(string? messageTemplate, params object[] propertyValues);
    void Fatal(Exception exception, string? messageTemplate);
    void Fatal(string? messageTemplate);
    void Fatal(string? messageTemplate, params object[] propertyValues);
    void Information(Exception exception, string? messageTemplate);
    void Information(string? messageTemplate);
    void Information(string? messageTemplate, params object[] propertyValues);
    void Verbose(Exception exception, string? messageTemplate);
    void Verbose(string? messageTemplate);
    void Verbose(string? messageTemplate, params object[] propertyValues);
    void Warning(Exception exception, string? messageTemplate);
    void Warning(string? messageTemplate);
    void Warning(string? messageTemplate, params object[] propertyValues);
}