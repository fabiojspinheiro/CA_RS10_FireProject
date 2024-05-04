class UploadExpenses
{
    User _user;
    FileManager _f = new FileManager();

    List<Categories> _categories;
    IEnumerable<Expenses> _records;         //CSV Reader return a IEnumerable

    public UploadExpenses(User user)
    {
        _user = user;
        _categories = _f.GetCategories();
    }

    public bool Upload()
    {
        bool sucess = false;
        ReadCsv();

        if (_user.UserExpenses == null)               // If user doesn´t have expenses create a new list
            _user.UserExpenses = new();

        if (_categories != null)        // Check if admin uploaded categories
        {
            if (CheckCategories())
            {
                Menus.UploadedSuccessfully();
                sucess = true;
            }
            else if(_records == null)
            {
                Menus.ErrorEmptyDir();
                sucess = false;
            }
        }
        else
        {
            Menus.ErrorEmptyCategories();
            sucess = false;
        }

        return sucess;
    }


    bool CheckCategories()
    {
        int line = 1;
        bool success = false;
        bool checkCatg = false;
        bool checkSub = false;

        if (_records != null)
        {
            foreach (var record in _records)
            {
                // Check mandatory fields
                if (IsEmpty(record.Date.ToString()) || IsEmpty(record.Category) ||
                    IsEmpty(record.SubCategory) || IsEmpty(record.Amout.ToString()))
                {
                    Menus.ErrorUploadExpenses(line);
                    success = false;
                    break;
                }
                else
                {
                    foreach (var catg in _categories)
                    {
                        if (catg.Name.ToLower() == record.Category.ToLower())   // check if category exists
                        {
                            foreach (var subCatg in catg.SubCategories)
                            {
                                if (subCatg.Name.ToLower() == record.SubCategory.ToLower()) // check if subcategory exists
                                {
                                    checkSub = true;
                                    break;
                                }
                                else
                                    checkSub = false;
                            }
                            checkCatg = true;
                            break;
                        }
                        else
                            checkCatg = false;
                    }

                    // If doesn´t exists are "Others"
                    if (!checkCatg)
                        record.Category = "Others";
                    if (!checkSub)
                        record.SubCategory = "Others";

                    line++;     // count file lines
                    _user.UserExpenses.Add(record);      // add expense         
                    success = true;
                }
            }
        }
        else
            success = false;

        return success;
    }

    bool IsEmpty(string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    public void ReadCsv()
    {
        try
        {
            var reader = new StreamReader(_f.GetCsvPath());
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
            var csv = new CsvReader(reader, config);
            _records = csv.GetRecords<Expenses>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

}
