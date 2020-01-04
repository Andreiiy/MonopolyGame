using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Wcf_Chat
{
   public class ServerUserInGame
    {
        public int ID { get; set; }
        public int GameID { get; set; }
        public string Name { get; set; }
        public OperationContext operationContext { get; set; }
    }
}
