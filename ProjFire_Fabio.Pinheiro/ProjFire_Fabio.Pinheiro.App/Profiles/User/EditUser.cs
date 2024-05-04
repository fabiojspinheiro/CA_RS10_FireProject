class EditUser
{
    User _user;
    FileManager _f = new FileManager();

    string[] commands =
                ["-help", "-name", "-pwd", "-db", "-assets", "-expenses", "-yield", "-inflation", "-ttl"];

    public EditUser(User user)
    {
        _user = user;
    }

    public void Edit(string userCommands)
    {
        string[] updates;
        string[] userCommand = userCommands.Split(" -", StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < userCommand.Length; i++)
        {
            updates = userCommand[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if (updates[0] == commands[0])
                {
                    Console.WriteLine("\nfire set-user" +
                        "\r\n\t--name Nome Investidor" +
                        "\r\n\t--pwd password" +
                        "\r\n\t--db data de nascimento usando o formato dd-mm-yyyy ou yyyy-mm-dd" +
                        "\r\n\t--assets valor total património" +
                        "\r\n\t--expenses média mensal de despesas" +
                        "\r\n\t--yield taxa de retorno esperada [0, 1]" +
                        "\r\n\t--inflation taxa de inflação [0,1]" +
                        "\r\n\t--ttl longevidade prevista em anos" +
                        "\r\n\t[Enter to Exit]");

                    Menus.Continue();
                }
                if (updates[0] == commands[1])
                {
                    TryName(updates[1]);
                }
                if (updates[0] == commands[2])
                {
                    _user.Password = updates[1];
                }
                if (updates[0] == commands[3])
                {
                    _user.BirthDate = TryDateOnly(updates[1]);
                }
                if (updates[0] == commands[4])
                {
                    _user.Assets = TryDouble(updates[1]);
                }
                if (updates[0] == commands[5])
                {
                    _user.Expenses = TryDouble(updates[1]);
                }
                if (updates[0] == commands[6])
                {
                    _user.Yield = TryDouble(updates[1]);
                }
                if (updates[0] == commands[7])
                {
                    _user.Inflation = TryDouble(updates[1]);
                }
                if (updates[0] == commands[8])
                {
                    _user.TTL = TryInt(updates[1]);
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    void TryName(string input)
    {
        var file = new FileInfo(_f.UserFilePath(input)); ;
        if (!file.Exists)
        {
            _user.Name = input;
        }
        else
        {
            Menus.AlreadyExists(_user.Name);
        }
    }
    double TryDouble(string input)
    {
        bool success = double.TryParse(input, out double number);
        return number;
    }
    int TryInt(string input)
    {
        bool success = int.TryParse(input, out int number);
        return number;
    }
    DateOnly TryDateOnly(string input)
    {
        bool success = DateOnly.TryParse(input, out DateOnly dateOnly);
        return dateOnly;
    }
}
