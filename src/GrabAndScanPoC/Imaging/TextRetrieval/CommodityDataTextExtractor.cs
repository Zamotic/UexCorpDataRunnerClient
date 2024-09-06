using System.Diagnostics;
using System.Drawing.Imaging;
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
            Bitmap bitmap = MakeGrayscale(new Bitmap(image));
            bitmap.Save(@"c:\users\batom\desktop\testGray.png", System.Drawing.Imaging.ImageFormat.Png);
            bitmap = MakeBlackAndWhite(new Bitmap(image));
            bitmap.Save(@"c:\users\batom\desktop\testBW.png", System.Drawing.Imaging.ImageFormat.Png);

            using (var engine = new TesseractEngine(TesseractDataPath, TesseractLanguage, EngineMode.Default))
            using (var img = PixConverter.ToPix(bitmap))
            using (var page = engine.Process(img))
            {
                var text = page.GetText();
                var meanConfidence = page.GetMeanConfidence();
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

    private Bitmap MakeGrayscale(Bitmap original)
    {
        //create a blank bitmap the same size as original
        Bitmap newBitmap = new Bitmap(original.Width, original.Height);

        //get a graphics object from the new image
        using (Graphics g = Graphics.FromImage(newBitmap))
        {

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][]
                {
            new float[] {.3f, .3f, .3f, 0, 0},
            new float[] {.59f, .59f, .59f, 0, 0},
            new float[] {.11f, .11f, .11f, 0, 0},
            new float[] {0, 0, 0, 1, 0},
            new float[] {0, 0, 0, 0, 1}
                });

            //create some image attributes
            using (ImageAttributes attributes = new ImageAttributes())
            {
                //set the color matrix attribute
                attributes.SetColorMatrix(colorMatrix);

                //draw the original image on the new image
                //using the grayscale color matrix
                g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                            0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            }
        }
        return newBitmap;
    }

    private Bitmap MakeBlackAndWhite(Bitmap original)
    {

        //create a blank bitmap the same size as original
        Bitmap newBitmap = new Bitmap(original.Width, original.Height);

        //get a graphics object from the new image
        using (Graphics g = Graphics.FromImage(newBitmap))
        {
            var gray_matrix = new float[][] {
                new float[] { 0.299f, 0.299f, 0.299f, 0, 0 },
                new float[] { 0.587f, 0.587f, 0.587f, 0, 0 },
                new float[] { 0.114f, 0.114f, 0.114f, 0, 0 },
                new float[] { 0,      0,      0,      1, 0 },
                new float[] { 0,      0,      0,      0, 1 }
            };

            using (var ia = new ImageAttributes())
            {
                ia.SetColorMatrix(new ColorMatrix(gray_matrix));
                ia.SetThreshold(0.8f); // Change this threshold as needed
                var rc = new Rectangle(0, 0, original.Width, original.Height);
                g.DrawImage(original, rc, 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, ia);
            }
        }

        return newBitmap;
    }

}
