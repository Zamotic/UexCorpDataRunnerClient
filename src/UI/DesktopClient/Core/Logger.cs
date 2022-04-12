using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace UexCorpDataRunner.DesktopClient.Core;
public class Logger : ILogger
{
    private Serilog.ILogger _Logger;

    public Logger(Serilog.ILogger logger)
    {
        _Logger = logger;
    }

    public void Debug(string? messageTemplate)
    {
        _Logger.Debug(messageTemplate);
    }
    public void Debug(Exception exception, string? messageTemplate)
    {
        _Logger.Debug(exception, messageTemplate);
    }
    public void Debug(string? messageTemplate, params object[] propertyValues)
    {
        _Logger.Debug(messageTemplate, propertyValues);
    }

    public void Information(string? messageTemplate)
    {
        _Logger.Information(messageTemplate);
    }
    public void Information(Exception exception, string? messageTemplate)
    {
        _Logger.Information(exception, messageTemplate);
    }
    public void Information(string? messageTemplate, params object[] propertyValues)
    {
        _Logger.Information(messageTemplate, propertyValues);
    }

    public void Verbose(string? messageTemplate)
    {
        _Logger.Verbose(messageTemplate);
    }
    public void Verbose(Exception exception, string? messageTemplate)
    {
        _Logger.Verbose(exception, messageTemplate);
    }
    public void Verbose(string? messageTemplate, params object[] propertyValues)
    {
        _Logger.Verbose(messageTemplate, propertyValues);
    }

    public void Warning(string? messageTemplate)
    {
        _Logger.Warning(messageTemplate);
    }
    public void Warning(Exception exception, string? messageTemplate)
    {
        _Logger.Warning(exception, messageTemplate);
    }
    public void Warning(string? messageTemplate, params object[] propertyValues)
    {
        _Logger.Warning(messageTemplate, propertyValues);
    }

    public void Fatal(string? messageTemplate)
    {
        _Logger.Fatal(messageTemplate);
    }
    public void Fatal(Exception exception, string? messageTemplate)
    {
        _Logger.Fatal(exception, messageTemplate);
    }
    public void Fatal(string? messageTemplate, params object[] propertyValues)
    {
        _Logger.Fatal(messageTemplate, propertyValues);
    }
}
