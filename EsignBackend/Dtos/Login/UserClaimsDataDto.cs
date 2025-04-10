namespace EsignBackend.Dtos.Login
{
    public class UserClaimsDataDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string UserDep { get; set; }
        public string UserFirstName { get; internal set; }
    }
}
