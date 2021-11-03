using JohaRepository.Models.ErrorModels;

using LiteDB;

using Microsoft.AspNetCore.Http;

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreResult
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ResultLogic
    {

        private static IHttpContextAccessor _httpContext;
        public static IHttpContextAccessor HttpContext
        {
            get
            {
                return _httpContext;
            }
            set
            {
                _httpContext = value;
            }
        }
        public static JohaRepository.Models.ErrorConnection Connection { get; set; }
    }
    /// <summary>
    /// Error Handler
    /// </summary>
    public static partial class ResultLogic
    {
        internal static bool ConnectionExist { get; set; } = false;
        public static ErrorModal GetError(this int code)
        {
            if (ConnectionExist)
            {
                var result = ErrorModals.FindOne(m => m.Code == code);
                return result;
            }
            return null;

        }

        private static ILiteCollection<ErrorModal> _errorModals;
        public static ILiteDatabase liteDatabase { get; set; }
        public static ILiteDatabase LiteDatabase
        {
            get
            {

                if (!ConnectionExist) return null;
                if (liteDatabase == null)
                {
                    liteDatabase = new LiteDatabase(Connection?.DbName ?? "error.db");
                }
                return liteDatabase;
            }
        }
        public static ILiteCollection<ErrorModal> ErrorModals
        {
            get
            {
                if (_errorModals == null)
                {
                    _errorModals = liteDatabase.GetCollection<ErrorModal>();

                }
                return _errorModals;
            }
        }


        public static HttpClient Client
        {
            get
            {
                HttpClientHandler handler = new HttpClientHandler()
                {

                };
                // ... Use HttpClient.            
                HttpClient client = new HttpClient(handler);
                client.BaseAddress = new Uri(Connection.Url);
                var byteArray = Encoding.ASCII.GetBytes(Connection.LoginName + ":" + Connection.Password);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                return client;
            }
        }
        public static async Task<object> GetErrorList()
        {
            var result = await Client.GetAsync("");
            var erroroList = result.Content;
            return erroroList;
        }

    }
}
