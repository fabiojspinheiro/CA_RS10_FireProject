using System.Text.Json;

public class FileAuthenticator : IAuthenticator
{

    private string _filePath;

    public FileAuthenticator(string filePath)
    {
        _filePath = filePath;
    }
    public bool Authenticator(string username, string password)
    {
        var userJson = File.ReadAllText(_filePath);
        Person user = JsonSerializer.Deserialize<Person>(userJson);
        return (username.Equals(user.Name, StringComparison.OrdinalIgnoreCase) && password == user.Password) ? true : false;
    }

    public bool ValidatePassword(string password)
    {
        if (password.Any(char.IsLetter) && password.Any(char.IsNumber) && password.Length <= 8)
        {
            return true;
        }
        else
        {
            Menus.PasswordConditions();
            return false;
        }
    }



    public void Exit() => Menus.LogOut();
}
