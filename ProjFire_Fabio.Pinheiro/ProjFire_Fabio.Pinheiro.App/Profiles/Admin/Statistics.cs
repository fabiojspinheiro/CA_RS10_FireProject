class Statistics
{
    List<User> _users;
    FileManager _f;

    public Statistics()
    {
        _users = new List<User>();
        _f = new FileManager();
        
        _users = _f.ReadUserFiles();
    }

    public void PrintStatistics()
    {
        Menus.Title();
        string[] columns = ["User", "Assets", "Retire now", "Ratio"];
        var table = new ConsoleTable(columns);
        table.Options.EnableCount = false;

        foreach (User user in _users)
        {
            FireCalculator calc = new FireCalculator(user);
            table.AddRow(user.Name, user.Assets, calc.RetireNow().ToString("0.##"), calc.Ratio().ToString("0.##"));
        }
        table.Write();
        Menus.Continue();
    }
}
