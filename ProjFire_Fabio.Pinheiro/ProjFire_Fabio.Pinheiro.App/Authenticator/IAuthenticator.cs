public interface IAuthenticator
{
    bool Authenticator(string username, string password);

    void Exit();
}
