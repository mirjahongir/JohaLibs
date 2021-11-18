using JohaRepository.Models.ErrorModels;

using System.Collections.Generic;

namespace JohaRepository.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class RepoException : System.Exception
    {
        public int Code { get; set; }
        public int Status { get; set; }

        public object Data { get; set; }

        public List<ErrorModal> ErrorModels { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RepoException() : base("")
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public RepoException(List<ErrorModal> list) : base("")
        {
            ErrorModels = list;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public RepoException(int code) : this()
        {
            Code = code;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="status"></param>
        public RepoException(int code, int status) : this(code)
        {
            Status = status;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public RepoException(string message) : base(message)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public RepoException(int code, string message) : this(message)
        {

            Code = code;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        public RepoException(int code, int status, string message) : this(message)
        {
            Code = code;
            Status = status;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="status"></param>
        /// <param name="data"></param>
        public RepoException(int code, int status, object data) : this(code, status)
        {
            Data = data;
        }


    }
}
