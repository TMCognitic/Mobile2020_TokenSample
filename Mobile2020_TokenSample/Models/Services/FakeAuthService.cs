using Mobile2020_TokenSample.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mobile2020_TokenSample.Models.Services
{
    public class FakeAuthService : IAuthService
    {
        private static List<User> _users;

        static FakeAuthService()
        {
            _users = new List<User>(new User[]
                {
                    new User() { Id = 1, LastName = "Morre", FirstName = "Thierry", Email ="thierry.morre@cognitic.be", Passwd = SHA1.HashData(Encoding.Default.GetBytes("Test1234=")) }
                });
        }

        public int Register(User user)
        {
            user.Id = _users.Max(user => user.Id) + 1;
            _users.Add(user);
            return user.Id;
        }

        public User Login(string email, string passwd)
        {
            return _users.SingleOrDefault(u => u.Email == email.ToLower() && Convert.ToBase64String(u.Passwd) == Convert.ToBase64String(SHA1.HashData(Encoding.Default.GetBytes(passwd))));
        }

        public bool Check(User user)
        {
            return _users.SingleOrDefault(u => u.Email == user.Email && u.Id == user.Id /* Actif, non banni, etc....*/) is not null;
        }
    }
}
