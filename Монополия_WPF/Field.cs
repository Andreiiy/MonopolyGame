using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_WPF
{
    public class Field
    {
        public int number { get; set; }
        public string name { get; set; }
        public int left { get; set; }
        public int top { get; set; }

        public string status { get; set; }
        public string owner { get; set; }

        public int price { get; set; }
        public int rent { get; set; }


        //public Field(int number, string name, int left, int top, string status = "", string owner = "", int price = 0, int rent = 0)
        //{
        //    this.number = number;
        //    this.name = name;
        //    this.left = left;
        //    this.top = top;
        //    this.status = status;
        //    this.owner = owner;
        //    this.price = price;
        //    this.rent = rent;
        //}


    }
}
