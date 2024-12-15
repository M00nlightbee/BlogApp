using BlogApp;
using System.Linq;
using System.IO;
using Microsoft.Win32;

////list Collection
//List<Person> people = new List<Person>();

//string firstName = "Blessing"; 
//string lastName = "Uloyok";
//string email = "test@email.com";
//string passWord = "pass123";
//string confirmPassword;
//string blogDate = DateTime.Now.ToString("dd/MM/yyyy");
//string blogTitle = "Title";
//string blogDescription = "some description";

////NOTE: rewrite to check for user input use try catch for robustness

////MemoryStream memorystream = new MemoryStream();

//while (true)
//{
//    BlogEntry entry = new BlogEntry(firstName, lastName, email, passWord, blogDate, blogTitle, blogDescription);
//    people.Add(entry);

//    ////C:\Users\ouloy\Documents\uniYear2\Systems Programming\assessment\BlogApp\BlogApp\bin\Debug\net8.0 to access file
//    FileStream fsw = new FileStream("blog.dat", FileMode.Create);
//    BinaryWriter bw = new BinaryWriter(fsw);

//    foreach (Person p in people)
//    {
//        p.WriteBinary(bw);
//    }

//    bw.Close();
//    fsw.Close();

//    List<Person> peopleFromBinary = new List<Person>();

//    FileStream fsr = new FileStream("blog.dat", FileMode.Open);
//    BinaryReader br = new BinaryReader(fsr);

//    while (br.BaseStream.Position < br.BaseStream.Length)
//    {
//        Person binPerson = null;
//        string type = br.ReadString();
//        if (type == "BlogEntry")
//        {
//            binPerson = new BlogEntry(firstName, lastName, email, passWord, blogDate, blogTitle, blogDescription);
//            binPerson.ReadBinary(br);
//        }
//        peopleFromBinary.Add(binPerson);
//    }

//    br.Close();
//    fsr.Close();

//    //Login Section
//    Console.WriteLine("Login Section");
//    Console.Write("email: ");
//    email = Convert.ToString(Console.ReadLine());
//    Console.Write("Password: ");
//    passWord = Convert.ToString(Console.ReadLine());

//    //Algorithms
//    var matches = people.Where(p => p.GetEmail() == email && p.GetPassWord() == passWord).Take(1);

//    if (email == "" || passWord == "")
//    {
//        Console.WriteLine("cannot be empyty");
//    }
//    else if (matches.Count() == 0)
//    {
//        Console.WriteLine($"No account found, Register Below");
//    }
//    else
//    {
//        entry.LogIn();
//    }

//    //FirstOrDefault() in place of First() to avoid throwing error for null value
//    Person loggedIn = matches.FirstOrDefault();

//    while (true)
//    {
//        //Options section
//        Console.Write("[R]egister, [C]reate, [V]iew,or [Q]uit? ");
//        string choice = Console.ReadLine().ToUpper();

//        //Register Section
//        if (choice == "R")
//        {
//            while (true)
//            {
//                Console.WriteLine("Register Section");
//                Console.Write("First Name: ");
//                firstName = Convert.ToString(Console.ReadLine());
//                Console.Write("Last Name: ");
//                lastName = Convert.ToString(Console.ReadLine());
//                Console.Write("Email: ");
//                email = Convert.ToString(Console.ReadLine());
//                Console.Write("Password: ");
//                passWord = Convert.ToString(Console.ReadLine());
//                Console.Write("Confirm Password: ");
//                confirmPassword = Convert.ToString(Console.ReadLine());

//                BlogEntry newEntry = new BlogEntry(firstName, lastName, email, passWord, blogDate, blogTitle, blogDescription);

//                //Algorithms
//                //var match = people.Where(p => p.GetEmail() == email && p.GetPassWord() == passWord).Take(1);

//                if (firstName == "" || lastName == "" || passWord == "" || confirmPassword == "")
//                {
//                    Console.WriteLine("Cannot have empty field!!");
//                }
//                else if (firstName.Contains(" ") || lastName.Contains(" ") || email.Contains(" ") || passWord.Contains(" ") || confirmPassword.Contains(" "))
//                {
//                    Console.WriteLine("Each field MUST BE one word, no spaces");
//                }
//                else if (passWord != confirmPassword)
//                {
//                    Console.WriteLine("Password does not match");
//                }
//                else if (matches.Count() == 1)
//                {
//                    Console.WriteLine($"Account Exist");
//                }
//                else
//                {
//                    people.Add(newEntry);
//                    newEntry.Register();
//                    break;
//                }
//            }
//        }

//        //Make an Entry
//        else if (choice == "C")
//        {
//            Console.WriteLine("Entry Section");
//            Console.WriteLine("Make an entry below");

//            Console.Write("Blog Title: ");
//            blogTitle = Convert.ToString(Console.ReadLine());
//            Console.Write("Blog Entry: ");
//            blogDescription = Console.ReadLine();

//            Console.WriteLine($"Your entry for {blogDate} by {firstName} {lastName}");
//            Console.WriteLine($"Title: {blogTitle}");
//            Console.WriteLine(blogDescription);
//        }

//        //View Section
//        else if (choice == "V")
//        {
//            loggedIn.View();
//        }

//        //quit or logout 
//        else if (choice == "Q")
//        {
//            break;
//        }
//    }
//}

//list Collection
List<Person> people = new List<Person>();

string firstName = "Blessing";
string lastName = "Uloyok";
string email = "test@email.com";
string passWord = "pass123";
string confirmPassword;
string blogDate = DateTime.Now.ToString("dd/MM/yyyy");
string blogTitle = "Title";
string blogDescription = "some description";

var matches = people.Where(p => p.GetEmail() == email && p.GetPassWord() == passWord).Take(1);

//FirstOrDefault() in place of First() to avoid throwing error for null value
//Person loggedIn = matches.FirstOrDefault();

while (true)
{
    BlogEntry newEntry = new BlogEntry(firstName, lastName, email, passWord, blogDate, blogTitle, blogDescription);
    //Options section
    Console.Write("[R]egister,[L]ogin, [C]reate, [V]iew,or [Q]uit? ");
    string choice = Console.ReadLine().ToUpper();

    //Register Section
    if (choice == "R")
    {
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
                register.Register();
                break;
            }
        }
    }

    else if (choice == "L")
    {
        //Login Section
        Console.WriteLine("Login Section");
        Console.Write("email: ");
        email = Convert.ToString(Console.ReadLine());
        Console.Write("Password: ");
        passWord = Convert.ToString(Console.ReadLine());

        BlogEntry login = new BlogEntry(firstName, lastName, email, passWord, blogDate, blogTitle, blogDescription);
        people.Add(login);

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

            login.LogIn();
        }    
    }

    //Make an Entry
    else if (choice == "C")
    {
        Console.WriteLine("Entry Section");
        Console.WriteLine("Make an entry below");

        Console.Write("Blog Title: ");
        blogTitle = Convert.ToString(Console.ReadLine());
        Console.Write("Blog Entry: ");
        blogDescription = Console.ReadLine();

        Console.WriteLine($"Your entry for {blogDate} by {firstName} {lastName}");
        Console.WriteLine($"Title: {blogTitle}");
        Console.WriteLine(blogDescription);

        BlogEntry create = new BlogEntry(firstName, lastName, email, passWord, blogDate, blogTitle, blogDescription);
        //people.Add(create);

        if (matches.Count() == 1)
        {
            people.Add(create);
        }      
    }

    //View Section
    else if (choice == "V")
    {
        //BlogEntry view = new BlogEntry(firstName, lastName, email, passWord, blogDate, blogTitle, blogDescription);
        foreach (Person p in people) 
        {
            if (matches.Count() == 1)
            {
                Console.WriteLine($"Date of entry {blogDate}");
                Console.WriteLine($"Title: {blogTitle} by {firstName} {lastName}");
                Console.WriteLine(blogDescription);
            }
        }
      

    }

    //quit or logout 
    else if (choice == "Q")
    {
        break;
    }

    FileStream fsw = new FileStream("blog.dat", FileMode.Create);
    BinaryWriter bw = new BinaryWriter(fsw);

    foreach (Person p in people)
    {
        p.WriteBinary(bw);
    }

    bw.Close();
    fsw.Close();

    //new list to store read data
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







