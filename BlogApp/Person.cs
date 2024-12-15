using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlogApp
{
    internal abstract class Person
    {
        //Encapsulation
        //protected data members
        protected string firstName;
        protected string lastName;
        protected string email;
        protected string passWord;

        //Person Contructor
        public Person(string fName, string lName, string myEmail, string pwd)
        {
            firstName = fName;
            lastName = lName;
            email = myEmail;
            passWord = pwd;
        }

        public string GetFirstName() { return firstName; }
        public string GetLastName() { return lastName; }
        public string GetEmail() { return email; }
        public string GetPassWord() { return passWord; }

        public void LogIn()
        {
            Console.WriteLine($" Hello {firstName} {lastName}");
        }
        public void Register()
        {
            Console.WriteLine($" You are now registered: {firstName} {lastName}");
        }

        //Polymorphism
        //Serialisation and binary
        public abstract void WriteBinary(BinaryWriter bw);

        //Deserialisation and binary
        public abstract void ReadBinary(BinaryReader br);

    }
}
