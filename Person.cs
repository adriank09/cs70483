using System;

class Person
{
    // Properties
    public string Name { get; set; }
    public string City { get; set; }
    public int Age { get; set; }

    // Delegates
    // Delegate for the name changed event
    public Action<Person> NameChanged { get; set; }

    public void SetName(string name)
    {
        Name = name;

        // Raise the NameChanged event if it is subscribed.
        if(NameChanged != null)
        {
            NameChanged(this);
        }
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

    private void OnNameChanged(Person p)
    {
        System.Console.WriteLine("Name for this person was set to: {0}.", p.Name);
    }
}