class FileManager
{
    public string configDirectory = @"C:\fire";
    public string configFileName = @"config.fire";
    string categoriesFileName = @"categories.categories";

    public void CreateDirectory(string directory)
    {
        var dir = new DirectoryInfo(directory);
        if (!dir.Exists)
            dir.Create();
    }

    public bool CheckIfDirExists()
    {
        return string.IsNullOrWhiteSpace(ConfigFileContent());
    }

    public bool IsDirEmpty(string path)
    {
        if (path == string.Empty)
            return true;
        else
            return !Directory.EnumerateFileSystemEntries(path).Any();
    }

    public void CreateFile(string filePath, string content)
    {
        var file = new FileInfo(filePath); ;
        if (!file.Exists)
            File.WriteAllText(filePath, content);
    }

    public string ConfigFileContent()
    {
        return File.ReadAllText(ConfigFilePath());
    }

    public string ConfigFilePath()
    {
        return Path.Combine(configDirectory, configFileName);
    }

    public string UserFilePath(string name)
    {
        string fileName = $@"{name}.json".Replace(" ", "").ToLower();
        return Path.Combine(ConfigFileContent(), fileName);
    }

    public void SaveAdmin(Admin admin, string adminFilePath)
    {
        string jsonAdmin = JsonSerializer.Serialize(admin);
        File.WriteAllText(adminFilePath, jsonAdmin);
    }

    public void SaveNewUser(User user, string userFilePath)
    {
        string jsonUser = JsonSerializer.Serialize(user);
        File.WriteAllText(userFilePath, jsonUser);
    }

    public void ReSaveUser(User user, string oldName = "")
    {
        string oldFilePath = Path.Combine(ConfigFileContent(), $@"{oldName}.json".Replace(" ", "").ToLower());

        File.Move(oldFilePath, UserFilePath(user.Name));

        string jsonUser = JsonSerializer.Serialize(user);
        File.WriteAllText(UserFilePath(user.Name), jsonUser);
    }

    public string GetCsvPath()
    {
        Menus.Title();
        Console.WriteLine(@"Enter the directory of CSV File.");
        Console.WriteLine(@"[ex: ""C:\fire\expenses.csv""]");
        Console.WriteLine();
        string csvPath = Console.ReadLine();

        var file = new FileInfo(csvPath); ;
        if (!file.Exists)
        {
            Menus.Title();
            Console.WriteLine("File not found!!! Try again...");
            Menus.Continue();
            return null;
        }
        else
            return csvPath;
    }

    public string GetFilePath()
    {
        Menus.Title();
        Console.WriteLine(@"Enter the directory of the File.");
        Console.WriteLine(@"[ex: ""C:\fire\expenses.[file format]""]");
        Console.WriteLine();
        string filePath = Console.ReadLine();

        var file = new FileInfo(filePath); ;
        if (!file.Exists)
        {
            Menus.Title();
            Console.WriteLine("File not found!!! Try again...");
            Menus.Continue();
            return null;
        }
        else
            return filePath;
    }

    string CategoriesFilePath()
    {
        return Path.Combine(configDirectory, categoriesFileName);
    }

    public void SaveCategories(List<Categories> categories)
    {
        string jsonCategories = JsonSerializer.Serialize(categories);
        File.WriteAllText(CategoriesFilePath(), jsonCategories);
    }

    public List<Categories> GetCategories()
    {
        try
        {
            var jsonCategories = File.ReadAllText(CategoriesFilePath());
            return JsonSerializer.Deserialize<List<Categories>>(jsonCategories);
        } catch 
        { 
            return null; 
        }
    }

    public List <User> ReadUserFiles()
    {
        List<User> users = new List<User>();
        try
        {
            DirectoryInfo _directoryInfo = new DirectoryInfo(ConfigFileContent());
            FileInfo[] allFiles = _directoryInfo.GetFiles();
            foreach (FileInfo file in allFiles)
            {
                try
                {
                    string pathFile = Path.Combine(ConfigFileContent(), file.Name);
                    var userJson = File.ReadAllText(pathFile);

                    users.Add(JsonSerializer.Deserialize<User>(userJson));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        } catch{ }

        return users;
    }
}