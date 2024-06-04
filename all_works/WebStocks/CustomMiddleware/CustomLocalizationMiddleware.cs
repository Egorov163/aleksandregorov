using System.Globalization;

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
            var localFromCookie = context.Request.Cookies["languages"];
            if (localFromCookie == null) 
            {
                string acceptLanguage = context.Request.Headers.AcceptLanguage;
                var locale = acceptLanguage.Substring(0, 2);

                culture = new System.Globalization.CultureInfo(locale);
            }
            else 
            {
                culture = new System.Globalization.CultureInfo(localFromCookie);
            }

            

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;


            await _next.Invoke(context);
        }
    }
}
