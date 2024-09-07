using static System.Windows.Forms.DataFormats;
using System.IO;
using System.Windows.Media.Imaging;

namespace GrabAndScanPoC.Imaging.ImageRetrieval.ClipboardConverters;
public class BitmapClipboardImageConverter : IClipboardImageConverter
{
    private const string format = "Bitmap";
    public Image? GetClipboardImage(System.Windows.IDataObject dataObject)
    {
        try
        {
            Object contents = dataObject.GetData(format);
            BitmapSource? bitmapSource = contents as BitmapSource;

            if(bitmapSource is null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(ms);
                //bitmap.Save("c:\\users\\batom\\desktop\\clipboard.png");
                return bitmap;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return null;
    }
}
