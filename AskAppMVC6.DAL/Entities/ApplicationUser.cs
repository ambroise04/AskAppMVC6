using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AskAppMVC6.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}