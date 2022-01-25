using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class DALUser : BaseDAL<User>
    {
        public DALUser(DbContextOptions options): base(options)
        {

        }

        public Task<User> Get(string login)
        {
            return Users.Where(u => u.Login == login).FirstOrDefaultAsync();
        }

        public override bool Exists(int id)
        {
            return Users.Where(u => u.Id == id).Any();

        }
    }
}
