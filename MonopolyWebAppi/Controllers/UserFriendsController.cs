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
    public class UserFriendsController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/UserFriends
        public IQueryable<UserFriend> GetUserFriends()
        {
            return db.UserFriends;
        }

        // GET: api/UserFriends/5
        [ResponseType(typeof(UserFriend))]
        public async Task<IQueryable<UserFriend>> GetUserFriendAsync(int info1)
        {
            MonopolyLogic monopolyLogic = new MonopolyLogic();
            return await Task.Run(() => monopolyLogic.GetUserFriends(info1));
        }


        // GET: api/UserFriends/5/5
        //inform1 = User ID
        //inform2 = Friend ID

        [ResponseType(typeof(UserFriend))]
        public async Task GetAddUserToFriends(int info1,int info2)
        {
            MonopolyLogic monopolyLogic = new MonopolyLogic();
           await monopolyLogic.AddUserToFrendsAsync(info1, info2);
           

        }
















        // PUT: api/UserFriends/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserFriend(int id, UserFriend userFriend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userFriend.ID)
            {
                return BadRequest();
            }

            db.Entry(userFriend).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserFriendExists(id))
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

        // POST: api/UserFriends
        [ResponseType(typeof(UserFriend))]
        public async Task<IHttpActionResult> PostUserFriend(UserFriend userFriend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserFriends.Add(userFriend);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userFriend.ID }, userFriend);
        }

        // DELETE: api/UserFriends/5
        [ResponseType(typeof(UserFriend))]
        public async Task<IHttpActionResult> DeleteUserFriend(int id)
        {
            UserFriend userFriend = await db.UserFriends.FindAsync(id);
            if (userFriend == null)
            {
                return NotFound();
            }

            db.UserFriends.Remove(userFriend);
            await db.SaveChangesAsync();

            return Ok(userFriend);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserFriendExists(int id)
        {
            return db.UserFriends.Count(e => e.ID == id) > 0;
        }
    }
}