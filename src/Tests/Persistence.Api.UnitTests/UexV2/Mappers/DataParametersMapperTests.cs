using NSubstitute.Core;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.DataRunnerV2;
using UexCorpDataRunner.Persistence.Api;
using UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
using UexCorpDataRunner.Persistence.Api.UexV2.Mappers;


namespace Persistence.Api.UnitTests.UexV2.Mappers;
public class DataParametersMapperTests
{
    [Fact]
    public void ConvertFromDto_WhenCalled_ShouldReturnDataParameters()
    {
        // Arrange
        DataParametersDto dataParametersDto = new DataParametersDto
        {
            Global = new()
            {
                IsAcceptingReports = true,
                GameVersion = "1.0",
                GameVersionPtu = "1.1",
                EvaluationPeriodDays = 1
            }
        };
        // Act
        DataParameters result = DataParametersMapper.ConvertFromDto(dataParametersDto);
        // Assert
        result.ShouldNotBeNull();
        result.Global.ShouldNotBeNull();
        result.Global.IsAcceptingReports.ShouldBe(dataParametersDto.Global.IsAcceptingReports);
        result.Global.GameVersion.ShouldBe(dataParametersDto.Global.GameVersion);
        result.Global.GameVersionPtu.ShouldBe(dataParametersDto.Global.GameVersionPtu);
        result.Global.EvaluationPeriodDays.ShouldBe(dataParametersDto.Global.EvaluationPeriodDays);
    }

    [Fact]
    public void ConvertToDto_WhenCalled_ShouldReturnDataParameters()
    {
        // Arrange
        DataParameters dataParameters = new DataParameters
        {
            Global = new()
            {
                IsAcceptingReports = true,
                GameVersion = "1.0",
                GameVersionPtu = "1.1",
                EvaluationPeriodDays = 1
            }
        };
        // Act
        DataParametersDto result = DataParametersMapper.ConvertToDto(dataParameters);
        // Assert
        result.ShouldNotBeNull();
        result.Global.ShouldNotBeNull();
        result.Global.IsAcceptingReports.ShouldBe(dataParameters.Global.IsAcceptingReports);
        result.Global.GameVersion.ShouldBe(dataParameters.Global.GameVersion);
        result.Global.GameVersionPtu.ShouldBe(dataParameters.Global.GameVersionPtu);
        result.Global.EvaluationPeriodDays.ShouldBe(dataParameters.Global.EvaluationPeriodDays);
    }
}
