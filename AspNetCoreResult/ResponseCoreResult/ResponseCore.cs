using JohaRepository.Exception;
using JohaRepository.Models.ErrorModels;

using System;

namespace AspNetCoreResult.ResponseCoreResult
{
    /// <summary>
    ///  Core Result Fields
    /// </summary>
    public partial class CoreResult<T>
        where T : class
    {
        public T Result { get; set; }
        public int HttpStatus { get; set; }
        public bool IsSuccess { get; set; } = true;
        public int Code { get; set; }
        public ErrorModal Error { get; set; }

    }

    /// <summary>
    /// Error Handler
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class CoreResult<T>
           where T : class
    {
        //   private Exception ext;

        public CoreResult(Exception ext)
        {
            if (ext is RepoException repoError)
            {
                ParseRepoException(repoError);
                return;
            }
            ParseException(ext);

        }
        public void ParseException(Exception ext)
        {
            SetHttpStatus(400);
        }
        public void ParseRepoException(RepoException ext)
        {
            if (ext.Code != 0)
            {
                var errorModal = ext.Code.GetError();
                ParseErrorModal(errorModal);
            }
            else
            {
                if (ext.Status == 0)
                {
                    SetHttpStatus(ext.Status);
                }
            }
        }
        public void SetHttpStatus(int statusCode, int defaultValue = 400)
        {
            if (statusCode == 0)
            {
                HttpStatus = defaultValue;
                ResultLogic.HttpContext.Response.StatusCode = defaultValue;
            }
            else
            {
                HttpStatus = statusCode;
                ResultLogic.HttpContext.Response.StatusCode = statusCode;

            }
        }
        public void ParseErrorModal(ErrorModal errorModal)
        {
            if (errorModal == null)
            {

            }
            this.Error = errorModal;

            SetHttpStatus(errorModal.StatusCode, 400);

        }
        public void SetIsSuccess(bool isSuccess = true)
        {
            this.IsSuccess = isSuccess;
        }


        public static implicit operator CoreResult<T>(Exception ext)
        {
            return new CoreResult<T>(ext);
        }
        public static implicit operator CoreResult<T>(RepoException ext)
        {
            return new CoreResult<T>(ext);
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class CoreResult<T>
        where T : class
    {

        public CoreResult(T model)
        {
            Result = model;
        }
        public CoreResult(object model)
        {

        }
        public CoreResult()
        {

        }
        public CoreResult(CoreResult<T> model)
        {
            Result = model.Result;
            this.Error = model.Error;
            this.HttpStatus = model.HttpStatus;
            this.IsSuccess = model.IsSuccess;
            this.Code = model.Code;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>

        public static implicit operator CoreResult<T>(T model)
        {
            return new CoreResult<T>(model);
        }
        public static implicit operator CoreResult<T>(int code) => new CoreResult<T>(code);


    }

}
