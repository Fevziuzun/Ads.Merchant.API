using Ads.Merchant.API.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Ads.Merchant.API.Repositories;

public class Repository : IRepository
{
    
    private readonly IMongoCollection<Models.Merchant> _merchants;

    public Repository(IOptions<MongoDBsettings> mongodbSettings)
    {
        var client = new MongoClient(mongodbSettings.Value.ConnectionString);
        var database = client.GetDatabase(mongodbSettings.Value.DatabaseName);
        _merchants = database.GetCollection<Models.Merchant>("merchants");
    }
    
    public Models.Merchant AddMerchant(Models.Merchant merchant)
    {
        _merchants.InsertOne(merchant);
        return merchant;
    }
    
    
    public Models.Merchant GetById(string id)
    {
        return _merchants.Find(x => x.Id == id).FirstOrDefault();
    }

    public List<Models.Merchant> GetAll()
    {
        var merchantList = _merchants.Find(Builders<Models.Merchant>.Filter.Empty).ToList();
        return merchantList;
    }
    
    public async Task<bool> UpdateMerchantAsync(Models.Merchant merchant)
    {
        var filter = Builders<Models.Merchant>.Filter.Eq("_id", merchant.Id);
        var update = Builders<Models.Merchant>.Update
            .Set(item => item.Name, merchant.Name);
        var updateResult = await _merchants.UpdateOneAsync(filter, update);
        return updateResult.ModifiedCount > 0;
    }
    
    public bool Delete(string id)
    {
        var filter = Builders<Models.Merchant>.Filter.Eq("_id", id);
        var deleteResult = _merchants.DeleteOne(filter);
        return deleteResult.DeletedCount > 0;
    }


}