using UexCorpDataRunner.Common;

namespace Common.UnitTests;
public class SimpleCipherTests
{
    [Theory]
    [InlineData("TestString","TestCipherKey")]
    [InlineData("ThisIsASuperLongTestString", "TestCipherKey")]
    [InlineData("ThisIsAnEvenLongerSuperLongTestString", "TestCipherKey")]
    [InlineData("TestString", "ThisIsASuperLongCipherKeyString")]
    [InlineData("ThisIsASuperLongTestString", "ThisIsASuperLongCipherKeyString")]
    [InlineData("ThisIsAnEvenLongerSuperLongTestString", "ThisIsASuperLongCipherKeyString")]
    [InlineData("TestString", "ThisIsAnEvenLongerSuperLongCipherKeyString")]
    [InlineData("ThisIsASuperLongTestString", "ThisIsAnEvenLongerSuperLongCipherKeyString")]
    [InlineData("ThisIsAnEvenLongerSuperLongTestString", "ThisIsAnEvenLongerSuperLongCipherKeyString")]
    [InlineData("TestApiKey", "UnitedExpressCorporationFoundedIn2947")]
    [InlineData("0e80186321a7c8b7c316f31521a596795ce63462", "UnitedExpressCorporationFoundedIn2947")]
    public void EncryptDecrypt_ShouldReturnOriginalString(string stringToEncrypt, string encryptionKeyString)
    {
        // Assemble

        // Act
        var encryptedString = SimpleCipher.Encrypt(stringToEncrypt, encryptionKeyString);
        var decryptedString = SimpleCipher.Decrypt(encryptedString, encryptionKeyString);

        // Assert
        decryptedString.Should().Be(stringToEncrypt);
    }

    [Theory]
    [InlineData("Viz6DAUqzKgg68HzX+14myAUc66trsBTAeCLEVJLHEqB1CEtkTD0lf4UfW9+FVbyDOwVmNbax8rCbW3hctdTkdB/jhi+oSCuR/OpYB/PtRo=", "UnitedExpressCorporationFoundedIn2947", "0e80186321a7c8b7c316f31521a596795ce63462")]
    public void Decrypt_ShouldReturnOriginalString(string encryptedString, string encryptionKeyString, string expectedValue)
    {
        // Assemble

        // Act
        var decryptedString = SimpleCipher.Decrypt(encryptedString, encryptionKeyString);

        // Assert
        decryptedString.Should().Be(expectedValue);
    }
}