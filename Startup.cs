using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace corelsp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddBlazorStyled();
            //serviceCollection.AddBlazorStyled();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");                              
        }
    }
}
