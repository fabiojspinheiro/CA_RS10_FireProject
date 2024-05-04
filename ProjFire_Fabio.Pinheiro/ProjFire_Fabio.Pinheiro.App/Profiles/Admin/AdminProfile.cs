class AdminProfile
{
    Admin _admin;
    FileManager _f = new FileManager();

    public AdminProfile(Admin admin)
    {
        _admin = admin;
        bool showMenu = true;
        while (showMenu)
        {
            showMenu = Menu();
        }
    }

    public bool Menu()
    {
        string[] options = ["Change Directory ", "Upload categories", "All Users Stats  ", "Exit             "];
        int choice = Menus.Menu(options);

        switch (choice)
        {
            case 0:
                ChangeDirectory();
                return true;
            case 1:
                UploadCategories upload = new UploadCategories();
                upload.Upload();
                return true;
            case 2:
                Statistics stats = new Statistics();
                stats.PrintStatistics();
                return true;
            case 3:
                Menus.LogOut();
                return false;
            default:
                return true;
        }
    }

    void ChangeDirectory()
    {
        string configFilePath = _f.ConfigFilePath();
        Menus.Title();

        if (_f.IsDirEmpty(File.ReadAllText(configFilePath)))
        {
            Menus.ChangeDir();
            string content = Console.ReadLine();

            try
            {
                var dir = new DirectoryInfo(content);
                if (!dir.Exists)
                    dir.Create();
                File.WriteAllText(configFilePath, content);
            } catch { }
        }
        else
        {
            Menus.ErrorChangeDir();
        }
    }
}
