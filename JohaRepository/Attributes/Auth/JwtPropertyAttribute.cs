namespace JohaRepository.Attributes.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtPropertyAttribute : System.Attribute
    {
        private JwtPropertyAttribute()
        {

        }
        public JwtPropertyAttribute(string jwtKey,
            bool isRequired = false

            )
        {
            JwtKey = jwtKey;
            IsRequired = isRequired;

        }
        public string JwtKey { get; set; }
        public bool IsRequired { get; set; }
        //public ClaimsIdentity ClaimIdentity { get; set; }
    }
}

