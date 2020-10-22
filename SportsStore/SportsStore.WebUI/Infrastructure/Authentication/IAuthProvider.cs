namespace SportsStore.WebUI.Infrastructure.Authentication
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
}
