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
using MonopolyWebAppi4.Models;

namespace MonopolyWebAppi4.Controllers
{
    public class UsersController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/Users/GetUsers
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        public async Task<List<User>> GetUsersWithoutFriends(int info1)
        {
            MonopolyLogic logic = new MonopolyLogic();
            return await Task.Run(() => logic.GetUsersWithoutFriends(info1));
        }
        
         // GET: api/Users/GetUser/5
         //info1 = ID User
         [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int info1)
        {
            User user = await db.Users.FindAsync(info1);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Users/GetUserByName/5
        //info1 = ID User
        [ResponseType(typeof(User))]
        public async Task<User> GetUserByName(string info1)
        {
            MonopolyLogic logic = new MonopolyLogic();
            return await Task.Run(() => logic.GetUserByName(info1));
                      
        }













        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.ID)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.ID }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.ID == id) > 0;
        }
    }
}