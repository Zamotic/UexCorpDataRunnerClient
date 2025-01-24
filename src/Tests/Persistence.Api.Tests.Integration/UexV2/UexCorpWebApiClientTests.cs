using UexCorpDataRunner.Domain.DataRunner;
using UexCorpDataRunner.Domain.Services;
using UexCorpDataRunner.Persistence.Api.Common;
using UexCorpDataRunner.Persistence.Api.UexV2;

namespace Persistence.Api.Tests.Integration.UexV2;

public class UexCorpWebApiClientTests
{
    readonly UexCorpWebApiClient _sut;

    public UexCorpWebApiClientTests()
    {
        IUexCorpWebApiConfiguration uexCorpWebApiConfiguration = NSubstitute.Substitute.For<IUexCorpWebApiConfiguration>();
        ISettingsService settingsService = NSubstitute.Substitute.For<ISettingsService>();
        HttpClient httpClient = new HttpClient();

        _sut = new(uexCorpWebApiConfiguration, httpClient, settingsService);
    }

    [Fact]
    public async void GetDataParameters_Should_ReturnExpectedValues()
    {
        // Arrange
        DataParameters expected = new()
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
        DataParameters actual = await _sut.GetDataParametersAsync();

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}

