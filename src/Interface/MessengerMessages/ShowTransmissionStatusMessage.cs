﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Application.DataRunner;

namespace UexCorpDataRunner.Interface.MessengerMessages;
public class ShowTransmissionStatusMessage
{
    public System.Collections.Concurrent.ConcurrentQueue<string> Queue { get; }

    public ShowTransmissionStatusMessage(System.Collections.Concurrent.ConcurrentQueue<string> queue)
    {
        Queue = queue;
    }
}
