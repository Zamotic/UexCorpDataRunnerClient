using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Business.Common;
using UexCorpDataRunner.Domain.Configurations;

namespace UexCorpDataRunner.DesktopClient.WebClient;
public class UexCorpWebApiConfiguration : IUexCorpWebApiConfiguration
{
    public const string ConfigurationSectionName = "UEXWebApiConfig";

    public string? WebApiEndPointUrl { get; set; }
    public string? DataRunnerEndpointPath { get; set; }

    
    public UexCorpWebApiConfiguration()
    {
        //NameValueCollection? configurationValues = configuration.GetSection(Globals.WebApiConfigSectionName) as NameValueCollection;
        //configuration.GetSection(ConfigurationSectionName).Bind(this);

        //if (WebApiEndPointUrl is null)
        //{
        //    throw new ConfigurationErrorsException("UEX Corp Web Api Configuration Settings have not been defined.");
        //}

        //WebApiEndPointUrl = configurationValues["WebApiEndpointUrl"];
        //DataRunnerEndpointPath = configurationValues["DataRunnerEndpointPath"];
    }
}
/*
public class UexCorpWebApiConfiguration : IUexCorpWebApiConfiguration
{
    public string? WebApiEndPointUrl { get; private set; }
    public string? DataRunnerEndpointPath { get; private set; }
    //public string? AuthUser { get; private set; }
    //public string? AuthPass { get; private set; }

    //string IWebApiConfiguration.WebApiEndPointUrl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    #region     Static Configuration Generation
    readonly static UexCorpWebApiConfiguration _Configuration;

    static UexCorpWebApiConfiguration()
    {
        NameValueCollection? configurationValues = ConfigurationManager.GetSection(Globals.WebApiConfigSectionName) as NameValueCollection;

        if (configurationValues is null)
        {
            throw new ConfigurationErrorsException("UEX Corp Web Api Configuration Settings have not been defined.");
        }

        //string decryptedApiKey = DecryptValue(configurationValues["ApiKey"]);
        //string decryptedAuthPass = DecryptValue(configurationValues["AuthPass"]);

        _Configuration = new UexCorpWebApiConfiguration()
        {
            WebApiEndPointUrl = configurationValues["WebApiEndpointUrl"],
            DataRunnerEndpointPath = configurationValues["DataRunnerEndpointPath"]
        };
    }

    //static string DecryptValue(string encryptedValue)
    //{
    //    string decryptedValue = StringCipher.Decrypt(encryptedValue, Business.Globals.GeneralCipherSalt);
    //    return decryptedValue;
    //}


    public static UexCorpWebApiConfiguration GetConfig()
    {
        return _Configuration;
    }
    #endregion  Static Configuration Generation
}
*/