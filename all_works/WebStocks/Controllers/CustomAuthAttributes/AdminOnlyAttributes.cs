﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebStocks.Services;

namespace WebStocks.Controllers.CustomAuthAttributes
{
    public class AdminOnlyAttributes : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authService = context.HttpContext.RequestServices.GetService<AuthService>();

            if (!authService.IsAdmin())
            {
                context.Result = new ForbidResult(AuthController.AUTH_KEY);
            }
        }
    }
}
