using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.DesktopClient.Core;
public interface IMessenger
{
    /// <summary>
    /// Registers a recipient for a type of message T. The action parameter will be executed
    /// when a corresponding message is sent.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="recipient"></param>
    /// <param name="action"></param>
    void Register<T>(object recipient, Action<T> action);

    /// <summary>
    /// Registers a recipient for a type of message T and a matching context. The action parameter will be executed
    /// when a corresponding message is sent.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="recipient"></param>
    /// <param name="action"></param>
    /// <param name="context"></param>
    void Register<T>(object recipient, Action<T> action, object? context);

    /// <summary>
    /// Unregisters a messenger recipient completely. After this method is executed, the recipient will
    /// no longer receive any messages.
    /// </summary>
    /// <param name="recipient"></param>
    void Unregister<T>(object recipient);

    /// <summary>
    /// Unregisters a messenger recipient with a matching context completely. After this method is executed, the recipient will
    /// no longer receive any messages.
    /// </summary>
    /// <param name="recipient"></param>
    /// <param name="context"></param>
    void Unregister<T>(object recipient, object? context);

    /// <summary>
    /// Sends a message to registered recipients. The message will reach all recipients that are
    /// registered for this message type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    void Send<T>(T message);

    /// <summary>
    /// Sends a message to registered recipients. The message will reach all recipients that are
    /// registered for this message type and matching context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="context"></param>
    void Send<T>(T message, object context);
}
