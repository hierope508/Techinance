using DAL;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLReport : BaseBLL<Report>, IBaseBLL<Report>
    {
        private readonly DALReport reportDAL;

        public BLLReport(DbContextOptions options) : base(options)
        {
            reportDAL = new DALReport(options);
            dal = reportDAL;
        }

        public override async Task Delete(Report obj)
        {
            try
            {
                obj.Enabled = false;
                Update(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public override async Task Insert(Report obj)
        {
            try
            {
                obj.Id = 0;
                await reportDAL.Insert(obj);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override void Update(Report obj)
        {
            try
            {
                reportDAL.Update(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        

        public string ExecuteReport(Report report)
        {
            DataTable dt = dal.ExecuteQuery(report.Query);

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return System.Text.Json.JsonSerializer.Serialize(rows);

        }
    }
}
