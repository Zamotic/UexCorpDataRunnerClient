using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabAndScanPoC.Imaging.TextRetrieval;
public class CommodityDataTextExtractor : ITextExtractor
{
    public CommodityDataTextExtractor()
    {
        
    }

    public string ExtractTextFromImage(System.Drawing.Image image)
    {
        return "Text extracted from image.";
    }
}
