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
    public class MsgToUsersController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/MsgToUsers
        public IQueryable<MsgToUser> GetMsgToUsers()
        {
            return db.MsgToUsers;
        }

        // GET: api/MsgToUsers/userIdFrom/userIdTo
        // info1 -> ID UserFrom
        // info2 ->  IDUserTo
        [ResponseType(typeof(MsgToUser))]
        public async Task<List<MsgToUser>> GetMsgToUserAsync(int info1, int info2)
        {
            MonopolyLogic logic = new MonopolyLogic();
            var msgToUser =  await Task.Run(() => logic.GetMsgToUser(info1,info2));
            
            return msgToUser;
        }

        // GET: api/MsgToUsers/userIdFrom/userIdTo/msg
        // info1 -> ID UserFrom
        // info2 ->  IDUserTo
        // info3 -> Text Message
        
        [ResponseType(typeof(MsgToUser))]
        public async Task GetSendtMsgToUserAsync(string info1, int info2, int info3)
        {
            MonopolyLogic logic = new MonopolyLogic();
           // logic.SendMsg(info3, info2, info1);
            await Task.Run(() =>  logic.SendMsg(info2, info3, info1));
            
        }























        // PUT: api/MsgToUsers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMsgToUser(int id, MsgToUser msgToUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != msgToUser.ID)
            {
                return BadRequest();
            }

            db.Entry(msgToUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MsgToUserExists(id))
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

        // POST: api/MsgToUsers
        [ResponseType(typeof(MsgToUser))]
        public async Task<IHttpActionResult> PostMsgToUser(MsgToUser msgToUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MsgToUsers.Add(msgToUser);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = msgToUser.ID }, msgToUser);
        }

        // DELETE: api/MsgToUsers/5
        [ResponseType(typeof(MsgToUser))]
        public async Task<IHttpActionResult> DeleteMsgToUser(int id)
        {
            MsgToUser msgToUser = await db.MsgToUsers.FindAsync(id);
            if (msgToUser == null)
            {
                return NotFound();
            }

            db.MsgToUsers.Remove(msgToUser);
            await db.SaveChangesAsync();

            return Ok(msgToUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MsgToUserExists(int id)
        {
            return db.MsgToUsers.Count(e => e.ID == id) > 0;
        }
    }
}