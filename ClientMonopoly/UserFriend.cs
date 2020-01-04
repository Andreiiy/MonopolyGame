using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonopoly
{
    public  class UserFriend
    {
        public int ID { get; set; }

        public int UserId { get; set; }

        public int FreindId { get; set; }

        public List<UserFriend> GetUserFriends(NameValueCollection list)
        {

            List<UserFriend> friends = new List<UserFriend>();

            string id = "";
            string userid = "";
            string friendid = "";
            foreach (string key in list)
            {


                if (key == "ID")
                    id = list[key];

                if (key == "UserId")
                    userid = list[key];

                if (key == "FreindId")
                    friendid = list[key];
            }

            for (int i = 0; i < 6; i++)
            {
                if (i == 0 || i % 2 == 0)
                {
                    ClientMonopoly.UserFriend friend = new UserFriend();
                    int num = id[i] - '0';
                    friend.ID = num;
                    num = userid[i] - '0';
                    friend.UserId = num;
                    num = friendid[i] - '0';
                    friend.FreindId = num;
                    friends.Add(friend);
                }
            }
            return friends;
        }
    }

}
