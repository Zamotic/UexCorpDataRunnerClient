using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Interface.MessengerMessages;
public class ThemeChangedMessage
{
    public Theme.Skin SelectedSkin { get; }

    public ThemeChangedMessage(string selectedSkin)
    {
        SelectedSkin = ConvertStringToSkin(selectedSkin);
    }

    private Theme.Skin ConvertStringToSkin(string selectedSkinString)
    {
        switch(selectedSkinString)
        {
            case Domain.Globals.Settings.Light:
                return Theme.Skin.Light;
            case Domain.Globals.Settings.Dark:
            default:
                return Theme.Skin.Dark;
        }
             
    }
}
