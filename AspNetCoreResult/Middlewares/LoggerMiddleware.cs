using JohaRepository.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreResult.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        ILogger<LoggerModel> _log;
        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerModel> log)
        {
            _next = next;
            _log = log;
        }
        public async Task ParseRequest(HttpContext context, LoggerModel model)
        {
            context.Request.EnableBuffering();
            var body = context.Request.Body;
            var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
            await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            context.Request.Body.Position = 0;
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            model.RequestData = bodyAsText;
            model.Method = context.Request.Method;
            model.Query = context.Request.Query.ToDictionary(m => m.Key, k => k.Value.ToString());

        }
        public async Task ParseResponse(HttpContext context, LoggerModel model)
        {
            Stream originalBody = context.Response.Body;
            try
            {
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    await _next.Invoke(context);
                    watch.Stop();
                    model.ElepsedMillesecund = watch.ElapsedMilliseconds;
                    watch = null;
                    memStream.Position = 0;
                    string responseBody = new StreamReader(memStream).ReadToEnd();

                    model.ResponseData = responseBody;
                    model.ResponseStatus = context.Response.StatusCode;

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }

            }
            catch (Exception ext)
            {
                model.ParseError(ext);

            }
        }
        public async Task InvokeAsync(HttpContext context)
        {

            LoggerModel model = new LoggerModel();
            model.UserName = context.User?.Identity?.Name;
            model.DateTime = DateTime.Now;
            await ParseRequest(context, model);
            await ParseResponse(context, model);
            _log.LogInformation(JsonConvert.SerializeObject(model));
        }



    }
}
