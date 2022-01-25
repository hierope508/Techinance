using DAL;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLUser : BaseBLL<User>, IBaseBLL<User>
    {

        private readonly DALUser userDAL;

        public BLLUser(DbContextOptions<BaseContext> options) : base(options)
        {
            userDAL = new DALUser(options);
            dal = userDAL;
        }

        public async Task<bool> Authenticate(string login, string password)
        {
            try
            {
                User user = await userDAL.Get(login);
                return BLLAuthentication.Authenticate(user, password);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public override async Task Delete(User obj)
        {
            try
            {
                await dal.Delete(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public override async Task Insert(User obj)
        {
            try
            {
                obj.Id = 0;
                GenerateUserPassword(ref obj);
                await dal.Insert(obj);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void GenerateUserPassword(ref User user)
        {
            string password = user.Password;
            if (String.IsNullOrEmpty(password))
                throw new Exception("Password can't be null");

            BLLSecurity bllSecurity = new BLLSecurity();
            string hashedPassword = bllSecurity.GenerateHashedPassword(password);
            user.Password = hashedPassword;
        }

        public override void Update(User obj)
        {
            try
            {
                dal.Update(obj);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
