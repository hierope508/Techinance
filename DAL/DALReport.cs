using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALReport : BaseDAL<Report>
    {
        public DALReport(DbContextOptions options) : base(options)
        {

        }

        public override Task<List<Report>> SelectAll()
        {
            return Reports.Where(r => r.Enabled).ToListAsync();
        }

        public override bool Exists(int id)
        {
            return Reports.Where(p => p.Id == id).Any();
        }
    }
}
