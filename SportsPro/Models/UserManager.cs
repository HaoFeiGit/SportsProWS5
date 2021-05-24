using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.Models
{
    public interface IUserManager
    {
        User Authenticate(string username, string password);
    }

    public class UserManager : IUserManager
    {
        SportsProContext _context;

        public UserManager(SportsProContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {

            var user = _context.Users.
                SingleOrDefault(usr => usr.Username == username
                                && usr.Password == password);
            return user; //this will either be null or an object
        }
    }
}
