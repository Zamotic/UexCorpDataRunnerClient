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
public class MainViewModelData: MainViewModel
{
#if DEBUG
    /// <summary>
    /// Initializes a new instance of the MainViewModel class.
    /// </summary>
    public MainViewModelData() : base(new WeakReferenceMessenger(), new Services.UexDataService())
    {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject()) == false)
        {
            base.BindableCommodities = new List<BindableCommodity>()
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
#endif


}
