using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Mobile2020_TokenSample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mobile2020_TokenSample.Infrastructure.Security
{
    public class AuthRequiredAttribute : TypeFilterAttribute
    {
        public AuthRequiredAttribute() : base(typeof(AuthRequiredFilter))
        {
        }

        private class AuthRequiredFilter : IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                ITokenService tokenService = (ITokenService)context.HttpContext.RequestServices.GetService(typeof(ITokenService));

                context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authorizations);
                string token = authorizations.SingleOrDefault(authorization => authorization.StartsWith("Bearer "));

                if (token is null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                User user = tokenService.ValidateToken(token);

                if(user is null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                return;
            }
        }
    }
}
