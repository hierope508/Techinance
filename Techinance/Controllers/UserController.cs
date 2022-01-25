using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Techinance.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {

        private readonly BLLUser userBLL;

        public UserController(BLLUser userBLL)
        {
            this.userBLL = userBLL;
        }

        // GET: UserController
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            try
            {
                return await userBLL.Get(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                throw;
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            try
            {
                return await userBLL.GetAll();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost]
        public async Task<ActionResult> Insert(User user, string password)
        {
            try
            {
                user.Password = password;
                await userBLL.Insert(user);
                return CreatedAtAction("Get", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Edit(User updatedUser)
        {
            try
            {
                User user = await userBLL.Get(updatedUser.Id);

                if (user == null)
                    return NotFound();

                userBLL.Update(updatedUser);

                return CreatedAtAction("Get", new { id = user.Id }, user);


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
                User user = await userBLL.Get(id);

                if (user == null)
                    return NotFound();

                await userBLL.Delete(user);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Authenticate")]
        public async Task<ActionResult> Authenticate(string login, string password)
        {
            try
            {
                var validUser = await userBLL.Authenticate(login, password);

                if (!validUser)
                {
                    return Unauthorized();
                }

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
    }
}
