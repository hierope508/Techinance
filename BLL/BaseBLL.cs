using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class BaseBLL<T> where T : class
    {
        protected BaseDAL<T> dal;

        public BaseBLL(DbContextOptions options)
        {

        }

        public async Task<T> Get(int id)
        {
            return await dal.Select(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await dal.SelectAll();
        }

        
        public bool Exists(int id)
        {
            try
            {
                return dal.Exists(id);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public abstract Task Insert(T obj);

        public abstract Task Delete(T obj);
        public abstract void Update(T obj);

    }
}
