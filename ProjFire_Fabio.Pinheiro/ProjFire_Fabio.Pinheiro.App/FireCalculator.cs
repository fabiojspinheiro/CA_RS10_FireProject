class FireCalculator
{
    User _user;
    int _years;
 
    public FireCalculator(User user)
    {
        _user = user;
        _years = _user.TTL - (DateTime.Today.Year - _user.BirthDate.Year);
    }

    public double RetireNow()
    {
        double income = _user.Assets * (_user.Yield - _user.Inflation);     // Income value
        double monthlyIncome = income / 12;

        if (monthlyIncome > _user.Expenses)
            return monthlyIncome;               
        else
        {
            // Check if user have enough money
            for (int i = 0; i < _years; i++)
            {
                if (_user.Assets > _user.Expenses * 12)
                {
                    _user.Assets -= Math.Abs(_user.Expenses - monthlyIncome) * 12;  // Deducts from assets each year

                    income = _user.Assets * (_user.Yield - _user.Inflation);     //Recalculate income next year
                    monthlyIncome = income / 12;
                }
                else
                {
                    monthlyIncome = 0;
                    break;
                }
            }

            return monthlyIncome;
        }
    }

    public double Ratio()
    {
        return RetireNow() / _user.Expenses;
    }

    public string RetirePossability()
    {
        if (_user.Expenses <= RetireNow())
            return "Yes";
        else
            return "No";
    }

}