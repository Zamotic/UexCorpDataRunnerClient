using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Presentation.DataRunner;
#if DEBUG
public class DataRunnerViewModelData
{
    public IReadOnlyList<Domain.DataRunner.System> SystemList { get; } = new List<Domain.DataRunner.System>();
    public Domain.DataRunner.System? SelectedSystem { get; set; }
    public IList<Planet> PlanetList { get; } =new List<Planet>();
    public Planet? SelectedPlanet { get; set; }
    public IList<Satellite> SatelliteList { get; } = new List<Satellite>();
    public Satellite? SelectedSatellite { get; set; }
    public IList<Tradeport> TradeportList { get; } = new List<Tradeport>();
    public Tradeport? SelectedTradeport { get; set; }

    public IList<Commodity>? BindableCommodities { get; }

    /// <summary>
    /// Initializes a new instance of the MainViewModel class.
    /// </summary>
    public DataRunnerViewModelData()
    {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject()) == false)
        {
            BindableCommodities = new List<Commodity>()
            {
                new Commodity()
                {
                    Name = "Aluminum",
                    Code = "ALUM",
                    BuyPrice = 1.11m,
                    SellPrice = 0m,
                    Kind = "Metals",
                    DateModified = DateTime.Now,
                },
                new Commodity()
                {
                    Name = "Diamond",
                    Code = "DIAM",
                    BuyPrice = 5.85m,
                    SellPrice = 0m,
                    Kind = "Metals",
                    DateAdded = DateTime.MinValue,
                    DateModified = DateTime.Now,
                }
            };
        }
    }

}
#endif
