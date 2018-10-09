using ClaimsTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaimsTraining.Services
{
    public interface IUserService
    {
        User Authenticate(String UserName, String UserPass);
        IEnumerable<User> GetAll();
    }

    public class UserService : IUserService
    {
        public User Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
