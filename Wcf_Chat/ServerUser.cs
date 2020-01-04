using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Wcf_Chat
{
    public class ServerUser
    {
        public int ID {get;set;}
        public string Name { get; set; }
        public string Password { get; set; }
        public int GameID { get; set; }
        public OperationContext operationContext { get; set; }
    }
}
