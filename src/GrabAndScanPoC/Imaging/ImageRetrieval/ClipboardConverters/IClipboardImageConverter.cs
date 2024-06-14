using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabAndScanPoc.Imaging.ImageRetrieval.ClipboardConverters;
public interface IClipboardImageConverter
{
    public Image? GetClipboardImage();
}
