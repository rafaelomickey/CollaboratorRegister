using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CollaboratorRegisterApi.Config
{
    [ExcludeFromCodeCoverage]
    public class ApiKeyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var result = true;
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                result = false;

            var token = string.Empty;
            if (result)
            {
                token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
                result = token == Environment.GetEnvironmentVariable("ApiKeyTesteZup");
            }

            if (!result)
            {
                context.ModelState.AddModelError("Erro", "Não Autorizado");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }
        }
    }
}
