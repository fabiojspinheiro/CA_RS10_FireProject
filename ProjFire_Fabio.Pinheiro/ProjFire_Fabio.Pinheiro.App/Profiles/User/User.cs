class User : Person, IProfile
{
    public double Assets { get; set; }
    public double Expenses { get; set; }
    public double Yield { get; set; }
    public double Inflation { get; set; }
    public int TTL { get; set; }

    public List <Expenses> UserExpenses { get; set; }  

    public User()
    {
        TTL = 100;
    }
}
