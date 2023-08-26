namespace Ads.Merchant.API.Services;

public interface IService
{
    public Models.Merchant GetById(string id);
    public Models.Merchant AddMerchant(Models.Merchant merchant);
    public List<Models.Merchant> GetAll();
    public  Task<bool> Update(Models.Merchant merchant);
    bool Delete(string id);
   

}