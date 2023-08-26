using Ads.Merchant.API.Configs;
using Ads.Merchant.API.Repositories;
using Ads.Merchant.API.Services;
using FluentValidation.AspNetCore;
using MongoDB.Driver;


namespace Ads.Merchant.API;

public class Startup
{
    
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get;}
    
    

    public void ConfigureServices(IServiceCollection services)
    
    {
        
        
        services.Configure<MongoDBsettings>(Configuration.GetSection("MongoDBsettings"));
        services.AddSingleton<IRepository, Repository>();
        services.AddSingleton<IService, Service>();
        

        services.AddControllers(options => { options.OutputFormatters.Insert(0, new CustomOutputFormatter()); })
            .AddFluentValidation(s => s.RegisterValidatorsFromAssemblyContaining<Startup>())
            .AddNewtonsoftJson();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseRouting();
        app.UseAuthorization();
        app.UseHttpsRedirection();

        var errorHandlingMiddleware = new ErrorHandlingSettings();
        errorHandlingMiddleware.Configure(app);
        
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}

