using Ads.Merchant.API.V1.Models.RequestModels;
using FluentValidation;

namespace Ads.Merchant.API.V1.Models;

public class MerchantRequestValidationModel : AbstractValidator<MerchantCreateRequestModel>
{

    public MerchantRequestValidationModel()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name Cannot Be Null");
        RuleFor(x => x.Number).Matches("^0\\d+").WithMessage("Phone number must start with 0")
            .Must(number => number.Length == 11).WithMessage("Phone number must have exactly 11 digits");

    }
    
}