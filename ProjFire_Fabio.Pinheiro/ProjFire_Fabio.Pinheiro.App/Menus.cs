class Menus
{
    static int _size = 50;
    static int _index = 0;
    static string[] _options;

    public static void Title()
    {
        Console.Clear();
        Console.WriteLine(new string('*', _size));
        Console.WriteLine("\n");
        Console.WriteLine(PadCenter("PROJECT FIRE"));
        Console.WriteLine("\n");
        Console.WriteLine(new string('*', _size));
        Console.WriteLine();
    }

    public static int Menu(string[] options)
    {
        _options = options;
        _index = 0;

        ConsoleKey keyPressed;
        do
        {
            DisplayOptions();
            ConsoleKeyInfo key = Console.ReadKey(true);
            keyPressed = key.Key;

            if (keyPressed == ConsoleKey.UpArrow)
            {
                _index--;
                if (_index < 0)
                    _index = _options.Length - 1;
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                _index++;
                if (_index == _options.Length)
                    _index = 0;
            }
        } while (keyPressed != ConsoleKey.Enter);

        return _index;
    }

    static void DisplayOptions()
    {
        Title();
        for (int i = 0; i < _options.Length; i++)
        {
            if (i == _index)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine(PadCenter($" > {_options[i]} "));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(PadCenter($"   {_options[i]} "));
            }
        }
        Console.ResetColor();
    }

    public static void Continue()
    {
        Console.Write("\nPress <Enter> to continue...");
        Console.ReadLine();
    }

    public static void NewAdmin()
    {
        Title();
        Console.WriteLine(PadCenter("Welcome Admin!!!"));
        Continue();
    }

    public static void AlreadyExists(string name)
    {
        Console.WriteLine($"\n{name} already exists. Please try again!");
        Continue();
    }

    public static void DoesNotExists()
    {
        Console.WriteLine("User doesn´t exists!!!");
        Continue();
    }

    public static void DirectoryNotSet()
    {
        Title();
        Console.WriteLine("Directory not set... Please contact your Admin!!!");
        Continue();
    }

    public static void ChangeDir()
    {
        Console.WriteLine(@"Choose the directory to save files.");
        Console.WriteLine(@"[ex: ""C:\fire""]");
        Console.WriteLine();
    }

    public static void ErrorChangeDir()
    {
        Console.WriteLine("The actual directory is not empty!!!");
        Continue();
    }

    public static void ErrorEmptyDir()
    {
        Console.WriteLine("Please enter a directory!!!");
        Continue();
    }

    public static void ErrorUploadExpenses(int line)
    {
        Title();
        Console.WriteLine("Data, Category, SubCategory and Amount are mandatory!!!");
        Console.WriteLine($"\nCheck in line {line}");
        Continue();
    }

    public static void ErrorEmptyCategories()
    {
        Title();
        Console.WriteLine("Categories not set... \nPlease contact your Admin!!!");
        Continue();
    }

    public static void InvalidPassword()
    {
        Console.WriteLine("\n\nInvalid Password!!!");
        Continue();
    }

    public static void PasswordConditions()
    {
        Console.WriteLine("The password must have letters, numbers and less the 8 characteres...");
        Continue();
    }

    public static void UploadedSuccessfully()
    {
        Title();
        Console.WriteLine("Upload completed successfully!!!");
        Continue();
    }

    public static void LogOut()
    {
        Console.Write("\nLogging out");
        for (int i = 0; i < 3; i++)
        {
            Console.Write(".");
            Thread.Sleep(600);
        }
    }

    public static string PadCenter(string str)
    {
        if (str == null)
            str = " ";

        int spaces = _size - str.Length;
        int padLeft = spaces / 2 + str.Length;
        return str.PadLeft(padLeft).PadRight(_size);
    }

    public static string Name()
    {
        Title();
        Console.Write("Name: ");
        return Console.ReadLine().Replace(" ", "");
    }

    public static string Password()
    {
        ConsoleKeyInfo key;
        string password = "";

        Title();
        Console.Write("Password: ");
        do
        {
            key = Console.ReadKey(true);
            if (key.Key != ConsoleKey.Backspace)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
            else if (password.Length > 0)
            {
                Console.Write("\b \b"); //Define new limit... Erase last char in console
                password = password.Remove(password.Length - 1, 1);
            }
        } while (key.Key != ConsoleKey.Enter);

        return password.Remove(password.Length - 1, 1);   //Remove enter
    }
}
