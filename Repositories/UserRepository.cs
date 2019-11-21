using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using ShopAuth.Data;
using ShopAuth.Models;

namespace ShopAuth.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDbContext context;

        public UserRepository(MongoDbContext context)
        {
            this.context = context;
        }

        public Task<User> Get(string username, string password)
        {
            return this.context.Users.Find(u => u.Username.ToLower() == username.ToLower() && u.Password == password).FirstOrDefaultAsync();
        }
    }
}