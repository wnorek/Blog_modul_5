using Bloger.Sdk;
using Blogger.Contracts.Responses;
using Refit;
using System;
using System.Threading.Tasks;

namespace Blogger.Sdk.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var identityApi = RestService.For<IIdentityApi>("https://localhost:44387/");

            var register = await identityApi.RegisterAsync(new RegisterModel()
            {
                Email = "sdkaccount@gmail.com",
                UserName = "sdkaccount",
                Password = "Pa$$w0rd123!"
            });

            var login = await identityApi.LoginAsync(new LoginModel()
            {
                UserName = "sdkaccount",
                Password = "Pa$$w0rd123!"
            });
        }
    }
}
