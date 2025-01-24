using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Persistence.Api.Common;
using UexCorpDataRunner.Persistence.Api.UexV2;
using NSubstitute.Extensions;

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
    public async void GetDataParameters_Should_ReturnExpectedValues()
    {
        // Arrange
        DataParametersDto expected = new()
        {
            Global = new()
            {
                IsAcceptingReports = true,
                GameVersion = "4.0",
                GameVersionPtu = "4.0.1",
                EvaluationPeriodDays = 90
            }
        };

        // Act
        DataParametersDto actual = await _sut.GetDataParametersAsync();

        // Assert
        actual.ShouldBeEquivalentTo(expected);
    }
}

