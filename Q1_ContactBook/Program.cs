using Q1_ContactBook;

ContactBook contactBook = new ContactBook();
contactBook.SeedContacts();

while (true)
{
    Console.WriteLine("Main Menu");
    Console.WriteLine("1: Add Contact");
    Console.WriteLine("2: Show All Contacts");
    Console.WriteLine("3: Show Contact Details");
    Console.WriteLine("4: Update Contact");
    Console.WriteLine("5: Delete Contact");
    Console.WriteLine("0: Exit");
    Console.Write("Choose an option: ");

    string? input = Console.ReadLine();

    if (input == "0")
    {
        break;
    }

    switch (input)
    {
        case "1":
            AddContact(contactBook);
            break;
        case "2":
            ShowAllContacts(contactBook);
            break;
        case "3":
            ShowContactDetails(contactBook);
            break;
        case "4":
            UpdateContact(contactBook);
            break;
        case "5":
            DeleteContact(contactBook);
            break;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }

    Console.WriteLine();
}

static void AddContact(ContactBook contactBook)
{
    Contact c = new Contact();

    Console.Write("First Name: ");
    c.FirstName = Console.ReadLine() ?? string.Empty;

    Console.Write("Last Name: ");
    c.LastName = Console.ReadLine() ?? string.Empty;

    Console.Write("Company: ");
    c.Company = Console.ReadLine() ?? string.Empty;

    c.MobileNumber = ReadValidMobileNumber();

    Console.Write("Email: ");
    c.Email = Console.ReadLine() ?? string.Empty;

    Console.Write("Birthdate (yyyy-mm-dd): ");
    if (!DateTime.TryParse(Console.ReadLine(), out DateTime birthdate))
    {
        birthdate = DateTime.MinValue;
    }
    c.Birthdate = birthdate;

    contactBook.AddContact(c);
    Console.WriteLine("Contact added.");
}

static void ShowAllContacts(ContactBook contactBook)
{
    contactBook.ShowAllContacts();
}

static void ShowContactDetails(ContactBook contactBook)
{
    int index = ReadContactIndex(contactBook);
    if (index == -1)
    {
        return;
    }

    Contact? c = contactBook.GetContact(index);
    if (c == null)
    {
        Console.WriteLine("Contact not found.");
        return;
    }
    Console.WriteLine($"Name: {c.FirstName} {c.LastName}");
    Console.WriteLine($"Company: {c.Company}");
    Console.WriteLine($"Mobile: {c.MobileNumber}");
    Console.WriteLine($"Email: {c.Email}");
    Console.WriteLine($"Birthdate: {c.Birthdate:yyyy-MM-dd}");
}

static void UpdateContact(ContactBook contactBook)
{
    int index = ReadContactIndex(contactBook);
    if (index == -1)
    {
        return;
    }

    Contact? c = contactBook.GetContact(index);
    if (c == null)
    {
        Console.WriteLine("Contact not found.");
        return;
    }

    Console.Write($"First Name ({c.FirstName}): ");
    string? first = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(first)) c.FirstName = first;

    Console.Write($"Last Name ({c.LastName}): ");
    string? last = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(last)) c.LastName = last;

    Console.Write($"Company ({c.Company}): ");
    string? company = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(company)) c.Company = company;

    Console.Write($"Mobile ({c.MobileNumber}): ");
    string? mobileInput = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(mobileInput))
    {
        string? valid = ValidateMobile(mobileInput);
        if (valid != null)
        {
            c.MobileNumber = mobileInput;
        }
        else
        {
            Console.WriteLine("Invalid mobile number. Keeping old value.");
        }
    }

    Console.Write($"Email ({c.Email}): ");
    string? email = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(email)) c.Email = email;

    Console.Write($"Birthdate ({c.Birthdate:yyyy-MM-dd}): ");
    string? birthInput = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(birthInput) && DateTime.TryParse(birthInput, out DateTime newBirth))
    {
        c.Birthdate = newBirth;
    }

    Console.WriteLine("Contact updated.");
}

static void DeleteContact(ContactBook contactBook)
{
    int index = ReadContactIndex(contactBook);
    if (index == -1)
    {
        return;
    }

    contactBook.DeleteContact(index);
    Console.WriteLine("Contact deleted.");
}

static int ReadContactIndex(ContactBook contactBook)
{
    if (contactBook.Count() == 0)
    {
        Console.WriteLine("No contacts available.");
        return -1;
    }

    ShowAllContacts(contactBook);
    Console.Write("Select contact number: ");
    if (!int.TryParse(Console.ReadLine(), out int number))
    {
        Console.WriteLine("Invalid number.");
        return -1;
    }

    if (number < 1 || number > contactBook.Count())
    {
        Console.WriteLine("Contact not found.");
        return -1;
    }

    return number - 1;
}

static string ReadValidMobileNumber()
{
    while (true)
    {
        Console.Write("Mobile Number (9 digits, non-zero): ");
        string? input = Console.ReadLine();
        string? valid = ValidateMobile(input);
        if (valid != null)
        {
            return valid;
        }

        Console.WriteLine("Invalid mobile number. Please try again.");
    }
}

static string? ValidateMobile(string? input)
{
    if (string.IsNullOrWhiteSpace(input)) return null;
    if (input.Length != 9) return null;
    if (!long.TryParse(input, out long number)) return null;
    if (number <= 0) return null;
    if (input[0] == '0') return null;
    return input;
}

static void SeedContacts(List<Contact> contacts)
{
    for (int i = 1; i <= 20; i++)
    {
        contacts.Add(new Contact
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
