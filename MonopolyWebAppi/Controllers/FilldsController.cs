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
    public class FilldsController : ApiController
    {
        private Model1 db = new Model1();
        // GET: api/Fillds
        public IQueryable<Filld> GetFillds()
        {
            return db.Fillds;
        }

        // GET: api/Fillds/5/5
        //info1 = Game ID
        //info2 = Field Number

        [ResponseType(typeof(Filld))]
        public async Task<Filld> GetField(int info1, int info2)
        {
            MonopolyLogic logic = new MonopolyLogic();
         return   await Task.Run(() => logic.GetField(info1, info2));

        }

        // GET: api/Fillds/5
        //info1 = Game ID
        [ResponseType(typeof(Filld))]
        public async Task<List<Filld>> GetFields(int info1)
        {
            MonopolyLogic logic = new MonopolyLogic();
            return await Task.Run(() => logic.GetFields(info1));

        }



        //info1 Game ID
        //info2 Player ID
        //info3 Filld Number
        [ResponseType(typeof(void))]
        public async Task GetFilldBuy(int info1, int info2, int info3)
        {
               MonopolyLogic logic = new MonopolyLogic();
               await Task.Run(()=> logic.FilldBuy( info1, info2, info3));
            
        }
        
        //info1 Game ID
        //info2 Player ID
        //info3 Filld Number
        [ResponseType(typeof(void))]
        public async Task GetFilldSell(int info1, int info2, int info3)
        {
            MonopolyLogic logic = new MonopolyLogic();
            await Task.Run(() => logic.FilldSell(info1, info2, info3));

        }

        //info1 Game ID
        //info2 Player ID
        //info3 Filld Number
        public async Task GetPayRent(int info1, int info2, int info3)
        {
            MonopolyLogic logic = new MonopolyLogic();
            await Task.Run(() => logic.PayRent(info1, info2, info3));

        }
        // GET: api/Fillds/gameId/filldNumber/status
        public async Task GetFilldDepositAsync(int info1, int info2, string info3)
        {
            MonopolyLogic logic = new MonopolyLogic();
          await  logic.FilldDepositAsync(info1, info3, info2);
        }





        // PUT: api/Fillds/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFilld(int id, Filld filld)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != filld.ID)
            {
                return BadRequest();
            }

            db.Entry(filld).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilldExists(id))
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

        // POST: api/Fillds
        [ResponseType(typeof(Filld))]
        public async Task<IHttpActionResult> PostFilld(Filld filld)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fillds.Add(filld);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = filld.ID }, filld);
        }

        // DELETE: api/Fillds/5
        [ResponseType(typeof(Filld))]
        public async Task<IHttpActionResult> DeleteFilld(int id)
        {
            Filld filld = await db.Fillds.FindAsync(id);
            if (filld == null)
            {
                return NotFound();
            }

            db.Fillds.Remove(filld);
            await db.SaveChangesAsync();

            return Ok(filld);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FilldExists(int id)
        {
            return db.Fillds.Count(e => e.ID == id) > 0;
        }
    }
}