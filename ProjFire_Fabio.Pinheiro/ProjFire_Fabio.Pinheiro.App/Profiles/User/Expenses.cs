class Expenses
{
    [Name("date")]      // Attributes - To make a match between csv header and class properties
    public DateOnly Date {  get; set; }

    [Name("category")]
    public string Category { get; set; }

    [Name("subcategory")]
    public string SubCategory { get; set; }

    [Name("beneficiary")]
    public string Beneficiary { get; set;}

    [Name("description")]
    public string Description { get; set;}

    [Name("amount")]
    public double Amout { get; set;}
}
