using BlogApp;
using System.Linq;
using System.IO;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;
using Spectre.Console;

//list Collection
//test@email.com
//pass09&DO
List<Person> people = new List<Person>();

string firstName = "";
string lastName = "";
string email = "";
string passWord = "";
string confirmPassword;
string blogDate = DateTime.Now.ToString("dd/MM/yyyy");
string blogTitle = "";
string blogDescription = "";

StringBuilder sb = new StringBuilder(1000);

//AnsiConsole.MarkupLine("[center]Blog App\n[/] [italic blue]\nBy: Owajigbana Blessing Uloyok[/]");

var rule = new Rule("[bold green]Blog App\n[/] [italic blue]\nBy: Owajigbana Blessing Uloyok[/]");
rule.Centered();
AnsiConsole.Write(rule);

//Full application calling created function for section
//for both readability and resueability
while (true)
{
    //Options section
    Console.Write("Options: [R]egister,[L]ogin, or [Q]uit? ");
    string choice = Console.ReadLine().ToUpper();

    //Register Section
    if (choice == "R")
    {
        Register();
    }
    //Login Section
    else if (choice == "L")
    {
        Login();
    }
    //quit or logout 
    else if (choice == "Q")
    {
        break;
    }
}

//Register Section Function
void Register()
{
    while (true)
    {
        AnsiConsole.MarkupLine("[bold yellow]\nRegister Section[/] ");
        //Console.WriteLine("\nRegister Section");
        Console.Write("First Name: ");
        firstName = Convert.ToString(Console.ReadLine().ToUpper());
        Console.Write("Last Name: ");
        lastName = Convert.ToString(Console.ReadLine().ToUpper());
        Console.Write("Email: ");
        email = Convert.ToString(Console.ReadLine());
        Console.Write("Password: ");
        passWord = Convert.ToString(Console.ReadLine());

        BlogEntry register = new BlogEntry(firstName, lastName, email, passWord, blogDate, blogTitle, blogDescription);

        //Algorithm (linq query)
        var userMatch = people.Where(p => p.GetEmail() == email && p.GetPassWord() == passWord).Take(1);

        // Regular expressions for email format
        Regex regexEmail = new Regex(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$");
        // Regular expressions for strong password and length of >= 8
        Regex validatePwd = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

        //StringBuilder sb = new StringBuilder();

        //User input validation exception handling
        try
        {
            if (String.IsNullOrWhiteSpace(firstName) == true)
            {
                //sb.Append("First Name field can't be empty or whitespace");
                //Console.WriteLine(sb);
                AnsiConsole.MarkupLine("[#ff0000]First Name field can't be empty or whitespace[/]");
            }
            else if (String.IsNullOrWhiteSpace(lastName) == true)
            {
                //sb.Append("Last Name field can't be empty or whitespace");
                //Console.WriteLine(sb);
                AnsiConsole.MarkupLine("[#ff0000]Last Name field can't be empty or whitespace[/]");
            }
            else if (!regexEmail.IsMatch(email))
            {
                throw new InvalidEmailException(email);
            }
            else if (!validatePwd.IsMatch(passWord))
            {
                throw new InvalidPasswordException(passWord);
            }
            else if (userMatch.Count() == 1)
            {
                sb.Append("Account already exist");
                //Console.WriteLine(sb);
                AnsiConsole.MarkupLine("[#ff0000]Account already exist[/]");
            }
            else
            { 
                people.Add(register);

            
                AnsiConsole.MarkupLine($"[#00ff00]You are now registered: {firstName} {lastName}\n[/]");
                break;
            }
        }
        catch (InvalidEmailException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (InvalidPasswordException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    // save to users file
    SaveUser();
}

void Login()
{
    //BinaryReader with list to read into list and access to print back to console
    List<Person> blogFromBinary = new List<Person>();

    //replace filestream to memorystream and store in memory
    FileStream fsr = new FileStream("user.dat", FileMode.Open);
    BinaryReader br = new BinaryReader(fsr);

    while (br.BaseStream.Position < br.BaseStream.Length)
    {
        Person loggedin = null;
        string type = br.ReadString();
        if (type == "BlogEntry")
        {
            loggedin = new BlogEntry(firstName, lastName, email, passWord, blogDate, blogTitle, blogDescription);
            loggedin.ReadBinary(br);
        }
        blogFromBinary.Add(loggedin);
    }

    br.Close();
    fsr.Close();

    //For user entry
    AnsiConsole.MarkupLine("[bold yellow]\nLogin Section[/] ");
    //Console.WriteLine("\nLogin Section");
    Console.Write("Email: ");
    email = Convert.ToString(Console.ReadLine());
    Console.Write("Password: ");
    passWord = Convert.ToString(Console.ReadLine());

    //Algorithm(linq query)
    IEnumerable<Person> userMatch = from p in blogFromBinary
                                    where p.GetEmail() == email && p.GetPassWord() == passWord
                                    select p;

    if (email == "" || passWord == "")
    {
        sb.Append("cannot be empyty\n");
        Console.WriteLine(sb);
    }
    else if (userMatch.Count() == 1)
    {
        // use multiple processor
        Parallel.ForEach(userMatch, p =>
        {
            sb.Append($"Hello {p.GetFirstName()} {p.GetLastName()}\n");
            Console.WriteLine(sb);
        });
        while (true)
        {
            AnsiConsole.MarkupLine("[bold yellow]Would you like to create an entry?[/] ");
            Console.Write("[Y]es or [N]o? ");
            string opt = Console.ReadLine().ToUpper();
            if (opt == "Y")
            {
                Create();
            }
            else if (opt == "N")
            {
                break;
            }
        }
    }
    else
    {
        sb.Append("No account found, Register Below");
        Console.WriteLine(sb);
    }
}

//Make an Entry Function
void Create()
{
    AnsiConsole.MarkupLine("[bold yellow]\nEntry Section[/] ");
    //Console.WriteLine("Entry Section");
    Console.WriteLine("Make an entry below:");
    Console.Write("Blog Title: ");
    blogTitle = Convert.ToString(Console.ReadLine());
    Console.Write("Blog Entry: ");
    blogDescription = Console.ReadLine();

    BlogEntry create = new BlogEntry(firstName, lastName, email, passWord, blogDate, blogTitle, blogDescription);

    if (!String.IsNullOrEmpty(blogTitle))
    {
        people.Add(create);
        Console.WriteLine(create.CreateEntry());
    }
    // save to entry file
    SaveEntry();
}

//BinaryWriter Function
void SaveUser()
{
    FileStream fsw = new FileStream("user.dat", FileMode.Create);
    BinaryWriter bw = new BinaryWriter(fsw);

    foreach (Person p in people)
    {
        p.WriteBinary(bw);
    }

    bw.Close();
    fsw.Close();
}

void SaveEntry()
{
    FileStream fsw = new FileStream("entry.dat", FileMode.Create);
    BinaryWriter bw = new BinaryWriter(fsw);

    foreach (Person p in people)
    {
        p.WriteBinary(bw);
    }

    bw.Close();
    fsw.Close();
}







