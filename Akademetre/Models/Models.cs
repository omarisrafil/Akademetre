using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Akademetre.Models
{
    public class Models
    {
    }

    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool IsNull()
        {
            return 
                string.IsNullOrEmpty(Name) && 
                string.IsNullOrEmpty(Surname) && 
                string.IsNullOrEmpty(Email) && 
                string.IsNullOrEmpty(Address) ? 
                true : false;
        }
    }

    public class HomeViewModel
    {
        public Person Person { get; set; }
        public List<Person> People { get; set; }
    }
}