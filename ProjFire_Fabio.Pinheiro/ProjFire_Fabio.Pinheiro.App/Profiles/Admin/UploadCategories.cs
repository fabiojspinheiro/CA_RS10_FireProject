class UploadCategories
{
    FileManager _f = new FileManager();   
    Categories _category;

    public void Upload()
    {
        try
        {
            _f.SaveCategories(CategoriesList());
            Menus.UploadedSuccessfully();

        } catch (Exception e) 
        {
            Console.WriteLine(e.Message);
        }
    }

    List <Categories> CategoriesList()
    {
        List<Categories> listCategories = new List<Categories>();

        string adminCategories = File.ReadAllText(_f.GetFilePath());    // Read Admin categories file
        string[] categories = adminCategories.Split("\r\n", StringSplitOptions.RemoveEmptyEntries); // Split by categorie
       
        for (int i = 2; i < categories.Length; i++)     // Ignore title and separator
        {
            if (categories[i][0] != '\t')   // \t means that is indented, so is a subcategory
            {
                _category = new Categories();   // Create new category
                _category.Name = categories[i];
                listCategories.Add(_category);   // Add category to list
            }
            else
            {
                SubCategories sub = new SubCategories();    // Create new subcategory
                sub.Name = categories[i].Remove(0, 1);      // Remove \t
                if (_category.SubCategories == null)
                    _category.SubCategories = new();

                _category.SubCategories.Add(sub);           // Add to list
            }
        }
        return listCategories;
    }
}
