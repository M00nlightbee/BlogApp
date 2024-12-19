using BlogApp;
using System.Linq;
using System.IO;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

//list Collection
List<Person> people = new List<Person>();

string firstName = "";
string lastName = "";
string email = "";
string passWord = "";
string confirmPassword;
string blogDate = DateTime.Now.ToString("dd/MM/yyyy");
string blogTitle = "";
string blogDescription= "";

//Full application calling created function for section
//for both readability and resueability
while (true)
{
    //Options section
    Console.Write("[R]egister,[L]ogin, or [Q]uit? ");
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
        Console.WriteLine("Register Section");
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

        //User input validation exception handling
        try
        {
            if (String.IsNullOrWhiteSpace(firstName) == true)
            {
                Console.WriteLine("First Name field can't be empty or whitespace");
            }
            else if (String.IsNullOrWhiteSpace(lastName) == true)
            {
                Console.WriteLine("Last Name field can't be empty or whitespace");
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
                Console.WriteLine("Account already exist");
            }
            else
            {
                people.Add(register);
                Console.WriteLine($" You are now registered: {firstName} {lastName}");
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
    Console.WriteLine("Login Section");
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
        Console.WriteLine("cannot be empyty");
    }
    else if (userMatch.Count() == 1)
    {
        // use multiple processor
        Parallel.ForEach(userMatch, p =>
        {
            Console.WriteLine($" Hello {p.GetFirstName()} {p.GetLastName()}");
        });
        while (true)
        {
            Console.WriteLine("Would you like to create an entry?");
            Console.Write("[Y]es or [N]o? ");
            string opt = Console.ReadLine().ToUpper();
            if (opt == "Y")
            {
                Create();
            }
            else if(opt == "N")
            {
                break;
            }
        }
    }
    else
    {
        Console.WriteLine("No account found, Register Below");
    }
}

//Make an Entry Function
void Create()
{
    Console.WriteLine("Entry Section");
    Console.WriteLine("Make an entry below");
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




