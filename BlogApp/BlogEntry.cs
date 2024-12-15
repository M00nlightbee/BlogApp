
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlogApp
{
    //Inheritance
    internal class BlogEntry : Person
    {
        //Encapsulation
        //public data members
        protected string entryDate;
        protected string title;
        protected string description;

        //Constructor
        public BlogEntry(string fName, string lName, string myEmail, string pwd, string entryDay, string entryTitle, string entryDescription) : base(fName, lName, myEmail, pwd)
        {
            entryDate = entryDay;
            title = entryTitle;
            description = entryDescription;
        }

        //Polymorphism
        //Serialisation and binary
        //writing input as binary to file
        public override void WriteBinary(BinaryWriter bw)
        {
            bw.Write("BlogEntry");
            bw.Write(firstName);
            bw.Write(lastName);
            bw.Write(email);
            bw.Write(passWord);
            bw.Write(entryDate);
            bw.Write(title);
            bw.Write(description);
        }

        //Deserialisation and binary
        //reading saved binary file 
        public override void ReadBinary(BinaryReader br)
        {
            this.firstName = br.ReadString();
            this.lastName = br.ReadString();
            this.email = br.ReadString();
            this.passWord = br.ReadString();
            this.entryDate = br.ReadString();
            this.title = br.ReadString();
            this.description = br.ReadString();
        }
    }
}
