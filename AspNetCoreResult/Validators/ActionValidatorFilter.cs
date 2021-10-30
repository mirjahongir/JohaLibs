using JohaRepository.Attributes.Auth;

using Microsoft.AspNetCore.Mvc.Filters;

using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreResult.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionValidatorFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ChackUserInfo(context);
            if (context.ModelState.IsValid)
            {
                await next();
            }

        }
        internal void ChackUserInfo(ActionExecutingContext context)
        {
            foreach (var arg in context.ActionArguments)
            {
                var modelAttribute = arg.Value.GetType().GetCustomAttribute<AuthModelAttribute>();
                if (modelAttribute == null)
                {
                    break;
                }
                var properties = arg.Value.GetType().GetProperties().Where(m => m.GetCustomAttribute<JwtPropertyAttribute>() != null);
                var userInfo = arg.Value.GetType().GetProperties().FirstOrDefault(m => m.GetCustomAttribute<UserInfoAttribute>() != null);

                foreach (var prop in properties)
                {
                    var attr = prop.GetCustomAttribute<JwtPropertyAttribute>();

                    SetJwtValueToProperty(context.HttpContext.User, prop, arg.Value, attr);
                }



            }

        }
        public void SetJwtValueToProperty(ClaimsPrincipal claim, PropertyInfo prop, object value, JwtPropertyAttribute attr)
        {

            var jwtValue = JwtValue(claim, prop, attr.JwtKey);
            if (jwtValue == null)
            {
                if (attr.IsRequired)
                {

                }
                else
                {
                    return;
                }
            }

            prop.SetValue(value, jwtValue);
        }
        public object JwtValue(ClaimsPrincipal claim, PropertyInfo prop, string jwtKey)
        {
            var jwtValue = claim.FindFirst(m => string.Equals(jwtKey, m.Type, System.StringComparison.OrdinalIgnoreCase)).Value;
            if (string.IsNullOrEmpty(jwtValue)) return null;
            switch (prop.PropertyType.Name.ToLower())
            {
                case "string":
                    return jwtValue;
                case "int64":
                    long a;
                    long.TryParse(jwtValue, out a);
                    if (a == 0)
                    {
                        return null;
                    }
                    return a;

                case "int32":
                    int b;
                    int.TryParse(jwtValue, out b);
                    if (b == 0)
                    {

                    }
                    return b;

                case "double": 
                    
                    break;
                case "list`1": break;
                default: return null;
            }
            return null;
        }

    }
}
