using System;

class Person
{
    // Properties
    public string Name { get; set; }
    public string City { get; set; }
    public int Age { get; set; }

    // Delegates
    // Delegate for the name changed event
    public event EventHandler NameChanged = delegate {};
    public event EventHandler AgeChanged = delegate {};
    public event EventHandler CountryAssigned = delegate {};
    public void SetName(string name)
    {
        Name = name;

        // Raise the NameChanged event.
        NameChanged(this, EventArgs.Empty);
    }

    public void SetAge(int age)
    {
        Age = age;
    }

    // Constructor
    public Person(string name, string city, int age)
    {
        Name = name;
        Age = age;
        City = city;

        // Subscribe
        NameChanged += OnNameChanged;
    }

    private void OnNameChanged(object sender, EventArgs e)
    {
        if(sender is Person)
        {
            var p = sender as Person;
            System.Console.WriteLine("Name for this person was set to: {0}.", p.Name);
        }
    }

    public void AssignCountry(string country) => 
        CountryAssigned(this, new PersonEventArgs(country));

    

}