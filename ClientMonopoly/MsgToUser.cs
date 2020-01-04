using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonopoly
{
   public class MsgToUser
    {
        public int ID { get; set; }

        public int UserIdFrom { get; set; }

        public int UserIdTo { get; set; }

        
        public string Message { get; set; }

        public string Date { get; set; }


        public List<MsgToUser> GetMessages(NameValueCollection list)
        {

            string[] id = null;
            string[] useridfrom = null;
            string[] useridto = null;
            string[] msge = null;
            string[] date = null;
            List<MsgToUser> masseges = new List<MsgToUser>();
            foreach (string key in list)
            {

                if (key == "ID")
                {
                    String ID = list[key];
                    id = ID.Split(',');
                }
                if (key == "UserIdFrom")
                {
                    String UserIdFrom = list[key];
                    useridfrom = UserIdFrom.Split(',');
                }
                if (key == "UserIdTo")
                {
                    String UserIdTo = list[key];
                    useridto = UserIdTo.Split(',');
                }
                if (key == "Message")
                {
                    String Message = list[key];
                    msge = Message.Split(new string[] { "~f~," }, StringSplitOptions.None);
                }
                if (key == "Date")
                {
                    String Date = list[key];
                    date = Date.Split(',');
                }
            }
            int i = 0;
            if (id != null)
                while (i < id.Length)
            {
                MsgToUser msg = new MsgToUser();
                if (id[i] != "null")
                    msg.ID = Int32.Parse(id[i]);
                if (useridfrom[i] != "null")
                    msg.UserIdFrom = Int32.Parse(useridfrom[i]);
                if (useridto[i] != "null")
                    msg.UserIdTo = Int32.Parse(useridto[i]);
                if (msge[i] != "null")
                    msg.Message = msge[i];
                if (date[i] != "null")
                    msg.Date = date[i];

                masseges.Add(msg);
                i++;
            }


            return masseges;
        }
    }
}
