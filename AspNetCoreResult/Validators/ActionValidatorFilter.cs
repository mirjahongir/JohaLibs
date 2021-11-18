using AspNetCoreResult.ResponseCoreResult;

using JohaRepository.Attributes.Auth;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

using Newtonsoft.Json;

using System;
using System.Collections;
using System.Collections.Generic;
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

        public ActionValidatorFilter()
        {

        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            JsonConvert.SerializeObject(context.ActionArguments);

            ChackUserInfo(context);
            if (context.ModelState.IsValid)
            {
                await next();

                return;
            }
            var coreResult = new CoreResult<object>(context.ModelState);
            context.HttpContext.Response.StatusCode = 400;
            context.HttpContext.Response.ContentType = "application/json";
            await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(coreResult));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
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

                var obj = Activator.CreateInstance(userInfo.PropertyType);
                foreach (var i in userInfo.PropertyType.GetProperties())
                {
                    var attr = i.GetCustomAttribute<JwtPropertyAttribute>();
                    if (attr != null)
                        SetJwtValueToProperty(context.HttpContext.User, i, obj, attr);
                }


                userInfo.SetValue(arg.Value, obj);


            }

        }
        public void SetJwtValueToProperty(ClaimsPrincipal claim,
            PropertyInfo prop,
            object value,
            JwtPropertyAttribute attr)
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
        public object GetValue(string typeName, string jwtValue)
        {
            switch (typeName)
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
                    double d;
                    double.TryParse(jwtValue, out d);
                    if (d == 0) { }
                    return d;

                default: return null;
            }
        }
        public object JwtValue(ClaimsPrincipal claim, PropertyInfo prop, string jwtKey)
        {
            var jwtValue = claim?.Claims?.FirstOrDefault(m => string.Equals(jwtKey, m.Type, System.StringComparison.OrdinalIgnoreCase))?.Value;
            if (string.IsNullOrEmpty(jwtValue)) return null;
            switch (prop.PropertyType.Name.ToLower())
            {
                case "list`1":
                    var claims = claim.Claims.Where(m => m.Type == jwtKey).ToList();
                    return SetClaimsToListProperty(claims, prop);
                default:
                    return GetValue(prop.PropertyType.Name.ToLower(), jwtValue);

            }
        }
        public object SetClaimsToListProperty(List<Claim> claims, PropertyInfo prop)
        {

            var propType = prop.PropertyType.GenericTypeArguments[0].Name.ToLower();
            var instance = new ArrayList();

            foreach (var claim in claims)
            {
                instance.Add(GetValue(propType, claim.Value));
            }
            switch (propType)
            {
                case "string":
                    return instance.Cast<string>().ToList();
                case "int32":
                    return instance.Cast<int>().ToList();
                case "int64": return instance.Cast<long>().ToList();
                case "double": return instance.Cast<double>().ToList();
                default: return null;
            }
        }
    }
}
