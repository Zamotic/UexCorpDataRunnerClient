using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UexCorpDataRunner.Domain.DataRunner;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex.Maps.Resolvers;
public class TradeListingResolver : IValueResolver<KeyValuePair<string, TradeListingDto>, Domain.DataRunner.TradeListing, object>
{
	public object Resolve(KeyValuePair<string,TradeListingDto> source, Domain.DataRunner.TradeListing destination, object destMember, ResolutionContext context)
	{
		Domain.DataRunner.OperationType operationType = Domain.DataRunner.OperationType.Buy;
		if (source.Value.Operation?.Equals("sell") == true)
		{
			operationType = Domain.DataRunner.OperationType.Sell;
		}

		Domain.DataRunner.TradeListing responseTradeListing = new Domain.DataRunner.TradeListing()
		{
			Name = source.Value.Name,
			Code = source.Key,
			Kind = source.Value.Kind,
			Operation = operationType,
			PriceBuy = source.Value.PriceBuy,
			PriceSell = source.Value.PriceSell,
			DateUpdate = source.Value.DateUpdate,
			IsUpdated = source.Value.IsUpdated
		};

		return responseTradeListing;
	}
}