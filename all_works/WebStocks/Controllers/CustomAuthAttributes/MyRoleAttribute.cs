using Microsoft.AspNetCore.Mvc.Filters;

namespace WebStocks.Controllers.CustomAuthAttributes
{
    public class MyRoleAttribute : Attribute, IAuthorizationFilter
    {
        private string _roleName;

        public MyRoleAttribute(string roleName)
        {
            _roleName = roleName;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
