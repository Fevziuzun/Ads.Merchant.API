namespace Ads.Merchant.API.Repositories;

public interface IRepository
{
    public Models.Merchant GetById(string id);
    public Models.Merchant AddMerchant(Models.Merchant merchant);

    public List<Models.Merchant> GetAll();
    public  Task<bool> UpdateMerchantAsync(Models.Merchant merchant);
    public bool Delete(string id);
}