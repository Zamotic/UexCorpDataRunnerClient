using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Application.DataRunner;

namespace UexCorpDataRunner.Interface.MessengerMessages;
public class TransmissionStatusCompleteMessage
{
    public string ResponseMessage { get; }
    public bool AllResponsesSucceeded { get; }

    public TransmissionStatusCompleteMessage(string responseMessage, bool allResponsesSucceeded = true)
    {
        ResponseMessage = responseMessage;
        AllResponsesSucceeded = allResponsesSucceeded;
    }
}
