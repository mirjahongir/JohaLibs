using JohaRepository.Models.ErrorModels;

using System;
using System.Collections.Generic;
using System.Text;

namespace JohaRepository.Exception
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

        public RepoException() : base("")
        {

        }
        public RepoException(List<ErrorModal> list) : base("")
        {
            ErrorModels = list;

        }


        public RepoException(int code) : this()
        {
            Code = code;
        }
        public RepoException(int code, int status) : this(code)
        {
            Status = status;
        }
        public RepoException(string message) : base(message)
        {

        }
        public RepoException(int code, string message) : this(message)
        {

            Code = code;
        }
        public RepoException(int code, int status, string message) : this(message)
        {
            Code = code;
            Status = status;
        }

        public RepoException(int code, int status, object data) : this(code, status)
        {
            Data = data;
        }


    }
}
