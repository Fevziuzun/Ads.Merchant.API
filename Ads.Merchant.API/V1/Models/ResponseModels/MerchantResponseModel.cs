namespace Ads.Merchant.API.V1.Models.ResponseModels;

public class MerchantResponseModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    
    public string Number { get; set; }

    public MerchantResponseModel ToResponse(API.Models.Merchant merchant)
    {
        return new MerchantResponseModel
        {
            Id = merchant.Id,
            Name = merchant.Name,
            Number = merchant.Number
            
        };
    }
}