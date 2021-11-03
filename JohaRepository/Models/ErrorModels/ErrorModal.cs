using System.Collections.Generic;

namespace JohaRepository.Models.ErrorModels
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorModal
    {
        public int Code { get; set; }
        public int ProjectId { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Link { get; set; }
        public string UzbText { get; set; }
      
        public string RusText { get; set; }
        public string EngText { get; set; }
        public List<ErrorModal> Errors { get; set; }

        public ErrorModal ToError()
        {
            return null;
        }
    }
}
