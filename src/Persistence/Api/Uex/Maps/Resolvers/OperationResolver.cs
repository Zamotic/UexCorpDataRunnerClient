using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;

namespace UexCorpDataRunner.Persistence.Api.Uex.Maps.Resolvers;
public class OperationResolver : IValueResolver<TradeListingDto, Domain.DataRunner.TradeListing, Domain.DataRunner.OperationType>
{
	public Domain.DataRunner.OperationType Resolve(TradeListingDto source, Domain.DataRunner.TradeListing destination, Domain.DataRunner.OperationType member, ResolutionContext context)
	{
		if(source is null)
        {
			throw new ArgumentNullException(nameof(source));
        }

		if (source.Operation?.Equals("buy") == true)
			return Domain.DataRunner.OperationType.Buy;

		return Domain.DataRunner.OperationType.Sell;
	}
}