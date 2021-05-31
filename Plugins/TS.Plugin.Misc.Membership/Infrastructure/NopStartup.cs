using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Ts.Plugin.Misc.Membership.Filters;

namespace Ts.Plugin.Misc.Membership.Infrastructure
{
    public class NopStartup : INopStartup
    {
        public int Order => 10;

        public void Configure(IApplicationBuilder application)
        {
            
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<CatalogCategoryResultFilter>();
                options.Filters.Add<ProductDetailsResultFilter>();
            });
        }
    }
}
