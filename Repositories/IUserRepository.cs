using ShopAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAuth.Repositories
{
    public interface IUserRepository
    {
        Task<User> Get(string username, string password);
    }
}
