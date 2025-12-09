using System;
using System.Collections.Generic;

namespace Q1_ContactBook;

public class ContactBook
{
    private readonly List<Contact> _contacts = new List<Contact>();

    public void SeedContacts()
    {
        for (int i = 1; i <= 2; i++)
        {
            _contacts.Add(new Contact
            {
                FirstName = "First" + i,
                LastName = "Last" + i,
                Company = "Company" + i,
                MobileNumber = "12345678" + (i % 10),
                Email = $"person{i}@example.com",
                Birthdate = new DateTime(1990, 1, 1).AddDays(i)
            });
        }
    }

    public void AddContact(Contact contact)
    {
        _contacts.Add(contact);
    }

    public void ShowAllContacts()
    {
        if (_contacts.Count == 0)
        {
            Console.WriteLine("No contacts available.");
            return;
        }

        for (int i = 0; i < _contacts.Count; i++)
        {
            Contact c = _contacts[i];
            Console.WriteLine($"{i + 1}. {c.FirstName} {c.LastName}");
        }
    }

    public Contact? GetContact(int index)
    {
        if (index < 0 || index >= _contacts.Count)
        {
            return null;
        }
        return _contacts[index];
    }

    // Method overloading example: find by index or by full name
    public Contact? FindContact(string firstName, string lastName)
    {
        foreach (var c in _contacts)
        {
            if (string.Equals(c.FirstName, firstName, StringComparison.OrdinalIgnoreCase)
                && string.Equals(c.LastName, lastName, StringComparison.OrdinalIgnoreCase))
            {
                return c;
            }
        }

        return null;
    }

    public void DeleteContact(int index)
    {
        if (index < 0 || index >= _contacts.Count)
        {
            Console.WriteLine("Contact not found.");
            return;
        }
        _contacts.RemoveAt(index);
    }

    public int Count()
    {
        return _contacts.Count;
    }
}
