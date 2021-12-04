using JohaRepository.Exceptions;
using JohaRepository.Models.ErrorModels;

using Microsoft.AspNetCore.Mvc.ModelBinding;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public List<ErrorModal> Errors { get; set; } = new List<ErrorModal>();
    }
    /// <summary>
    /// Check Require Fields
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class CoreResult<T>
    {
        public CoreResult(ModelStateDictionary modelState)
        {
            SetHttpStatus(400);
            SetIsSuccess(false);

            foreach (var i in modelState.Values)
            {
                foreach (var err in i.Errors)
                {
                    ErrorModal modal = new ErrorModal()
                    {
                        RusText = err.ErrorMessage,
                        UzbText = err.ErrorMessage,
                        EngText = err.ErrorMessage
                    };
                }
            }
        }
        public static implicit operator CoreResult<T>(ModelStateDictionary model)
        {
            return new CoreResult<T>(model);
        }

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
        public ErrorModal ParseError(Exception ext)
        {
            ErrorModal result = new ErrorModal();
            result.Message = ext.Message;
            result.UzbText = ext.Message;
            result.RusText = ext.Message;
            result.EngText = ext.Message;
            return result;

        }
        public void ParseException(Exception ext)
        {
            Errors = new List<ErrorModal>();
            Errors.Add(ParseError(ext));
            SetHttpStatus(400);
            SetIsSuccess(false);
        }
        public void ParseRepoException(RepoException ext)
        {
            if (ext.Code != 0)
            {
                SetIsSuccess(false);
                var errorModal = ext.Code.GetError();
                if (errorModal == null)
                {
                    errorModal = new ErrorModal() { Message = ext.Message };
                }
                ParseErrorModal(errorModal);
            }
            else
            {
                if (ext.Status == 0)
                {
                    SetHttpStatus(ext.Status);
                    SetIsSuccess(false);
                    Errors.Add(new ErrorModal()
                    {
                        Message = ext.Message,
                        EngText = ext.Message,
                        UzbText = ext.Message,
                        RusText = ext.Message,

                    });

                }
            }
        }

        public void SetHttpStatus(int statusCode, int defaultValue = 400)
        {
            if (statusCode == 0)
            {
                HttpStatus = defaultValue;

                ResultLogic.HttpContext.HttpContext.Response.StatusCode = defaultValue;
            }
            else
            {
                HttpStatus = statusCode;
                ResultLogic.HttpContext.HttpContext.Response.StatusCode = statusCode;

            }
        }
        public void ParseErrorModal(ErrorModal errorModal)
        {

            this.Errors.Add(errorModal);

            SetHttpStatus(errorModal.HttpStatus, 400);

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
            this.Errors = model.Errors;
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
        public static implicit operator CoreResult<T>(Task<T> model)
        {
            return new CoreResult<T>(model.Result);
        }

        public static implicit operator CoreResult<T>(int code) => new CoreResult<T>(code);


    }

}
