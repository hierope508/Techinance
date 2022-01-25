using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IBaseBLL<T>
    {
        public Task<T> Get(int id);

        public Task<List<T>> GetAll();


        public bool Exists(int id);

        public Task Insert(T obj);

        public Task Delete(T obj);
        public void Update(T obj);
    }
}
