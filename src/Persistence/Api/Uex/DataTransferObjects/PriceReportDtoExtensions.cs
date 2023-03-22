using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UexCorpDataRunner.Domain.DataRunner;

namespace UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;
public static class PriceReportDtoExtensions
{
    public static FormUrlEncodedContent? ToFormUrlEncodedContent(this PriceReportDto source)
    {
        if(source is null)
        {
            return null;
        }

        var json = System.Text.Json.JsonSerializer.Serialize(source);

        if(json is null)
        {
            return null;
        }

        var contentDictionary = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(json);

        if(contentDictionary is null) 
        { 
            return null; 
        }

        var formUrlEncodedContent = new FormUrlEncodedContent(contentDictionary);

        return formUrlEncodedContent;
    }
}
