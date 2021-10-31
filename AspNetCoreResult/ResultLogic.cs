using JohaRepository.Models.ErrorModels;

using LiteDB;

using Microsoft.AspNetCore.Http;

namespace AspNetCoreResult
{
    /// <summary>
    /// 
    /// </summary>
    internal partial class ResultLogic
    {

        private static HttpContext _httpContext;
        public static HttpContext HttpContext
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
    internal static partial class ResultLogic
    {
        public static ErrorModal GetError(this int code)
        {
            var result = ErrorModals.FindOne(m => m.Code == code);
            return result;
        }


        private static ILiteCollection<ErrorModal> _errorModals;
        public static ILiteDatabase liteDatabase { get; set; }
        public static ILiteDatabase LiteDatabase
        {
            get
            {

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

    }
}
