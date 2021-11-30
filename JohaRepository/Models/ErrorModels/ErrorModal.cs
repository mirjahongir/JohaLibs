
namespace JohaRepository.Models.ErrorModels
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorModal
    {
        public int Code { get; set; }
        public string ProjectId { get; set; }
        public int HttpStatus { get; set; } = 400;
        public string Message { get; set; } = "Comming soon Error";
        public string Link { get; set; } = "Comming soon Error";
        public string UzbText { get; set; } = "Comming soon Error";

        public string RusText { get; set; } = "Comming soon Error";
        public string EngText { get; set; } = "Comming soon Error";

    }
}
