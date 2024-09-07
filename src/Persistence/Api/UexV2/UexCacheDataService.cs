﻿using UexCorpDataRunner.Domain.DataRunnerV2;

namespace UexCorpDataRunner.Persistence.Api.UexV2;
public class UexCacheDataServiceV2 : UexDataServiceV2
{
    Dictionary<Type, Dictionary<string, dynamic>> _cacheDictionary = new Dictionary<Type, Dictionary<string, dynamic>>();

    readonly object _lockObject = new object();

    public UexCacheDataServiceV2(IUexCorpWebApiClientAdapter webApiClientAdapter)
        : base(webApiClientAdapter)
    {
    }

    public override async Task<GameVersion> GetCurrentVersionAsync()
    {
        GameVersion gameVersion;
        var type = typeof(GameVersion);
        if (_cacheDictionary.Keys.Contains(type) == true)
        {
            var typeDictionary = _cacheDictionary[type];
            if (typeDictionary.Keys.Contains(string.Empty) == true)
            {
                var returnObject = typeDictionary[string.Empty] as GameVersion;
                if (returnObject is not null)
                {
                    return returnObject;
                }
            }

            gameVersion = await base.GetCurrentVersionAsync();
            typeDictionary.Add(string.Empty, gameVersion);
        }

        gameVersion = await base.GetCurrentVersionAsync();
        lock(_lockObject)
        {
            if (_cacheDictionary.Keys.Contains(type) == false)
            {
                var collectionDictionary = new Dictionary<string, dynamic>() { { string.Empty, gameVersion } };
                _cacheDictionary.Add(type, collectionDictionary);
            }
        }

        return gameVersion;
    }

    public override async Task<IReadOnlyCollection<StarSystem>> GetAllSystemsAsync()
    {
        IReadOnlyCollection<StarSystem> collection;
        var type = typeof(StarSystem);
        if (_cacheDictionary.Keys.Contains(type) == true)
        {
            var typeDictionary = _cacheDictionary[type];
            if(typeDictionary.Keys.Contains(string.Empty) == true)
            {
                var returnCollection = typeDictionary[string.Empty] as IReadOnlyCollection<StarSystem>;
                if(returnCollection is not null)
                {
                    return returnCollection;
                }                
            }

            collection = await base.GetAllSystemsAsync();
            typeDictionary.Add(string.Empty, collection);
        }

        collection = await base.GetAllSystemsAsync();
        var collectionDictionary = new Dictionary<string, dynamic>() { { string.Empty, collection } };
        _cacheDictionary.Add(type, collectionDictionary);
        return collection;
    }

    public override async Task<IReadOnlyCollection<Terminal>> GetTerminalsAsync(int starSystemId)
    {
        string starSystemIdString = starSystemId.ToString();
        IReadOnlyCollection<Terminal> collection;
        var type = typeof(Terminal);
        if (_cacheDictionary.Keys.Contains(type) == true)
        {
            var typeDictionary = _cacheDictionary[type];
            if (typeDictionary.Keys.Contains(starSystemIdString) == true)
            {
                var returnCollection = typeDictionary[starSystemIdString] as IReadOnlyCollection<Terminal>;
                if (returnCollection is not null)
                {
                    return returnCollection;
                }
            }

            collection = await base.GetTerminalsAsync(starSystemId);
            typeDictionary.Add(starSystemIdString, collection);
        }

        collection = await base.GetTerminalsAsync(starSystemId);
        var collectionDictionary = new Dictionary<string, dynamic>() { { starSystemIdString, collection } };
        _cacheDictionary.Add(type, collectionDictionary);
        return collection;
    }

    //public override async Task<IReadOnlyCollection<Planet>> GetAllPlanetsAsync(string systemCode)
    //{
    //    IReadOnlyCollection<Planet> collection;
    //    var type = typeof(Planet);
    //    if (_cacheDictionary.Keys.Contains(type) == true)
    //    {
    //        var typeDictionary = _cacheDictionary[type];
    //        if (typeDictionary.Keys.Contains(systemCode) == true)
    //        {
    //            var returnCollection = typeDictionary[systemCode] as IReadOnlyCollection<Planet>;
    //            if (returnCollection is not null)
    //            {
    //                return returnCollection;
    //            }
    //        }

    //        collection = await base.GetAllPlanetsAsync(systemCode);
    //        typeDictionary.Add(systemCode, collection);
    //    }

    //    collection = await base.GetAllPlanetsAsync(systemCode);
    //    var collectionDictionary = new Dictionary<string, dynamic>();
    //    collectionDictionary.Add(systemCode, collection);
    //    _cacheDictionary.Add(type, collectionDictionary);
    //    return collection;
    //}

    //public override async Task<IReadOnlyCollection<Satellite>> GetAllSatellitesAsync(string systemCode)
    //{
    //    IReadOnlyCollection<Satellite> collection;
    //    var type = typeof(Satellite);
    //    if (_cacheDictionary.Keys.Contains(type) == true)
    //    {
    //        var typeDictionary = _cacheDictionary[type];
    //        if (typeDictionary.Keys.Contains(systemCode) == true)
    //        {
    //            var returnCollection = typeDictionary[systemCode] as IReadOnlyCollection<Satellite>;
    //            if (returnCollection is not null)
    //            {
    //                return returnCollection;
    //            }
    //        }

    //        collection = await base.GetAllSatellitesAsync(systemCode);
    //        typeDictionary.Add(systemCode, collection);
    //    }

    //    collection = await base.GetAllSatellitesAsync(systemCode);
    //    var collectionDictionary = new Dictionary<string, dynamic>();
    //    collectionDictionary.Add(systemCode, collection);
    //    _cacheDictionary.Add(type, collectionDictionary);
    //    return collection;
    //}

    //public override async Task<IReadOnlyCollection<City>> GetAllCitiesAsync(string systemCode)
    //{
    //    IReadOnlyCollection<City> collection;
    //    var type = typeof(City);
    //    if (_cacheDictionary.Keys.Contains(type) == true)
    //    {
    //        var typeDictionary = _cacheDictionary[type];
    //        if (typeDictionary.Keys.Contains(systemCode) == true)
    //        {
    //            var returnCollection = typeDictionary[systemCode] as IReadOnlyCollection<City>;
    //            if (returnCollection is not null)
    //            {
    //                return returnCollection;
    //            }
    //        }

    //        collection = await base.GetAllCitiesAsync(systemCode);
    //        typeDictionary.Add(systemCode, collection);
    //    }

    //    collection = await base.GetAllCitiesAsync(systemCode);
    //    var collectionDictionary = new Dictionary<string, dynamic>();
    //    collectionDictionary.Add(systemCode, collection);
    //    _cacheDictionary.Add(type, collectionDictionary);
    //    return collection;
    //}

    //public override async Task<IReadOnlyCollection<Tradeport>> GetAllTradeportsAsync(string systemCode)
    //{
    //    IReadOnlyCollection<Tradeport> collection;
    //    var type = typeof(Tradeport);
    //    if (_cacheDictionary.Keys.Contains(type) == true)
    //    {
    //        var typeDictionary = _cacheDictionary[type];
    //        if (typeDictionary.Keys.Contains(systemCode) == true)
    //        {
    //            var returnCollection = typeDictionary[systemCode] as IReadOnlyCollection<Tradeport>;
    //            if (returnCollection is not null)
    //            {
    //                return returnCollection;
    //            }
    //        }

    //        collection = await base.GetAllTradeportsAsync(systemCode);
    //        typeDictionary.Add(systemCode, collection);
    //    }

    //    collection = await base.GetAllTradeportsAsync(systemCode);
    //    var collectionDictionary = new Dictionary<string, dynamic>();
    //    collectionDictionary.Add(systemCode, collection);
    //    _cacheDictionary.Add(type, collectionDictionary);
    //    return collection;
    //}

    public override async Task<IReadOnlyCollection<Commodity>> GetAllCommoditiesAsync()
    {
        IReadOnlyCollection<Commodity> collection;
        var type = typeof(Commodity);
        if (_cacheDictionary.Keys.Contains(type) == true)
        {
            var typeDictionary = _cacheDictionary[type];
            if (typeDictionary.Keys.Contains(string.Empty) == true)
            {
                var returnCollection = typeDictionary[string.Empty] as IReadOnlyCollection<Commodity>;
                if (returnCollection is not null)
                {
                    return returnCollection;
                }
            }

            collection = await base.GetAllCommoditiesAsync();
            typeDictionary.Add(string.Empty, collection);
        }

        collection = await base.GetAllCommoditiesAsync();
        var collectionDictionary = new Dictionary<string, dynamic>() { { string.Empty, collection } };
        _cacheDictionary.Add(type, collectionDictionary);
        return collection;
    }
}