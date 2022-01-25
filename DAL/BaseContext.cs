using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BaseContext : DbContext
    {
        protected BaseContext(DbContextOptions options) : base(options)
        {
#if DEBUG
            this.Database.EnsureCreated();
#endif
        }


        protected DbSet<User> Users { get; set; }

        protected DbSet<Report> Reports { get; set; }

    }
}
