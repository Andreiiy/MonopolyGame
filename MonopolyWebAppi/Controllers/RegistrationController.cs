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
    public class RegistrationController : ApiController
    {
        // GET: api/Registration/
        // info1 -> User Name
        // info2 -> User Email
        // info3 -> User Password
        // return string: "falseName";"falsePassword";true
            
        public async Task<string> GetRegistrationAsync(string info1, string info2,string info3,string info4)
        {
            MonopolyLogic logic = new MonopolyLogic();
               return await Task.Run(() => logic.CreateNewUser(info1, info2, info3,info4));
            
            
        }
    }
}
