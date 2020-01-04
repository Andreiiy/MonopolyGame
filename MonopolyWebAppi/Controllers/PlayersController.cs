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
    public class PlayersController : ApiController
    {
        private Model1 db = new Model1();
        MonopolyLogic logic = new MonopolyLogic();
        // GET: api/Players
        public IQueryable<Player> GetPlayers()
        {
            return db.Players;
        }

        // GET: api/Players/5
        // info1: User ID
        // info2: Game ID
        [ResponseType(typeof(Player))]
        public async Task<Player> GetPlayer(int info1, int info2)
        {

            return await Task.Run(() => logic.GetPlayer(info1, info2));

        }

        public async Task<Player> GetPlayerbyID(int info1)
        {
            Player player = logic.GetPlayerbyID(info1);
            return await Task.Run(() => logic.GetPlayerbyID(info1));

        }

        // info1: Player ID
        // info2: Game ID
        public async Task GetPlayerLost(int info1)
        {            
             await Task.Run(() => logic.PlayerLost(info1));
        }

        public async Task GetPlayerMyGameWinner(int info1)
        {
            await Task.Run(() => logic.PlayerMyGameWinner(info1));
        }

        // GET: api/GetMovePlayer/Players/7/Red/17
        // info1:Game ID
        // info2:Player Color
        // info3:Place 
        public async Task<GameInfo> GetMovePlayer(int info1, string info2, int info3)
        {
            
            logic.MovePlayer (info1, info2, info3);  
            GameInfo gameinfo =await Task.Run(() => logic.GetGameInfo(info1));
            return gameinfo;
        }


        // info1: Player ID
        // info2: sum money
        public async Task GetPlayerAddMoney(int info1, int info2)
        {
           await Task.Run(() => logic.PlayerAddMoney(info1,info2));
        }

        // info1: Player ID
        // info2: sum money
        public async Task GetPlayerReduceMoney(int info1, int info2)
        {
           await Task.Run(() => logic.PlayerReduceMoney(info1, info2));
        }

        // PUT: api/Players/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlayer(int id, Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != player.ID)
            {
                return BadRequest();
            }

            db.Entry(player).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // POST: api/Players
        [ResponseType(typeof(Player))]
        public async Task<IHttpActionResult> PostPlayer(Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Players.Add(player);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = player.ID }, player);
        }

        // DELETE: api/Players/5
        [ResponseType(typeof(Player))]
        public async Task<IHttpActionResult> DeletePlayer(int id)
        {
            Player player = await db.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            db.Players.Remove(player);
            await db.SaveChangesAsync();

            return Ok(player);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayerExists(int id)
        {
            return db.Players.Count(e => e.ID == id) > 0;
        }
    }
}