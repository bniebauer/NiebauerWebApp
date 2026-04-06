using System;
using Microsoft.Azure.Cosmos;

namespace NiebauerWebApp.Server.Services;

public class CosmosDbService
{

    private readonly CosmosClient _client;
    private readonly Database database;

    public CosmosDbService(IConfiguration config)
    {
        _client = new CosmosClient(config["CosmosDb:ConnectionString"]);
        database = _client.GetDatabase(config["CosmosDb:DatabaseName"]);
    }

    private Container GetContainer(string name) => database.GetContainer(name);

    public async Task<List<T>> GetAllAsync<T>(string containerName)
    {
        var container = GetContainer(containerName);
        var query = container.GetItemQueryIterator<T>("SELECT * FROM c");
        var results = new List<T>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }

    public async Task AddAsync<T>(string containerName, T item, string id)
    {
        var container = GetContainer(containerName);
        await container.CreateItemAsync(item, new PartitionKey(id));
    }

}
