using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ClientMonopoly;

namespace Monopoly_WPF
{
    public class Board
    {

        private List<Field> fields;
        MainWindow mainWindow;
        Client client;
        Random rand ;
        private List<String> listChance;

        public Board(MainWindow mainWindow , Client client)
        {
            this.mainWindow = mainWindow;
            fields = new List<Field>();
            this.client = client;
            rand = new Random();

            fields = new List<Field>() {
             
            new Field{number = 0,name="start",left= 15,top = 0,status ="",price = 0,rent = 0 },
            new Field{number = 1,name= "Porsche",left = 145,top = 0,status = "free",price= 50000,rent = 20000 },
            new Field{number = 2,name= "Lamborghini",left = 220, top = 0, status = "free",price =  25000,rent = 10000},
            new Field{number = 3,name= "BMW",left = 295,top = 0 ,status= "free", price = 35000, rent = 15000},
            new Field{number = 4,name= "chance",left = 370,top = 0 , status= "", price = 0, rent = 0},
                              
            new Field{number = 5,name="Manchester",left = 445,top = 0, status= "free", price = 45000, rent = 25000},
            new Field{number = 6,name="Arsenal",left = 520,top = 0, status= "free", price = 30000, rent = 15000},
            new Field{number = 7,name="Chelsea",left = 595,top = 0, status= "free", price = 25000, rent = 10000},
            new Field{number = 8,name="jail",left = 670,top = 0, status= "", price = 0,rent = 0},
                               
            new Field{number = 9,name="AMD",left = 670,top = 130, status = "free", price = 65000,rent = 25000},
            new Field{number =10,name="Intel",left = 670,top = 195, status = "free", price = 80000,rent = 30000},
            new Field{number =11,name="chance",left =  670,top = 260,status = "", price = 0,rent = 0},
                              
            new Field{number =12,name="Acer",left =670,top = 325, status = "free", price = 40000,rent = 20000},
            new Field{number =13,name="Asus",left = 670,top = 390, status = "free", price = 50000,rent = 25000},
            new Field{number =14,name="Lenovo",left = 670,top = 455, status = "free", price = 30000,rent = 15000},
            new Field{number =15,name="parking",left = 670,top = 515, status = "", price = 0, rent = 0},
                              
            new Field{number =16,name="Nintendo",left = 595,top = 515, status = "free", price = 60000,rent = 25000},
            new Field{number =17,name="Sony",left = 520,top = 515, status = "free", price = 40000,rent = 20000},
            new Field{number =18,name="Nvidia",left = 445,top = 515, status = "free", price = 35000,rent = 15000},
            new Field{number =19,name="chance",left = 370,top = 515, status = "", price = 0,rent = 0},

            new Field{number =20, name="Thomson",left = 295,top = 515, status ="free", price = 15000,rent = 10000},
            new Field{number =21, name="Panasinic",left = 220,top = 515, status ="free", price = 25000,rent = 10000},
            new Field{number =22, name="Philips",left = 145,top = 515, status ="free", price = 10000,rent = 3000},
            new Field{number =23, name="gotojail",left = 15,top = 515, status ="", price = 0,rent = 0},
                        
            new Field{number =24, name="HUAWEI",left = 15,top = 450, status ="free", price = 15000,rent = 5000},
            new Field{number =25, name="Aple",left =15,top = 385, status ="free", price = 25000,rent = 10000},
            new Field{number =26, name="SAMSUNG",left =15,top = 320, status ="free", price = 20000,rent = 7000},
            new Field{number =27, name="chance",left = 15,top = 255, status ="", price = 0,rent = 0},
                        
            new Field{number =28, name="Microsoft",left =15,top = 195, status ="free", price = 10000,rent = 3000 },
            new Field{number =29, name="Linux", status ="free",left =15,top = 130, price = 90000, rent = 40000 },
        };

            listChance = new List<string>()
            {
                "get10",
                "get15",
                "pay10",
                "pay15",
                "gotostart",
                "gotojoil"
            };
        }

        public Field GetField(int number)
        {

            return fields[number];
        }

        public List<Field> GetFields()
        {
            return fields;
        }

        public void BuyField(int numberField, Player owner)
        {
            fields[numberField].owner = owner.Color;
            fields[numberField].status = "buy";
            SetOwnertoLableFilld(numberField, owner.Color);
            owner.myFilds.Add(fields[numberField]);
            if(owner.myFilds.Count >= 3)
            {
                if(owner.Color.Equals("Red"))
                    mainWindow.red.Source = new BitmapImage(new Uri(@"/Monopoly_WPF;component/" + "Image\\Plaers\\red2.png", UriKind.Relative));
                else if(owner.Color.Equals("Green"))
                     mainWindow.green.Source = new BitmapImage(new Uri(@"/Monopoly_WPF;component/" + "Image\\Plaers\\green2.png", UriKind.Relative));
                else 
                    mainWindow.yellow.Source = new BitmapImage(new Uri(@"/Monopoly_WPF;component/" + "Image\\Plaers\\yellow2.png", UriKind.Relative));
            }
        }

        public void RentField(int number, string owner)
        {

            fields[number].status = "rent";
        }

        private void SetOwnertoLableFilld(int numberFilld, string collorUser)
        {

            switch (numberFilld)
            {
                case 1:
                    {
                        mainWindow.f1.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f1, collorUser);
                    }
                    break;
                case 2:
                    {
                        mainWindow.f2.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f2, collorUser);
                    }
                    break;
                case 3:
                    {
                        mainWindow.f3.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f3, collorUser);
                    }
                    break;
                case 5:
                    {
                        mainWindow.f5.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f5, collorUser);
                    }
                    break;
                case 6:
                    {
                        mainWindow.f6.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f6, collorUser);
                    }
                    break;
                case 7:
                    {
                        mainWindow.f7.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f7, collorUser);
                    }
                    break;
                case 9:
                    {
                        mainWindow.f9.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f9, collorUser);
                    }
                    break;
                case 10:
                    {
                        mainWindow.f10.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f10, collorUser);
                    }
                    break;
                case 12:
                    {
                        mainWindow.f12.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f12, collorUser);
                    }
                    break;
                case 13:
                    {
                        mainWindow.f13.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f13, collorUser);
                    }
                    break;
                case 14:
                    {
                        mainWindow.f14.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f14, collorUser);

                    }
                    break;
                case 16:
                    {
                        mainWindow.f16.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f16, collorUser);

                    }
                    break;
                case 17:
                    {
                        mainWindow.f17.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f17, collorUser);

                    }
                    break;
                case 18:
                    {
                        mainWindow.f18.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f18, collorUser);

                    }
                    break;
                case 20:
                    {
                        mainWindow.f20.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f20, collorUser);

                    }
                    break;
                case 21:
                    {
                        mainWindow.f21.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f21, collorUser);

                    }
                    break;
                case 22:
                    {
                        mainWindow.f22.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f22, collorUser);

                    }
                    break;
                case 24:
                    {
                        mainWindow.f24.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f24, collorUser);

                    }
                    break;
                case 25:
                    {
                        mainWindow.f25.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f25, collorUser);

                    }
                    break;
                case 26:
                    {
                        mainWindow.f26.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f26, collorUser);

                    }
                    break;
                case 28:
                    {
                        mainWindow.f28.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f28, collorUser);

                    }
                    break;
                case 29:
                    {
                        mainWindow.f29.Content = collorUser;
                        SetColorForgtoFilld(mainWindow.f29, collorUser);

                    }
                    break;
            }
            

        }
        
        public void SellField(int numberField)
        {
            fields[numberField].owner = "";
            fields[numberField].status = "free";

            switch (numberField)
            {
                case 1:
                    {
                        mainWindow.f1.Content = "Free";
                        mainWindow.f1.Foreground = Brushes.White;
                    }
                    break;
                case 2:
                    {
                        mainWindow.f2.Content = "Free";
                        mainWindow.f2.Foreground = Brushes.White;
                    }
                    break;
                case 3:
                    {
                        mainWindow.f3.Content = "Free";
                        mainWindow.f3.Foreground = Brushes.White;
                    }
                    break;
                case 5:
                    {
                        mainWindow.f5.Content = "Free";
                        mainWindow.f5.Foreground = Brushes.White;
                    }
                    break;
                case 6:
                    {
                        mainWindow.f6.Content = "Free";
                        mainWindow.f6.Foreground = Brushes.White;
                    }
                    break;
                case 7:
                    {
                        mainWindow.f7.Content = "Free";
                        mainWindow.f7.Foreground = Brushes.White;
                    }
                    break;
                case 9:
                    {
                        mainWindow.f9.Content = "Free";
                        mainWindow.f9.Foreground = Brushes.White;
                    }
                    break;
                case 10:
                    {
                        mainWindow.f10.Content = "Free";
                        mainWindow.f10.Foreground = Brushes.White;
                    }
                    break;
                case 12:
                    {
                        mainWindow.f12.Content = "Free";
                        mainWindow.f12.Foreground = Brushes.White;
                    }
                    break;
                case 13:
                    {
                        mainWindow.f13.Content = "Free";
                        mainWindow.f13.Foreground = Brushes.White;
                    }
                    break;
                case 14:
                    {
                        mainWindow.f14.Content = "Free";
                        mainWindow.f14.Foreground = Brushes.White;

                    }
                    break;
                case 16:
                    {
                        mainWindow.f16.Content = "Free";
                        mainWindow.f16.Foreground = Brushes.White;

                    }
                    break;
                case 17:
                    {
                        mainWindow.f17.Content = "Free";
                        mainWindow.f17.Foreground = Brushes.White;

                    }
                    break;
                case 18:
                    {
                        mainWindow.f18.Content = "Free";
                        mainWindow.f18.Foreground = Brushes.White;

                    }
                    break;
                case 20:
                    {
                        mainWindow.f20.Content = "Free";
                        mainWindow.f20.Foreground = Brushes.White;

                    }
                    break;
                case 21:
                    {
                        mainWindow.f21.Content = "Free";
                        mainWindow.f21.Foreground = Brushes.White;

                    }
                    break;
                case 22:
                    {
                        mainWindow.f22.Content = "Free";
                        mainWindow.f22.Foreground = Brushes.White;

                    }
                    break;
                case 24:
                    {
                        mainWindow.f24.Content = "Free";
                        mainWindow.f24.Foreground = Brushes.White;

                    }
                    break;
                case 25:
                    {
                        mainWindow.f25.Content = "Free";
                        mainWindow.f25.Foreground = Brushes.White;

                    }
                    break;
                case 26:
                    {
                        mainWindow.f26.Content = "Free";
                        mainWindow.f26.Foreground = Brushes.White;

                    }
                    break;
                case 28:
                    {
                        mainWindow.f28.Content = "Free";
                        mainWindow.f28.Foreground = Brushes.White;

                    }
                    break;
                case 29:
                    {
                        mainWindow.f29.Content = "Free";
                        mainWindow.f29.Foreground = Brushes.White;

                    }
                    break;
            }


        }

        public void UpdataFields(int gameID)
        {
            var serverFields = client.GetFields(gameID);
            foreach(var serverField in serverFields)
            {
                if (fields[serverField.NumberFilld].status == "buy" && serverField.Status == "free")
                    SellField(serverField.NumberFilld);
                
                fields[serverField.NumberFilld].owner = serverField.Owner;
                fields[serverField.NumberFilld].status = serverField.Status;
            }
        }

        private void SetColorForgtoFilld(Label lable, string collor)
        {
            if (collor == "Red") lable.Foreground = Brushes.LightPink;
            else if (collor == "Green") lable.Foreground = Brushes.LightGreen;
            else lable.Foreground = Brushes.LightYellow;
        }

        public void ChengeFilld(ClientMonopoly.Field field)
        {
            fields[field.NumberFilld].number = field.NumberFilld;
            fields[field.NumberFilld].owner = field.Owner;
            fields[field.NumberFilld].status = field.Status;
        }

        public List<Field> GetMyFillds(string colorPlayer)
        {
            return fields.Where(f => (f.owner == colorPlayer)).ToList();
                 
        }

        public string GetChance()
        {
            return listChance[rand.Next(0, listChance.Count - 1)];
        }


    }
}
