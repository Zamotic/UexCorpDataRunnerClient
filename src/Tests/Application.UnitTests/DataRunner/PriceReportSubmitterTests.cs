using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.Services;
using Xunit;
using Moq;
using UexCorpDataRunner.Application;
using UexCorpDataRunner.Application.DataRunner;
using UexCorpDataRunner.Domain.DataRunner;

namespace Application.UnitTests.DataRunner;
public class PriceReportSubmitterTests
{
    Mock<IUexDataService> _mockDataService;
    Mock<ICommodityWrapperToPriceReportConverter> _mockConverter;
    System.Collections.Concurrent.ConcurrentQueue<string> _statusBufferQueue;
    CommodityWrapper _commodityWrapper;

    public PriceReportSubmitterTests()
    {

        _mockDataService = new Mock<IUexDataService>();
        _mockDataService.Setup(s => s.SubmitPriceReportAsync(It.IsAny<PriceReport>())).ReturnsAsync(new PriceReportResponse()
        {
            Response = true,
            StatusMessage = "ok"
        });

        _mockConverter = new Mock<ICommodityWrapperToPriceReportConverter>();
        _mockConverter.Setup(s => s.Convert(It.IsAny<CommodityWrapper>(), It.Is<string>(x => x.Equals("AM056")))).Returns(new PriceReport()
        {
            CommodityCode = "PRFO",
            TradeportCode = "AM056",
            Price = "1.2",
            Operation = "buy",
            AccessCode = "c5e000"
        });
        _statusBufferQueue = new System.Collections.Concurrent.ConcurrentQueue<string>();

        Commodity commodity = new Commodity()
        {
            Code = "PRFO",
            Name = "Processed Foods",
            Kind = "Food",
            BuyPrice = 1.21m,
            SellPrice = 0m,
            DateAdded = DateTimeOffset.Now,
            DateModified = DateTimeOffset.Now
        };
        TradeListing tradeListing = new TradeListing()
        {
            Code = "PRFO",
            Name = "Processed Foods",
            Kind = "Food",
            Operation = OperationType.Buy,
            PriceBuy = 1.0m,
            PriceSell = 0m,
            IsUpdated = true,
            DateUpdate = DateTimeOffset.Now
        };
        _commodityWrapper = new CommodityWrapper(commodity, tradeListing);
        _commodityWrapper.CurrentPrice = 1.2m;
    }

    [Fact]
    public async Task SubmitPriceReports_ShouldReturnExpectedResponse()
    {
        // Assemble
        const string TradeportCode = "AM056";
        PriceReportSubmitter priceReportSubmitter = new PriceReportSubmitter(converter: _mockConverter.Object, uexDataService: _mockDataService.Object);
        IEnumerable<CommodityWrapper> commodities = new List<CommodityWrapper>()
        {
            _commodityWrapper
        };

        // Act
        var actual = await priceReportSubmitter.SubmitReports(commodities, TradeportCode, statusBufferQueue: _statusBufferQueue).ConfigureAwait(false);

        // Assert
        actual.Should().ContainKey("PRFO");
        actual["PRFO"].Should().BeTrue();
    }
}
 