using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Persistence.Api.UexV2;

namespace Persistence.Api.Tests.Integration.UexV2;

public class UexCorpWebApiClientTests
{
    readonly UexCorpWebApiClient _sut;

    public UexCorpWebApiClientTests()
    {
        IUexCorpWebApiConfiguration uexCorpWebApiConfiguration = Substitute.For<IUexCorpWebApiConfiguration>();
        uexCorpWebApiConfiguration.WebApiEndPointUrl.Returns("https://api.uexcorp.space/");
        uexCorpWebApiConfiguration.DataRunnerEndpointPath.Returns("2.0/");
        uexCorpWebApiConfiguration.ApiKey.Returns("tFzGU35mHdBZVBVO9TMR/muwuHz8P7TimgK66fSj1wrBoCUsEL7ea9TVuJGakVvQ");

        ISettingsService settingsService = NSubstitute.Substitute.For<ISettingsService>();


        HttpClient httpClient = new HttpClient();

        _sut = new(uexCorpWebApiConfiguration, httpClient, settingsService);
    }

    [Fact]
    public async Task GetDataParameters_Should_ReturnExpectedValues()
    {
        // Arrange
        DataParametersDto expected = new()
        {
            Global = new()
            {
                IsAcceptingReports = true,
                GameVersion = "4.1",
                GameVersionPtu = "4.1.1",
                EvaluationPeriodDays = 90
            }
        };

        // Act
        DataParametersDto actual = await _sut.GetDataParametersAsync();

        // Assert
        actual.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetCommodityPrices_Should_ReturnExpectedValues()
    {
        // Arrange
        int terminalId = 1;
        CommodityPriceDto expected = new()
        {
            Id = 375,
            TerminalId = 1,
            StarSystemId = 68,
            StarSystemName = "Stanton",
            SpaceStationName = "ARC-L1 Wide Forest Station",
            PlanetId = 4,
            PlanetName = "ArcCorp",
            CommodityId = 70, 
            CommodityName = "Stims",
            ContainerSizes = "1,2,4,8,16,32",
            DateAdded = DateTimeOffset.Parse("12/26/2023 7:37:24 PM +00:00"),
            FactionName = "United Empire of Earth",
            GameVersion = "4.1",
        };

        // Act
        ICollection<CommodityPriceDto> actual = await _sut.GetCommodityPricesAsync(terminalId);

        // Assert
        actual.FirstOrDefault().ShouldBeEquivalentTo(expected);
    }
}

