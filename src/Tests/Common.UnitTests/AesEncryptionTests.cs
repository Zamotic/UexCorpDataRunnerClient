using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Common;

namespace Common.UnitTests;
public class AesEncryptionTests
{
    [Theory]
    [InlineData("TestString", "TestCipherKey")]
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
    [InlineData("97b03f4a6a64e1ff90fca2da4ce68fbae7103d37", "UnitedExpressCorporationFoundedIn2947")]
    [InlineData("c7379b7905a6e68c6440387aa913d6d50f6e470f", "UnitedExpressCorporationFoundedIn2947")]
    public void EncryptDecrypt_ShouldReturnOriginalString(string stringToEncrypt, string encryptionKeyString)
    {
        // Assemble
        AesEncryption aesEncryption = new(encryptionKeyString);

        // Act
        var encryptedString = aesEncryption.Encrypt(stringToEncrypt);
        var decryptedString = aesEncryption.Decrypt(encryptedString);

        // Assert
        decryptedString.Should().Be(stringToEncrypt);
    }

    [Theory]
    [InlineData("+7LIrgPBb5qyCf+W5avas6RlWRkRX/K0crETuv3Z+lH9uHfR/hTeNaRSU99Xlwho", "UnitedExpressCorporationFoundedIn2947", "0e80186321a7c8b7c316f31521a596795ce63462")]
    [InlineData("FoGk3H4kH1DnbSHkBRtoyyPQ/Uo/Ar0VPSXaqdVtI4RgoB4zJ25CiOH7ne5JzzbH", "UnitedExpressCorporationFoundedIn2947", "97b03f4a6a64e1ff90fca2da4ce68fbae7103d37")]
    [InlineData("tFzGU35mHdBZVBVO9TMR/muwuHz8P7TimgK66fSj1wrBoCUsEL7ea9TVuJGakVvQ", "UnitedExpressCorporationFoundedIn2947", "c7379b7905a6e68c6440387aa913d6d50f6e470f")]
    public void Decrypt_ShouldReturnOriginalString(string encryptedString, string encryptionKeyString, string expectedValue)
    {
        // Assemble
        AesEncryption aesEncryption = new(encryptionKeyString);

        // Act
        var decryptedString = aesEncryption.Decrypt(encryptedString);

        // Assert
        decryptedString.Should().Be(expectedValue);
    }
}
