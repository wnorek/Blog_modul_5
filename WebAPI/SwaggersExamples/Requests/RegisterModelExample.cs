using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.SwaggersExamples.Requests
{
    public class RegisterModelExample : IExamplesProvider<RegisterModel>
    {
        public RegisterModel GetExamples()
        {
            return new RegisterModel
            { 
                UserName = "yourUniqueName",
                Email = "yourEmailAddress@example.com",
                Password = "Pafsdgfdh"
            };

        }
    }
}
