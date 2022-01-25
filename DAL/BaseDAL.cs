using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class BaseDAL<T> : BaseContext where T : class
    {

        public BaseDAL(DbContextOptions options) : base(options)
        {

        }

        /// <summary>
        /// Get all <typeparamref name="T"/> objects from DB
        /// </summary>
        /// <returns>A List of <typeparamref name="T"/></returns>
        public virtual async Task<List<T>> SelectAll()
        {
            return await Set<T>().ToListAsync();
        }

        /// <summary>
        /// Get an <typeparamref name="T"/> filtering by Id
        /// </summary>
        /// <param name="id">The id of the <typeparamref name="T"/></param>
        /// <returns>A <typeparamref name="T"/></returns>
        public virtual async Task<T> Select(int id)
        {
            return await Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Update a <typeparamref name="T"/>
        /// </summary>
        /// <param name="obj"> <typeparamref name="T"/> object to be updated</param>
        /// <returns><typeparamref name="T"/> object</returns>
        public virtual void Update(T obj)
        {
            try
            {
                Set<T>().Update(obj);
                SaveChanges();
            }
            catch (Exception)
            {
                Entry(obj).State = EntityState.Unchanged;
                throw;
            }

        }

        /// <summary>
        /// Insert a <typeparamref name="T"/> on database
        /// </summary>
        /// <param name="obj"><typeparamref name="T"/> to be added</param>
        /// <returns></returns>
        public virtual async Task<T> Insert(T obj)
        {
            try
            {
                Set<T>().Add(obj);
                await SaveChangesAsync();
            }
            catch (Exception)
            {
                Entry(obj).State = EntityState.Detached;
                throw;
            }

            return obj;
        }

        /// <summary>
        /// Delete a <typeparamref name="T"/> from database
        /// </summary>
        /// <param name="obj">Object to be deleted</param>
        /// <returns></returns>
        public virtual async Task Delete(T obj)
        {
            try
            {
                Set<T>().Remove(obj);
                await SaveChangesAsync();
            }
            catch (Exception)
            {
                Entry(obj).State = EntityState.Unchanged;
                throw;
            }

        }

        /// <summary>
        /// Check if a <typeparamref name="T"/> exists in DB
        /// </summary>
        /// <param name="id">The id of the obj</param>
        /// <returns>True if exists, false otherwise</returns>
        public abstract bool Exists(int id);


        public DataTable ExecuteQuery(string query)
        {
            using (var context = this)
            {
                var dt = new DataTable();
                var conn = context.Database.GetDbConnection();
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open) conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // error handling
                    throw;
                }
                finally
                {
                    if (connectionState != ConnectionState.Closed) conn.Close();
                }

                return dt;
            }
        }
    }
}
