using System;

public class PersonEventArgs : EventArgs
{
    public string Country {get;set;}

    public PersonEventArgs(string country)
    {
        Country = country;
    }
}