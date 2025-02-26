namespace SchoolProject.Data.Helpers
{
    // Don't forget we put this file here to make core and service see it
    public class JwtAuthResult
    {
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }

    }
    public class RefreshToken
    {
        public string UserName { get; set; }
        public string TokenString { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
