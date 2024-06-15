using GrabAndScanPoC.Imaging.ImageRetrieval.ClipboardConverters;

namespace GrabAndScanPoC.Imaging.ImageRetrieval;
public static class ClipboardImageConverterFactory
{
    static Dictionary<string, IClipboardImageConverter> _ClipboardImageConverterList = new ();

    public static IClipboardImageConverter GetConverter(string format)
    {
        switch (format)
        {
            case "png":
                if(!_ClipboardImageConverterList.ContainsKey(format))
                {
                    _ClipboardImageConverterList.Add(format, new ClipboardConverters.PngClipboardImageConverter());
                }
                return _ClipboardImageConverterList[format];
            //case "jpg":
            //    return new ClipboardConverters.JpgClipboardImageConverter();
            //case "bmp":
            //    return new ClipboardConverters.BmpClipboardImageConverter();
            default:
                throw new NotSupportedException($"Clipboard format {format} is not supported.");
        }
    }
}
