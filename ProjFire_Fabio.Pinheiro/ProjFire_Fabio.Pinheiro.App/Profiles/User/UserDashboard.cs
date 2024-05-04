class UserDashboard
{
    User _user;
    FireCalculator _calc;

    public UserDashboard(User user)
    {
        _user = user;
        _calc = new FireCalculator(user);
    }

    public void ConsultDashboard()      // Display user wallet and fire calculations
    {
        do 
        {
            Menus.Title();

            WalletTable();
            CalculatedTable();

        } while(UserCommands() != " ");
    }


    public void EditDashboard()         // Display User personal info and wallet
    {
        do
        {
            Menus.Title();

            UserTable();
            WalletTable();

        } while (UserCommands() != " ");
    }

    string UserCommands()               // Edit dashboard information
    {
        Console.Write("fire set-user ");
        string userCommands = " " + Console.ReadLine().ToLower().Trim().Replace(".", ",");
        EditUser newEdit = new EditUser(_user);
        newEdit.Edit(userCommands);

        return userCommands;
    }

    void WalletTable()
    {
        string[] columns = ["Assets", "Expenses", "Yield", "Inflation", "TTL"];
        var table = new ConsoleTable(columns);
        table.AddRow(_user.Assets.ToString("0.##"), _user.Expenses, _user.Yield, _user.Inflation, _user.TTL);
        table.Options.EnableCount = false;
        table.Write();
    }

    void CalculatedTable()
    {
        string[] columns = ["Retire now", "Ratio", "Is Possible?"];
        var table = new ConsoleTable(columns);
        table.AddRow(_calc.RetireNow().ToString("0.##"), _calc.Ratio().ToString("0.##"), _calc.RetirePossability());
        table.Options.EnableCount = false;
        table.Write();
    }

    void UserTable()
    {
        string[] columns = ["Name", "Password", "Birth Date"];
        var table = new ConsoleTable(columns);
        table.AddRow(_user.Name, _user.Password, _user.BirthDate);
        table.Options.EnableCount = false;
        table.Write();
        Console.WriteLine();
    }
}

