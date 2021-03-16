using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public Author()
        {
               
        }

        public Author(string name, string surname, string email)
        {
            (Name, Surname, Email) = (name, surname, email);
        }
    }
}
