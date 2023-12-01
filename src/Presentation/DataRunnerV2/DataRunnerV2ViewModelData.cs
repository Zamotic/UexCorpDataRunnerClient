﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using UexCorpDataRunner.Application.DataRunnerV2;
using UexCorpDataRunner.Domain.DataRunnerV2;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace UexCorpDataRunner.Presentation.DataRunnerV2;
#if DEBUG
public class DataRunnerV2ViewModelData : ObservableObject
{
    public IReadOnlyList<StarSystem> SystemList { get; } = new List<StarSystem>();
    public StarSystem? SelectedSystem { get; set; }

    public IReadOnlyList<Terminal> TerminalList { get; } = new List<Terminal>();
    public Terminal? SelectedTerminal { get; set; }

    public List<CommodityWrapper> BuyableCommodities = new List<CommodityWrapper>()
    {
        new CommodityWrapper(
            new Commodity()
            {
                Name = "Aluminum",
                Code = "ALUM",
                BuyPrice = 1.11m,
                SellPrice = 0m,
                Kind = "Metals",
                DateAdded = DateTime.Today,
                DateModified = DateTime.Today,
            },
            new CommodityPrice()
            {
                CommodityId = 1,
                CommodityName = "Aluminum",
                BuyPrice = 1.11m,
                SellPrice = 0m
            }
        )
    };
    public List<CommodityWrapper> SellableCommodities = new List<CommodityWrapper>()
    {
        new CommodityWrapper(
            new Commodity()
            {
                Name = "Stims",
                Code = "STIM",
                BuyPrice = 0m,
                SellPrice = 2.8m,
                Kind = "Metals",
                DateAdded = DateTime.Today,
                DateModified = DateTime.Today,
            },
            new CommodityPrice()
            {
                CommodityId = 2,
                CommodityName = "Stims",
                BuyPrice = 0m,
                SellPrice = 1.11m
            }
        )
    };

    /// <summary>
    /// Initializes a new instance of the MainViewModel class.
    /// </summary>
    public DataRunnerV2ViewModelData()
    {
        //if (DesignerProperties.GetIsInDesignMode(new DependencyObject()) == false)
        //{
            //SelectedTradeport = new Tradeport()
            //{
            //    System = "ST",
            //    Planet = "ARC",
            //    Satellite = "WALA",
            //    City = null,
            //    Name = "ArcCorp Mining Area 056",
            //    NameShort = "ArcCorp 056",
            //    Code = "AM056",
            //    IsVisible = true,
            //    IsArmisticeZone = true,
            //    HasTrade = true,
            //    WelcomesOutlaws = false,
            //    HasRefinery = false,
            //    HasShops = false,
            //    IsRestrictedArea = false,
            //    HasMinables = false,
            //    DateAdded = DateTimeOffset.Parse("12/25/2020, 11:25:15 PM +03:00"),
            //    DateModified = DateTimeOffset.Parse("12/25/2020, 11:25:15 PM +03:00"),
            //    Prices = new Dictionary<string, TradeListing>()
            //    {
            //        { "PRFO", new TradeListing() { Name = "Processed Food", Kind = "Food", Operation = OperationType.Sell, PriceBuy = 0m, PriceSell = 1.5m, DateUpdate = DateTimeOffset.Parse("6/4/2022, 8:00:06 PM +03:00"), IsUpdated = true } },
            //        { "STIM", new TradeListing() { Name = "Stims", Kind = "Vice", Operation = OperationType.Sell, PriceBuy = 0m, PriceSell = 3.8m, DateUpdate = DateTimeOffset.Parse("6/4/2022, 8:00:06 PM +03:00"), IsUpdated = true } },
            //        { "DISP", new TradeListing() { Name = "Distilled Spirits", Kind = "Vice", Operation = OperationType.Sell, PriceBuy = 0m, PriceSell = 5.56m, DateUpdate = DateTimeOffset.Parse("6/4/2022, 8:00:06 PM +03:00"), IsUpdated = true } },
            //        { "MEDS", new TradeListing() { Name = "Medical Supplies", Kind = "Medical", Operation = OperationType.Sell, PriceBuy = 0m, PriceSell = 19.25m, DateUpdate = DateTimeOffset.Parse("6/4/2022, 8:00:06 PM +03:00"), IsUpdated = true } },
            //        { "DOLI", new TradeListing() { Name = "Dolivine", Kind = "Mineral", Operation = OperationType.Sell, PriceBuy = 0m, PriceSell = 130m, DateUpdate = DateTimeOffset.Parse("6/4/2022, 8:00:06 PM +03:00"), IsUpdated = false } },
            //        { "APHO", new TradeListing() { Name = "Aphorite", Kind = "Mineral", Operation = OperationType.Sell, PriceBuy = 0m, PriceSell = 152.5m, DateUpdate = DateTimeOffset.Parse("6/4/2022, 8:00:06 PM +03:00"), IsUpdated = true } },
            //        { "HADA", new TradeListing() { Name = "Hadanite", Kind = "Mineral", Operation = OperationType.Sell, PriceBuy = 0m, PriceSell = 275m, DateUpdate = DateTimeOffset.Parse("6/4/2022, 8:00:06 PM +03:00"), IsUpdated = true } },
            //        { "IODI", new TradeListing() { Name = "Iodine", Kind = "Halogen", Operation = OperationType.Buy, PriceBuy = 0.35m, PriceSell = 0m, DateUpdate = DateTimeOffset.Parse("6/4/2022, 8:00:06 PM +03:00"), IsUpdated = true } },
            //        { "ALUM", new TradeListing() { Name = "Aluminum", Kind = "Metal", Operation = OperationType.Buy, PriceBuy = 1.11m, PriceSell = 0m, DateUpdate = DateTimeOffset.Parse("6/4/2022, 8:00:06 PM +03:00"), IsUpdated = true } },
            //        { "TUNG", new TradeListing() { Name = "Tungsten", Kind = "Metal", Operation = OperationType.Buy, PriceBuy = 3.55m, PriceSell = 0m, DateUpdate = DateTimeOffset.Parse("6/4/2022, 8:00:06 PM +03:00"), IsUpdated = true } },
            //        { "DIAM", new TradeListing() { Name = "Diamond", Kind = "Metal", Operation = OperationType.Buy, PriceBuy = 6.28m, PriceSell = 0m, DateUpdate = DateTimeOffset.Parse("6/4/2022, 8:00:06 PM +03:00"), IsUpdated = true } },
            //        { "ASTA", new TradeListing() { Name = "Astatine", Kind = "Halogen", Operation = OperationType.Buy, PriceBuy = 7.52m, PriceSell = 0m, DateUpdate = DateTimeOffset.Parse("6/4/2022, 8:00:06 PM +03:00"), IsUpdated = true } },
            //        { "LARA", new TradeListing() { Name = "Laranite", Kind = "Metal", Operation = OperationType.Buy, PriceBuy = 27.74m, PriceSell = 0m, DateUpdate = DateTimeOffset.Parse("6/4/2022, 8:00:06 PM +03:00"), IsUpdated = true } }
            //    }
            //};
        //}
    }
}
#endif
