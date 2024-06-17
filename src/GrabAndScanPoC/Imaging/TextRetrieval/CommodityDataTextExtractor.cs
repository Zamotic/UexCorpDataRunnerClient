using System.Diagnostics;
using Tesseract;

namespace GrabAndScanPoC.Imaging.TextRetrieval;
public class CommodityDataTextExtractor : ITextExtractor
{
    const string TesseractDataPath = @"./tessdata";
    const string TesseractLanguage = "eng";

    public CommodityDataTextExtractor()
    {
        
    }

    public string? ExtractTextFromImage(System.Drawing.Image image)
    {
        try
        {
            using (var engine = new TesseractEngine(TesseractDataPath, TesseractLanguage, EngineMode.Default))
            using (var img = PixConverter.ToPix(new Bitmap(image!)))
            using (var page = engine.Process(img))
            {
                var text = page.GetText();
                Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                Console.WriteLine("Text (GetText): \r\n{0}", text);
                Console.WriteLine("Text (iterator):");
                return text;
            }
        }
        catch (Exception e)
        {
            Trace.TraceError(e.ToString());
            Console.WriteLine("Unexpected Error: " + e.Message);
            Console.WriteLine("Details: ");
            Console.WriteLine(e.ToString());
        }

        return null;
    }
}
