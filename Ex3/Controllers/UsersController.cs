using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Ex3.Models;
using System.Security.Cryptography;
using System.Text;

namespace Ex3.Controllers
{
    public class UsersController : ApiController
    {
        private Ex3Context db = new Ex3Context();

        // GET: api/Users
        public IQueryable<Users> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> GetUsers(string id)
        {
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUsers(string id, Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != users.Name)
            {
                return BadRequest();
            }

            db.Entry(users).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        /// <summary>
        /// Post method to add user to the database.
        /// </summary>
        /// <param name="users">user to be added</param>
        /// <returns></returns>
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> PostUsers(Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SHA1 sha = SHA1.Create();
            byte[] buffer = Encoding.ASCII.GetBytes(users.Password);
            byte[] hash = sha.ComputeHash(buffer);
            string hash64 = Convert.ToBase64String(hash);
            users.Password = hash64;
            users.Wins = 0;
            users.Losses = 0;
            db.Users.Add(users);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsersExists(users.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = users.Name }, users);
        }

        // DELETE: api/Users/5
        /// <summary>
        /// Delete user from the data base
        /// </summary>
        /// <param name="id">user id to be deleted</param>
        /// <returns></returns>
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> DeleteUsers(string id)
        {
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            db.Users.Remove(users);
            await db.SaveChangesAsync();

            return Ok(users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsersExists(string id)
        {
            return db.Users.Count(e => e.Name == id) > 0;
        }

        // GET: api/Users/5
        /// <summary>
        /// Get user from database according to it's id and password.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("api/Users/GetUsers/{id}/{password}")]
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> GetUsers(string id, string password)
        {
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            SHA1 sha = SHA1.Create();
            byte[] buffer = Encoding.ASCII.GetBytes(password);
            byte[] hash = sha.ComputeHash(buffer);
            string hash64 = Convert.ToBase64String(hash);
            if(users.Password == hash64)
            {
                return Ok(users);
            }
            return NotFound();
        }

        // POST: api/Users/5
        [Route("api/Users/PostUsers/{userN}/{winner}")]
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> PostUsers(string userN,int winner)
        {
            Users user = await db.Users.FindAsync(userN);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(winner == 1)
            {
                user.Wins++;
            }
            else
            {
                user.Losses++;
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(user.Name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}