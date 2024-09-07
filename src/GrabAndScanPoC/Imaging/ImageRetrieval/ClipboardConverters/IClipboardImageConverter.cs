using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabAndScanPoC.Imaging.ImageRetrieval.ClipboardConverters;
public interface IClipboardImageConverter
{
    public Image? GetClipboardImage(System.Windows.IDataObject dataObject);
}
