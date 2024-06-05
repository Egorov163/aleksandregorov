using System.Globalization;
using WebStocks.Services;

namespace WebStocks.CustomMiddleware
{
    public class CustomLocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomLocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            CultureInfo culture;

            var authService = context.RequestServices.GetService<AuthService>();

            if (authService.IsAuthenticated())
            {
                culture = new CultureInfo(authService.GetCurrentUserLocale());
            }

            else if (context.Request.Cookies["languages"] != null)
            {
                var localFromCookie = context.Request.Cookies["languages"];
                culture = new CultureInfo(localFromCookie);
            }
            else
            {
                string acceptLanguage = context.Request.Headers.AcceptLanguage;
                var locale = acceptLanguage.Substring(0, 2);

                culture = new CultureInfo(locale);
            }



            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;


            await _next.Invoke(context);
        }
    }
}
