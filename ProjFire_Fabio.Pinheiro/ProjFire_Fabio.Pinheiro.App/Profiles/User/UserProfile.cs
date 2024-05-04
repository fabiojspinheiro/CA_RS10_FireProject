class UserProfile
{
    User _user;
    FileManager _f = new FileManager();

    public UserProfile(User user)
    {
        _user = user;
        bool showMenu = true;
        while (showMenu)
        {
            showMenu = Menu();
        }
    }

    public bool Menu()
    {
        string[] options = ["Edit User      ", "Upload Expenses", "Dashboard      ", "Exit           "];
        int choice = Menus.Menu(options);

        switch (choice)
        {
            case 0:
                EditUser();
                return true;
            case 1:
                UploadExpenses();
                return true;
            case 2:
                Dashboard();
                return true;
            case 3:
                Menus.LogOut();
                return false;
            default:
                return true;
        }
    }

    void EditUser()
    {
        Menus.Title();
        string oldName = _user.Name;    // Save for latter - resave user
        UserDashboard board = new UserDashboard(_user);
        board.EditDashboard();
        _f.ReSaveUser(_user, oldName);  // Resave user if filename doesn´t exists
    }

    void Dashboard()
    {
        Menus.Title();
        var userJson = File.ReadAllText(_f.UserFilePath(_user.Name));
        var userCopy = JsonSerializer.Deserialize<User>(userJson);      //Make a copy of user
        UserDashboard board = new UserDashboard(userCopy);
        board.ConsultDashboard();       
    }

    void UploadExpenses()
    {
        string oldName = _user.Name;
        UploadExpenses uploadExpenses = new UploadExpenses(_user);

        if (uploadExpenses.Upload())     // if success save
            _f.ReSaveUser(_user, oldName);
    }
}
