using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Application.DataRunner;

namespace UexCorpDataRunner.Interface.MessengerMessages;
public class ShowTransmissionStatusMessage
{
    IEnumerable<CommodityWrapper> BuyableCommodities { get; }
    IEnumerable<CommodityWrapper> SellableCommodities { get; }

    public ShowTransmissionStatusMessage(IEnumerable<CommodityWrapper> buyableCommodities, IEnumerable<CommodityWrapper> sellableCommodities)
    {
        BuyableCommodities = buyableCommodities;
        SellableCommodities = sellableCommodities;
    }
}
