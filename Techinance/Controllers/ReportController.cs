using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Techinance.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : Controller
    {
        private readonly BLLReport reportBLL;
        public ReportController(BLLReport reportBLL)
        {
            this.reportBLL = reportBLL;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> Get(int id)
        {
            try
            {
                var report = await reportBLL.Get(id);

                if (report == null)
                    return NotFound();
                
                return report;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Report>>> GetAll()
        {
            try
            {
                return await reportBLL.GetAll();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        
        [HttpPost]
        public async Task<ActionResult> Insert(Report report)
        {
            try
            {
                await reportBLL.Insert(report);
                return CreatedAtAction("Get", new { id = report.Id }, report);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Edit(Report updatedReport)
        {
            try
            {
                Report report = await reportBLL.Get(updatedReport.Id);

                if (report == null)
                    return NotFound();

                reportBLL.Update(updatedReport);

                return CreatedAtAction("Get", new { id = report.Id }, report);


            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Report report = await reportBLL.Get(id);

                if (report == null)
                    return NotFound();

                await reportBLL.Delete(report);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Execute/{id}")]
        public async Task<ActionResult> ExecuteReport(int id)
        {
            try
            {
                Report report = await reportBLL.Get(id);

                if (report == null)
                    return NotFound();

                string result = reportBLL.ExecuteReport(report);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
