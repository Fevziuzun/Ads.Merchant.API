using Ads.Merchant.API.Repositories;

namespace Ads.Merchant.API.Services;

public class Service : IService
{
    private readonly IRepository _repository;

    public Service(IRepository repository)
    {
        _repository = repository;
    }

    public Models.Merchant AddMerchant(Models.Merchant merchant)
    {
        _repository.AddMerchant(merchant);
        return merchant;
    }

    public Models.Merchant GetById(string id)
    {
        var merchant = _repository.GetById(id);
        return merchant;
    }

    public List<Models.Merchant> GetAll()
    {
        return _repository.GetAll();
    }

    public async Task<bool> Update(Models.Merchant merchant)
    {
        return await _repository.UpdateMerchantAsync(merchant);
    }

    public bool Delete(string id)
    {
        var success = _repository.Delete(id);
        return success;
    }

}
        
    
