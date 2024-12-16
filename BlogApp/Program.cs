using BlogApp;
using System.Linq;
using System.IO;
using Microsoft.Win32;

//list Collection
List<Person> people = new List<Person>();

//string firstName = "Blessing";
//string lastName = "Uloyok";
//string email = "test@email.com";
//string passWord = "pass123";
//string confirmPassword;
//string blogDate = DateTime.Now.ToString("dd/MM/yyyy");
//string blogTitle = "Title";
//string blogDescription = "some description";

string firstName = "";
string lastName = "";
string email = "";
string passWord = "";
string confirmPassword;
string blogDate = DateTime.Now.ToString("dd/MM/yyyy");
string blogTitle = "";
string blogDescription= "";

//BlogEntry newEntry = new BlogEntry(firstName, lastName, email, passWord, blogDate, blogTitle, blogDescription);
//people.Add(newEntry);

//var matches = people.Where(p => p.GetEmail() == email && p.GetPassWord() == passWord).Take(1);

//FirstOrDefault() in place of First() to avoid throwing error for null value
//Person loggedIn = matches.FirstOrDefault();

while (true)
{
    //BlogEntry newEntry = new BlogEntry(firstName, lastName, email, passWord, blogDate, blogTitle, blogDescription);
    //Options section
    Console.Write("[R]egister,[L]ogin, [C]reate, [V]iew,or [Q]uit? ");
    string choice = Console.ReadLine().ToUpper();

    //Register Section
    if (choice == "R")
    {
        registerToBlog();
    }
    //Login Section
    else if (choice == "L")
    {
        logIntoBlog();
    }
    //Create entry Section
    else if (choice == "C")
    {
        createEntry();
    }
    //View entry Section
    else if (choice == "V")
    {
        viewEntry();
    }
    //quit or logout 
    else if (choice == "Q")
    {
        break;
    }
}

void registerToBlog()
{
    //Register Section
    while (true)
    {
        Console.WriteLine("Register Section");
        Console.Write("First Name: ");
        firstName = Convert.ToString(Console.ReadLine());
        Console.Write("Last Name: ");
        lastName = Convert.ToString(Console.ReadLine());
        Console.Write("Email: ");
        email = Convert.ToString(Console.ReadLine());
        Console.Write("Password: ");
        passWord = Convert.ToString(Console.ReadLine());
        Console.Write("Confirm Password: ");
        confirmPassword = Convert.ToString(Console.ReadLine());

        BlogEntry register = new BlogEntry(firstName, lastName, email, passWord, blogDate, blogTitle, blogDescription);
        var matches = people.Where(p => p.GetEmail() == email && p.GetPassWord() == passWord).Take(1);
        //Algorithms
        //var match = people.Where(p => p.GetEmail() == email && p.GetPassWord() == passWord).Take(1);

        if (firstName == "" || lastName == "" || passWord == "" || confirmPassword == "")
        {
            Console.WriteLine("Cannot have empty field!!");
        }
        else if (firstName.Contains(" ") || lastName.Contains(" ") || email.Contains(" ") || passWord.Contains(" ") || confirmPassword.Contains(" "))
        {
            Console.WriteLine("Each field MUST BE one word, no spaces");
        }
        else if (passWord != confirmPassword)
        {
            Console.WriteLine("Password does not match");
        }
        else if (matches.Count() == 1)
        {
            Console.WriteLine($"Account Exist");
        }
        else
        {
            people.Add(register);
            Console.WriteLine($" You are now registered: {firstName} {lastName}");
            break;
        }
    }
    Save();
}
void logIntoBlog()
{
    //Login Section
    Console.WriteLine("Login Section");
    Console.Write("email: ");
    email = Convert.ToString(Console.ReadLine());
    Console.Write("Password: ");
    passWord = Convert.ToString(Console.ReadLine());

    BlogEntry login = new BlogEntry(firstName, lastName, email, passWord, blogDate, blogTitle, blogDescription);
    var matches = people.Where(p => p.GetEmail() == email && p.GetPassWord() == passWord).Take(1);

    if (email == "" || passWord == "")
    {
        Console.WriteLine("cannot be empyty");
    }
    else if (matches.Count() == 0)
    {
        Console.WriteLine($"No account found, Register Below");
    }
    else
    {
        people.Add(login);
        Console.WriteLine($" Hello {firstName} {lastName}");
    }
    Save();
}

void createEntry()
{
    //Make an Entry
    Console.WriteLine("Entry Section");
    Console.WriteLine("Make an entry below");
    Console.Write("Blog Title: ");
    blogTitle = Convert.ToString(Console.ReadLine());
    Console.Write("Blog Entry: ");
    blogDescription = Console.ReadLine();

    BlogEntry create = new BlogEntry(firstName, lastName, email, passWord, blogDate, blogTitle, blogDescription);
    var matches = people.Where(p => p.GetEmail() == email && p.GetPassWord() == passWord).Take(1);
    if (matches.Count() == 1)
    {
        people.Add(create);
        //Console.WriteLine($"Entry created {blogDate}");
        Console.WriteLine($"Entry Created Successfully");
    }
    Save();
}
void viewEntry()
{
    //View Section
    foreach (BlogEntry p in people)
    {
        Console.WriteLine($"Date of entry {p.GetEntryDate()}");
        Console.WriteLine($"Title: {p.GetTitle()} by {p.GetFirstName()} {p.GetLastName()}");
        Console.WriteLine(p.GetDescription());
    }
    Read();
}

//BinaryWriter
void Save()
{
    FileStream fsw = new FileStream("blog.dat", FileMode.Create);
    BinaryWriter bw = new BinaryWriter(fsw);

    foreach (Person p in people)
    {
        p.WriteBinary(bw);
    }

    bw.Close();
    fsw.Close();
}

//BinaryReader
void Read()
{
    //new list to store data and read from
    List<Person> peopleFromBinary = new List<Person>();
    FileStream fsr = new FileStream("blog.dat", FileMode.Open);
    BinaryReader br = new BinaryReader(fsr);

    while (br.BaseStream.Position < br.BaseStream.Length)
    {
        Person binPerson = null;
        string type = br.ReadString();
        if (type == "BlogEntry")
        {
            binPerson = new BlogEntry(firstName, lastName, email, passWord, blogDate, blogTitle, blogDescription);
            binPerson.ReadBinary(br);
        }
        peopleFromBinary.Add(binPerson);
    }

    br.Close();
    fsr.Close();
}



