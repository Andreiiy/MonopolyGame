using MonopolyWebAppi4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MonopolyWebAppi4.Controllers
{
    public class EntryController : ApiController
    {
        private Model1 db = new Model1();
                
        public async Task<User> GetChackEntryAsync(string info1, string info2)
        {
            MonopolyLogic logic = new MonopolyLogic();
           // var user1 = logic.ChackEntry(info1, info2);
            var user = await Task.Run(() => logic.ChackEntry(info1, info2));
           return user;
        }

        //info1 User ID
        public async Task GetExitFromSystemAsync(int info1)
        {
            MonopolyLogic logic = new MonopolyLogic();
            await Task.Run(() => logic.ExitFromSystem(info1));
        }
    }
}
