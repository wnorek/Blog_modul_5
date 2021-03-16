using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class UserRoles
    {
        public const string SuperUser = "SuperUser";
        public const string Admin = "Admin";
        public const string User = "User";
        public const string AdminOrUser = Admin + "," + User;
        public const string SuperUserOrAdmin = SuperUser + "," + Admin;
        public const string SuperUserOrUser = SuperUser + "," + User;
        public const string SuperUserOrAdminOrUser = SuperUser + "," + Admin + "," + User;
        
    }
}
