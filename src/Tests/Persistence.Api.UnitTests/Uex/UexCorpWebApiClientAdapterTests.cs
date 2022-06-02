using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using AutoMapper;
using System.Net;
using UexCorpDataRunner.Persistence.Api.Uex;
using Moq;
using UexCorpDataRunner.Persistence.Api.Uex.Maps;

namespace Persistence.Api.UnitTests.Uex;
public class UexCorpWebApiClientAdapterTests
{
    public UexCorpWebApiClientAdapterTests()
    {
    }

    [Fact]
    public async Task GetSystemsAsyncReturnsExpectedCount()
    {
        // Assemble
        var webApiClient = new UexCorpDataRunner.Persistence.Api.Mock.Uex.UexCorpWebApiClientMock();
        var config = new MapperConfiguration(cfg => cfg.AddProfile<SystemProfile>());
        config.AssertConfigurationIsValid();
        var mapper = config.CreateMapper();


        // Act
        var adapter = new UexCorpWebApiClientAdapter(webApiClient, mapper);
        var systems = await adapter.GetSystemsAsync();

        // Assert
        systems.Count.Should().Be(2);
    }
}
