using GrabAndScanPoC.Imaging.TextRetrieval;
using System.Drawing;
using FluentAssertions;

namespace Imaging.UnitTests;

public class CommodityDataTextExtractorTests
{
    public Image? GetImageFromManifest(string imageName)
    {
        var assembly = typeof(CommodityDataTextExtractorTests).Assembly;
        var resourceName = $"GrabAndScanPoC.Imaging.UnitTests.ImageSamples.{imageName}";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if(stream is null)
        {
            return null;
        }
        return Image.FromStream(stream);
    }


    [Fact]
    public void ExtractTextFromImage_Should_ReturnExpectedText()
    {
        // Assemble
        var image = GetImageFromManifest("Sample.png");
        var sut = new CommodityDataTextExtractor();
        string expectedExtractedText = "Text extracted from image.";

        // Act
        image.Should().NotBeNull();
        var result = sut.ExtractTextFromImage(image!);

        // Assert
        result.Should().Be(expectedExtractedText);
    }
}