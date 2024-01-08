﻿using System.ComponentModel;
using System.Windows.Data;
using Microsoft.Xaml.Behaviors;
using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Presentation.DataRunnerV2.Sort;

public class MoonSort : Behavior<CollectionViewSource>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        AssociatedObject.SortDescriptions.Clear();
        AssociatedObject.SortDescriptions.Add(new SortDescription(nameof(Moon.Name), ListSortDirection.Ascending));
    }
}