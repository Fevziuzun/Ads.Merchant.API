using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;


namespace Ads.Merchant.API.Configs;

public class CustomOutputFormatter : TextOutputFormatter
{
    public CustomOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/csv"));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }
    protected override bool CanWriteType(Type? type)
        => typeof(Models.Merchant).IsAssignableFrom(type)
           || typeof(IEnumerable<Models.Merchant>).IsAssignableFrom(type);
    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;
        var buffer = new StringBuilder();
        if (context.Object is IEnumerable<Models.Merchant>)
        {
            foreach (var Merchant in (IEnumerable<Models.Merchant>)context.Object)
            {
                FormatCsv(buffer, Merchant);
            }
        }
        else
        {
            FormatCsv(buffer, (Models.Merchant)context.Object);
        }
        await response.WriteAsync(buffer.ToString(), selectedEncoding);
    }
    private static void FormatCsv(StringBuilder buffer, Models.Merchant merchant)
    {
        buffer.AppendLine($"{merchant.Name},{merchant.Id},{merchant.Number}");
    }
}