using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ApiUser = Mobile2020_TokenSample.Models.Entities.User;

namespace Mobile2020_TokenSample.Models.Services.Mappers
{
    internal static class Mappers
    {
        internal static User ToDbUser(this ApiUser entity)
        {
            return new User() { Id = entity.Id, LastName = entity.LastName, FirstName = entity.FirstName, Email = entity.Email, Passwd = SHA1.HashData(Encoding.Default.GetBytes("Test1234=")) };
        }

        internal static ApiUser ToApiUser(this User entity)
        {
            return new ApiUser() { Id = entity.Id, LastName = entity.LastName, FirstName = entity.FirstName, Email = entity.Email };
        }
    }
}
