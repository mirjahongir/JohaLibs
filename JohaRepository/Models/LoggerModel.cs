using System;
using System.Collections.Generic;

namespace JohaRepository.Models
{
    public class LoggerModel
    {
        public string UserName { get; set; }
        public object RequestData { get; set; }
        public Dictionary<string, string> Query { get; set; }
        public object ResponseData { get; set; }
        public int StatusCode { get; set; }
        public DateTime DateTime { get; set; }
        public long ElepsedMillesecund { get; set; }
        public int ResponseStatus { get; set; }
        public string Method { get; set; }
        public string ErrorText { get; set; }
        public void ParseError(Exception ext)
        {
            ErrorText = ext.Message;
        }
    }
}