namespace GrabAndScanPoC.Imaging.TextRetrieval;

public interface ITextExtractor
{
    public string? ExtractTextFromImage(System.Drawing.Image image);
}