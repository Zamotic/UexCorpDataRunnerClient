using FluentAssertions;
using UexCorpDataRunner.Application;
using UexCorpDataRunner.Application.DataRunner;
using UexCorpDataRunner.Domain.DataRunner;
using UexCorpDataRunner.Domain.Services;
using Moq;

namespace Application.UnitTests.DataRunner;
public class CommodityWrapperToPriceReportConverterTests
{
    ICommodityWrapperToPriceReportConverter _converter;
    CommodityWrapper _commodityWrapper;
    public CommodityWrapperToPriceReportConverterTests()
    {
        const string accessCode = "c5e000";
        Mock<ISettingsService> mockSettingsService = new Mock<ISettingsService>();
        mockSettingsService.SetupGet(g => g.Settings).Returns(new UexCorpDataRunner.Domain.Settings.SettingsValues()
        { 
            UserAccessCode = accessCode,
            SelectedGameVersion = GameVersion.LiveValue,
            LoadedGameVersion = new GameVersion() { Live = "3.17.4", Ptu = "3.18" }
        });

        _converter = new CommodityWrapperToPriceReportConverter(mockSettingsService.Object);

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
    public void Convert_ShouldReturnPriceReportWithExpectedCommodityCode()
    {
        // Assemble
        const string TradeportCode = "AM056";
        const string ExpectedValue = "PRFO";

        // Act
        PriceReport actual = _converter.Convert(commodity: _commodityWrapper, tradeportCode: TradeportCode);

        // Assert
        actual.CommodityCode.Should().NotBeNull();
        actual.CommodityCode.Should().Be(ExpectedValue);
    }

    [Fact]
    public void Convert_ShouldReturnPriceReportWithExpectedTradeportCode()
    {
        // Assemble
        const string TradeportCode = "AM056";
        const string ExpectedValue = "AM056";

        // Act
        PriceReport actual = _converter.Convert(commodity: _commodityWrapper, tradeportCode: TradeportCode);

        // Assert
        actual.TradeportCode.Should().NotBeNull();
        actual.TradeportCode.Should().Be(ExpectedValue);
    }

    [Fact]
    public void Convert_ShouldReturnPriceReportWithExpectedOperation()
    {
        // Assemble
        const string TradeportCode = "AM056";
        const string ExpectedValue = "buy";

        // Act
        PriceReport actual = _converter.Convert(commodity: _commodityWrapper, tradeportCode: TradeportCode);

        // Assert
        actual.Operation.Should().NotBeNull();
        actual.Operation.Should().Be(ExpectedValue);
    }

    [Fact]
    public void Convert_ShouldReturnPriceReportWithExpectedPrice()
    {
        // Assemble
        const string TradeportCode = "AM056";
        const string ExpectedValue = "1.2";

        // Act
        PriceReport actual = _converter.Convert(commodity: _commodityWrapper, tradeportCode: TradeportCode);

        // Assert
        actual.Price.Should().NotBeNull();
        actual.Price.Should().Be(ExpectedValue);
    }

    [Fact]
    public void Convert_ShouldReturnPriceReportWithExpectedUserHash()
    {
        // Assemble
        const string TradeportCode = "AM056";
        const string ExpectedValue = "c5e000";

        // Act
        PriceReport actual = _converter.Convert(commodity: _commodityWrapper, tradeportCode: TradeportCode);

        // Assert
        actual.AccessCode.Should().NotBeNull();
        actual.AccessCode.Should().Be(ExpectedValue);
    }

    [Fact]
    public void Convert_ShouldReturnPriceReportWithExpectedVersion()
    {
        // Assemble
        const string TradeportCode = "AM056";
        const string ExpectedValue = "3.17.4";

        // Act
        PriceReport actual = _converter.Convert(commodity: _commodityWrapper, tradeportCode: TradeportCode);

        // Assert
        actual.Version.Should().NotBeNull();
        actual.Version.Should().Be(ExpectedValue);
    }
}
