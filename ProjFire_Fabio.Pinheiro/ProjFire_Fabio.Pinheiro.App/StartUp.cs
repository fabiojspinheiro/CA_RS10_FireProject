class StartUp
{
    FileManager _fileManager = new FileManager();
    string _configDirectory;
    string _configFileName;

    public StartUp()
    {
        _configDirectory = _fileManager.configDirectory; // Base Directory
        _configFileName = _fileManager.configFileName;  // File with the admin directory

        // Create new directory if doesn't exists
        _fileManager.CreateDirectory(_configDirectory);

        // Create new file to save the admin directory if doesn't exists
        string _configFilePath = Path.Combine(_configDirectory, _configFileName);
        _fileManager.CreateFile(_configFilePath, "");

        CheckAdmin();
    }

    void CheckAdmin()
    {
        // Admin file should exists always in base directory
        string adminFilePath = Path.Combine(_configDirectory, @"admin.json");   
        FileInfo adminFile = new FileInfo(adminFilePath);
        FileAuthenticator auth = new FileAuthenticator(adminFilePath);

        if (!adminFile.Exists)
        {
            // Create New Admin and open the admin profile
            Admin admin = new Admin();
            admin.Name = "admin";
            do
            {
                Menus.NewAdmin();
                admin.Password = Menus.Password();

            } while (!auth.ValidatePassword(admin.Password));

            _fileManager.SaveAdmin(admin, adminFilePath);
            AdminProfile adminProfile = new AdminProfile(admin);
            adminProfile.Menu();
        }
    }
}
