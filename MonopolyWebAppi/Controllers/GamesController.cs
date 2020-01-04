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
    public class GamesController : ApiController
    {
        private Model1 db = new Model1();


        // GET: api/Games/GetGames/2
        
        // info2 -> User ID
        public List<GameInfo> GetGames(int info1)
        {
            MonopolyLogic logic = new MonopolyLogic();
            List<GameInfo> gameInfos = logic.GetGamesWait(info1);
            return gameInfos;
        }



        /// GET: api/Games/GetCreateGame/withAll/2
        /// <summary>
        /// using the Monopoly Logic class object, processes the client’s request to create a new game
        /// </summary>
        /// <param name="info1">Game Status</param>
        /// <param name="info2">User ID</param>
        /// <returns>new player information</returns>
        public async Task<NewPlayerInGame> GetCreateGame(string info1,int info2)
        {

            MonopolyLogic logic = new MonopolyLogic();
            NewPlayerInGame newPlayer = await Task.Run(() => logic.CreateGame(info1,info2));

            return newPlayer;
        }

        // GET: api/Games/5/5
        // GET: api/Games/
        // info11 -> User ID
        // info22 -> Game ID
        public async Task<NewPlayerInGame> GetAddPlayerToGame(int info1, int info2)
        {
            MonopolyLogic logic = new MonopolyLogic();

           NewPlayerInGame newPlayer =await Task.Run(() => logic.AddPlayerToGame(info1,info2));
            return newPlayer;
        }

        // GET: api/Games/GetDellGame/5/5
        // GET: api/Games/
        // info1 -> User ID
        // info2 -> Game ID

        public async Task GetDellGame( int info1, int info2)
        {
            MonopolyLogic logic = new MonopolyLogic();
            
                await Task.Run(() => logic.DellGame(info1, info2));
        }

        // GET: api/Games/GetDellGame/5/5
        // GET: api/Games/
        // info1 -> Game ID

        public async Task<string> GetCheckReadyPlay(int info1)
        {
            MonopolyLogic logic = new MonopolyLogic();

          return  await Task.Run(() => logic.CheckReadyPlay(info1));
        }




        // GET: api/Games/GetDellPlayerFromGame/5/5
        // GET: api/Games/
        // info1 -> User ID
        // info2 -> Game ID
        public async Task GetDellPlayerFromGame(int info1, int info2, string info3)
        {
            MonopolyLogic logic = new MonopolyLogic();
           
                await Task.Run(() => logic.DellPlayerFromGame(info1, info2));

        }

        // GET: api/Games/5/info
        // id -> Game ID
        public async Task<GameInfo> GetGameInfo(int info1)
        {
            MonopolyLogic logic = new MonopolyLogic();

            GameInfo gameinfo =await Task.Run(() => logic.GetGameInfo(info1));
            return gameinfo;
        }






        // PUT: api/Games/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGame(int id, Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != game.ID)
            {
                return BadRequest();
            }

            db.Entry(game).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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

        // POST: api/Games
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> PostGame(Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Games.Add(game);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = game.ID }, game);
        }

        // DELETE: api/Games/5
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> DeleteGame(int id)
        {
            Game game = await db.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            db.Games.Remove(game);
            await db.SaveChangesAsync();

            return Ok(game);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameExists(int id)
        {
            return db.Games.Count(e => e.ID == id) > 0;
        }
    }
}