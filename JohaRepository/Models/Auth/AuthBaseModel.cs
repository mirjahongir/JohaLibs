using JohaRepository.Attributes.Auth;

using System;

namespace JohaRepository.Models.Auth
{
    public class AuthBaseModel<T>
        where T : class
    {
        [UserInfo]
        public virtual T UserInfo { get; set; } = (T)Activator.CreateInstance(typeof(T), new object[] { });
    }
}