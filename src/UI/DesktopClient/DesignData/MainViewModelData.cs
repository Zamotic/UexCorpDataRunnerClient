using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using UexCorpDataRunner.Application.Common;
using UexCorpDataRunner.Application.ViewModels;
using UexCorpDataRunner.Application.ViewModels.Bindables;
using UexCorpDataRunner.Domain.Models;
using CommunityToolkit.Mvvm.Messaging;

namespace UexCorpDataRunner.DesktopClient.DesignData;
#if DEBUG
public class MainViewModelData
{
    public IReadOnlyList<Domain.Models.System> SystemList { get; } = new List<Domain.Models.System>();
    public Domain.Models.System? SelectedSystem { get; }
    public IList<Planet> PlanetList { get; } =new List<Planet>();
    public Planet? SelectedPlanet { get; }
    public IList<Satellite> SatelliteList { get; } = new List<Satellite>();
    public Satellite? SelectedSatellite { get; }
    public IList<Tradeport> TradeportList { get; } = new List<Tradeport>();
    public Tradeport? SelectedTradeport { get; }

    public IList<BindableCommodity>? BindableCommodities { get; }

    /// <summary>
    /// Initializes a new instance of the MainViewModel class.
    /// </summary>
    public MainViewModelData()
    {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject()) == false)
        {
            BindableCommodities = new List<BindableCommodity>()
            {
                new BindableCommodity(new Commodity()
                {
                    Name = "Aluminum",
                    Code = "ALUM",
                    BuyPrice = 1.11m,
                    SellPrice = 0m,
                    Kind = "Metals",
                    DateModified = DateTime.Now,
                }),
                new BindableCommodity(new Commodity()
                {
                    Name = "Diamond",
                    Code = "DIAM",
                    BuyPrice = 5.85m,
                    SellPrice = 0m,
                    Kind = "Metals",
                    DateAdded = DateTime.MinValue,
                    DateModified = DateTime.Now,
                })
            };
        }
    }

}
#endif
