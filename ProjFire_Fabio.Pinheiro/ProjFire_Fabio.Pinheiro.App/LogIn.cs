class LogIn
{
    FileManager _f = new FileManager();
    IProfile _user;

    public IProfile SignIn()
    {
        _user = null;   // Reset user logged

        string name = Menus.Name();

        if (UserExists(name))
        {
            string password = Menus.Password();

            if (name == "admin")
            {
                FileAuthenticator fAuth = new FileAuthenticator(Path.Combine(_f.configDirectory, @"admin.json"));
                if (fAuth.Authenticator(name, password))
                {
                    var userJson = File.ReadAllText(Path.Combine(_f.configDirectory, @"admin.json"));
                    _user = JsonSerializer.Deserialize<Admin>(userJson);
                }
                else
                    Menus.InvalidPassword();
            }
            else
            {
                FileAuthenticator fAuth = new FileAuthenticator(_f.UserFilePath(name));
                if (fAuth.Authenticator(name, password) && name != "admin")
                {
                    var userJson = File.ReadAllText(_f.UserFilePath(name));
                    _user = JsonSerializer.Deserialize<User>(userJson);
                }
                else
                    Menus.InvalidPassword();
            }
        }
        return _user;
    }

    public IProfile SignUp()
    {
        if (!_f.CheckIfDirExists())
        {
            // Create new user
            User user = new User();
            user.Name = Menus.Name();

            FileAuthenticator fAuth = new FileAuthenticator(_f.UserFilePath(user.Name));

            var file = new FileInfo(_f.UserFilePath(user.Name)); ;
            if (!file.Exists)
            {
                do
                {
                    user.Password = Menus.Password();

                } while (!fAuth.ValidatePassword(user.Password));

                _f.SaveNewUser(user, _f.UserFilePath(user.Name));
                return user;
            }
            else
            {
                Menus.AlreadyExists(user.Name);
                return null;
            }
        }
        else
        {
            Menus.DirectoryNotSet();
            return null;
        }
    }

    bool UserExists(string name)
    {
        var userFile = new FileInfo(_f.UserFilePath(name));

        if (!userFile.Exists) // Change dir to admin
        {
            userFile = new FileInfo(Path.Combine(_f.configDirectory, $@"{name}.json"));

            if (!userFile.Exists)
            {
                Menus.DoesNotExists();
                return false;
            }
            else
                return true;
        }
        else
            return true;
    }


    public bool Menu()
    {
        string[] options = ["Sign in", "Sign up", "Exit   "];
        int choice = Menus.Menu(options);

        switch (choice)
        {
            case 0:
                _user = SignIn();
                if (_user != null)
                {
                    if (_user is User)
                        new UserProfile((User)_user);
                    else if (_user is Admin)
                        new AdminProfile((Admin)_user);
                }
                return true;
            case 1:
                _user = SignUp();
                if (_user != null)
                {
                    if (_user is User)
                        new UserProfile((User)_user);
                }
                return true;
            case 2:
                return false;
            default:
                return true;
        }
    }
}
