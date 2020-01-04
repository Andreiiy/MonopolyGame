using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonopoly
{
  public  class Field
    {
        public int ID { get; set; }

        public int GameID { get; set; }

        public int NumberFilld { get; set; }

        public string Owner { get; set; }

        public int Price { get; set; }

        public int Rent { get; set; }
                
        public string Status { get; set; }


        public Field GetField(NameValueCollection list)
        {
            Field field = new Field();
            field.ID = Int32.Parse(list["ID"]);

            field.GameID = Int32.Parse(list["GameID"]);

            field.NumberFilld = Int32.Parse(list["NumberFilld"]);

            field.Price = Int32.Parse(list["Price"]);

            field.Rent = Int32.Parse(list["Rent"]);

            field.Owner = list["Owner"];

            field.Status = list["Status"];

            
            return field;
        }

        public List<Field> GetFields(NameValueCollection list)
        {
            List<Field> fields = new List<Field>();

            string[] id = null;
            string[] gameid = null;
            string[] numberFilld = null;
            string[] owner = null;
            string[] price = null;
            string[] rent = null;
            string[] status = null;

            foreach (string key in list)
            {


                if (key == "ID")
                {
                    String ID = list[key];
                    id = ID.Split(',');
                }
                if (key == "GameID")
                {
                    String GameID = list[key];
                    gameid = GameID.Split(',');
                }

                if (key == "NumberFilld")
                {
                    String NumberFilld = list[key];
                    numberFilld = NumberFilld.Split(',');
                }

                if (key == "Owner")
                {
                    String Owner = list[key];
                    owner = Owner.Split(',');
                }

                if (key == "Price")
                {
                    String Price = list[key];
                    price = Price.Split(',');
                }

                if (key == "Rent")
                {
                    String Rent = list[key];
                    rent = Rent.Split(',');
                }

                if (key == "Status")
                {
                    String Status = list[key];
                    status = Status.Split(',');
                }
            }
                int i = 0;
                while (i < id.Length)
                {
                    Field field = new Field();
                    if (id[i] != "null")
                        field.ID = Int32.Parse(id[i]);

                    if (gameid[i] != "null")
                        field.GameID = Int32.Parse(gameid[i]);

                    if (numberFilld[i] != "null")
                        field.NumberFilld = Int32.Parse(numberFilld[i]);

                    if (owner[i] != "null")
                        field.Owner = owner[i];

                    if (price[i] != "null")
                        field.Price = Int32.Parse(price[i]);

                    if (status[i] != "null")
                        field.Status = status[i];

                    if (rent[i] != "null")
                        field.Rent = Int32.Parse(rent[i]);

                    
                    fields.Add(field);
                    i++;
                    
                }
         return fields;
        }
       
    }
}
