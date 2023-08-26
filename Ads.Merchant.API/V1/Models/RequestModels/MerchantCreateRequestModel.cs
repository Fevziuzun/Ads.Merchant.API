namespace Ads.Merchant.API.V1.Models.RequestModels;

public class MerchantCreateRequestModel
{
    public string Name { get; set; }
    public string Number { get; set; }
   
     // public API.Models.Merchant TurnToMerchantWithId(string id)
     // {
     //     return new API.Models.Merchant()
     //    {
     //         Name = Name,
     //         Id = id,
     //        Number = Number
     //    };
     // }
}