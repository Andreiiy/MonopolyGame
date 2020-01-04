using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonopoly
{
   public class User
    {
        public int ID { get; set; }
       
        public string Name { get; set; }
               
        public string Password { get; set; }

        public string Email { get; set; }

        public int Points { get; set; }
               
        public string Status { get; set; }

        public string StatusInSystem { get; set; }

        public string Rank { get; set; }

        public string Gender { get; set; }

        public int NumberWins { get; set; }

        public int NumberLosses { get; set; }

        public User GetUser(NameValueCollection list)
        {
            User user = new User();
           user. ID =Int32.Parse( list["ID"]);

          user. Name = list["Name"];
          
          user. Password = list["Password"];
          
          user. Email = list["Email"];
          
          user. Points = Int32.Parse(list["Points"]);
          
          user. Status = list["Status"];
          
          user. StatusInSystem = list["StatusInSystem"];
         
          user. Gender = list["Gender"];
          
          user. Rank = list["Rank"];
          
          user. NumberWins = Int32.Parse(list["NumberWins"]);
          
          user. NumberLosses = Int32.Parse(list["NumberLosses"]);
            return user;
        }

        public List<User> GetUsers(NameValueCollection list)
        {
            List<User> users = new List<User>();

            string[] id = null;
            string[] name = null;
            string[] password = null;
            string[] email = null;
            string[] points = null;
            string[] status = null;
            string[] statusInSystem = null;
            string[] gender = null;
            string[] rank = null;
            string[] numberWins = null;
            string[] numberLosses = null;

            foreach (string key in list)
            {


                if (key == "ID")
                {
                 String ID = list[key];
                    id = ID.Split(',');
                }
                if (key == "Name")
                {
                String Name = list[key];
                    name = Name.Split(',');
                }
                   
                if (key == "Password")
                {
                    String Password = list[key];
                    password = Password.Split(',');
                }

                if (key == "Email")
                {
                    String Email = list[key];
                    email = Email.Split(',');
                }

                if (key == "Points")
                {
                    String Points = list[key];
                    points = Points.Split(',');
                }

                if (key == "Status")
                {
                    String Status = list[key];
                    status = Status.Split(',');
                }

                if (key == "StatusInSystem")
                {
                    String StatusInSystem = list[key];
                    statusInSystem = StatusInSystem.Split(',');
                }

                if (key == "Gender")
                {
                    String Gender = list[key];
                    gender = Gender.Split(',');
                }

                if (key == "Rank")
                {
                    String Rank = list[key];
                    rank = Rank.Split(',');
                }


                if (key == "NumberWins")
                {
                    String NumberWins = list[key];
                    numberWins = NumberWins.Split(',');
                }

                if (key == "NumberLosses")
                {
                    String NumberLosses = list[key];
                    numberLosses = NumberLosses.Split(',');
                }
            }

            int i = 0;
            while (i < id.Length)
            {
                User user = new User();
                if (id[i] != "null")
                    user.ID = Int32.Parse(id[i]);

                if (name[i] != "null")
                    user.Name = name[i];

                if (password[i] != "null")
                    user.Password = password[i];

                if (email[i] != "null")
                    user.Email = email[i];

                if (points[i] != "null")
                    user.Points = Int32.Parse(points[i]);

                if (status[i] != "null")
                    user.Status = status[i];

                if (statusInSystem[i] != "null")
                    user.StatusInSystem = statusInSystem[i];

                if (gender[i] != "null")
                    user.Gender = gender[i];
                               

                if (rank[i] != "null")
                    user.Rank = rank[i];

                if (numberWins[i] != "null")
                    user.NumberWins = Int32.Parse(numberWins[i]);

                if (numberLosses[i] != "null")
                    user.NumberLosses = Int32.Parse(numberLosses[i]);
                users.Add(user);
                i++;
            }
            return users;
        }
    }
}
