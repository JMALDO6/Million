namespace Million.Domain.Settings
{
    /// <summary>
    /// JWT configuration settings.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Issuer of the JWT token.
        /// </summary>
        public required string Issuer { get; set; }

        /// <summary>
        /// Audience for the JWT token.
        /// </summary>
        public required string Audience { get; set; }

        /// <summary>
        /// Secret key used for signing the JWT token.
        /// </summary>
        public required string SecretKey { get; set; }
    }
}
