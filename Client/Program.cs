using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddGraphClient(
                "profile",
                "openid",
                "email",
                "https://graph.microsoft.com/User.Read", 
                "https://graph.microsoft.com/User.Read.All", 
                "https://graph.microsoft.com/User.ReadWrite.All", 
                "https://graph.microsoft.com/Mail.Send",
                "https://graph.microsoft.com/Files.ReadWrite.All",
                "https://graph.microsoft.com/Mail.ReadWrite");

            //集成微软账户Authentication
            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.LoginMode = "popup";
                options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
                options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
            });

            //注册BootstrapBlazor服务
            builder.Services.AddBootstrapBlazor();

            await builder.Build().RunAsync();
        }
    }
}
