using System;
using mones;
using hosam;
using kanan;
using ahmad;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Uml_Project_2023
{
    [Serializable]
    class Factory
    {
        static List<Buyer> listOfBuyers;
        static List<Seller> listOfSellers;
        public static List<Buyer> ListOfBuyers { get { return listOfBuyers; } set { listOfBuyers = value; } }
        public static List<Seller> ListOfSellers { get { return listOfSellers; } set { listOfSellers = value; } }
        public Factory()
        {
            listOfBuyers = new List<Buyer>();
            listOfSellers = new List<Seller>();

            Load();
        }

        public string Login()
        {
            string email, passWord;
            Console.WriteLine("please enter your Email ");
            email = Console.ReadLine();
            Console.WriteLine("please enter your password");
            passWord = Console.ReadLine();
            while (true)
            {
                foreach (Buyer b in listOfBuyers)
                {
                    if (b.Email == email)
                        if (b.Password == passWord)
                        {
                            Console.WriteLine("you have sucssesfully loged in");
                            return email;
                        }
                }
                foreach (Seller s in listOfSellers)
                {
                    if (s.Email == email)
                        if (s.Password == passWord)
                        {
                            Console.WriteLine("you have sucssesfully loged in");
                            return email;
                        }

                }
                Console.WriteLine("Your email or password are invalid , Enter your email and password again or Enter 1 to leave");
                email = Console.ReadLine();
                if (email == "1")
                    return null;
                Console.WriteLine("please enter your password");
                passWord = Console.ReadLine();

            }
        }
        public User GetAcc(string email)
        {
            Buyer b=listOfBuyers.Find(x=> x.Email == email);
            Seller s= listOfSellers.Find(x => x.Email == email);
            if (b == null) return s;
            if (s == null) return b;
            return null;//not needed as if we sent an email as a parameter it means its found.
        }
        public void Register()
        {

            string name, email, phone, passWord, Adress,  sellect;
            Console.WriteLine("if you want to creat an account as a buyer please press (1)");
            Console.WriteLine("if you want to creat an account as a seller please press (2)");
            sellect = Console.ReadLine();
            Console.WriteLine("please enter your name");
            name = Console.ReadLine();
            Console.WriteLine("please enter your email");
            email = Console.ReadLine();
            while (true)
            {
                string check = "";
                bool flag = true;
                if (sellect == "1")
                {
                    foreach (Buyer b in listOfBuyers)
                    {
                        if (b.Email == email)
                        {
                            Console.WriteLine("This email already exists");
                            Console.WriteLine("Enter a new Email or Enter 1 to exit to log in");
                            email = Console.ReadLine();
                            check = email;
                            flag = false;
                            break;
                        } 
                    }
                }
                if (sellect == "2")
                {
                    foreach (Seller b in listOfSellers)
                    {
                        if (b.Email == email)
                        {
                            Console.WriteLine("This email already exists");
                            Console.WriteLine("Enter a new Email or Enter 1 to exit to log in");
                            email = Console.ReadLine();
                            check = email;
                            flag = false;
                            break;
                        } 
                    }
                }
                    if (check == "1" )
                        return;
                    if (flag == true)
                        break;
            }
            Console.WriteLine("please enter your phone");
            phone = Console.ReadLine();
            //Console.WriteLine("pleas enter your userid");  Email is unique we don't need a user id
            //passWord = Console.ReadLine();
            Console.WriteLine("please enter your password");
            passWord = Console.ReadLine();
            Console.WriteLine("please enter your adress");
            Adress = Console.ReadLine();


            if (sellect == "1")
            {
                Buyer buyer = new Buyer(name,email,phone,passWord,Adress);
                listOfBuyers.Add(buyer);
            }

            if (sellect == "2")
            {
                Seller seller = new Seller(name, email, phone, passWord, Adress);
                listOfSellers.Add(seller);
            }
            Store();

        }
        public void searchOfItem(string listingName)
        {
            foreach(Seller seller in listOfSellers)
                    foreach(Listings listing in seller.ListOfListings)
                    {
                    if (listing.ListingName == listingName)
                        listing.ViewListing();
                    }
        }
        public static void Store()
        {
            FileStream BuyerFile = new FileStream("Buyers.txt", FileMode.Create, FileAccess.Write);
            FileStream SellerFile = new FileStream("Sellers.txt", FileMode.Create, FileAccess.Write);
            BinaryFormatter Bu = new BinaryFormatter();
            BinaryFormatter Se = new BinaryFormatter();
            for (int i = 0; i < listOfBuyers.Count; i++)
                Bu.Serialize(BuyerFile,listOfBuyers[i]);
            for (int i = 0; i < listOfSellers.Count; i++)
                Bu.Serialize(SellerFile, listOfSellers[i]);
            BuyerFile.Close();
            SellerFile.Close();
        }
        public void Load()
        {
            FileStream BuyerFile = new FileStream("Buyers.txt", FileMode.Open, FileAccess.Read);
            FileStream SellerFile = new FileStream("Sellers.txt", FileMode.Open, FileAccess.Read);
            BinaryFormatter Bu = new BinaryFormatter();
            BinaryFormatter Se = new BinaryFormatter();
            try
            {
                for (int i = 0; ; i++)
                    listOfBuyers.Add((Buyer)Bu.Deserialize(BuyerFile));
            }
            catch { }
            try
            {
                for (int i = 0; ; i++)
                    listOfSellers.Add((Seller)Bu.Deserialize(SellerFile));
            }
            catch{ }
            BuyerFile.Close();
            SellerFile.Close();
        }
        public void Menu1()
        {
            Console.WriteLine("to log in please Enter 1");
            Console.WriteLine("to register please Enter 2");
            Console.WriteLine("to exit Enter 3");
            Console.WriteLine("*********************************");
        }
        public void MenuSeller()
        {
            Console.WriteLine("*********************************");
            Console.WriteLine("1-Add an Item.");
            Console.WriteLine("2-Delete an Item");
            Console.WriteLine("3-Change account info");
            Console.WriteLine("4-View sold items");
            Console.WriteLine("5-View Your items");
            Console.WriteLine("6-Edit Your items");
        }
        public void MenuBuyer()
        {
            Console.WriteLine("*********************************");
            Console.WriteLine("1-Add an Item to your cart");
            Console.WriteLine("2-Delete an Item");
            Console.WriteLine("3-Change account info");
            Console.WriteLine("4-View Your Cart");
            Console.WriteLine("5-Check out!");
            Console.WriteLine("6-view Order!");
            Console.WriteLine("7-serach for item");
        }

    }

    class MainFun
    {
        static void Main()
        {
            Factory s = new Factory();
            Console.WriteLine("Hello, welcome to Our store ");
            Console.WriteLine("*********************************");
            while (true)
            {
                s.Menu1();
                string choice = Console.ReadLine();
                string email;

                if (choice == "1")
                {
                    email = s.Login();
                    if (s.GetAcc(email) != null)
                    {
                        Type o = s.GetAcc(email).GetType();

                        if (o.Name == "Buyer")
                        {
                            while (true)
                            {
                                Buyer buyer = (Buyer)s.GetAcc(email);
                                s.MenuBuyer();
                                Console.WriteLine("Enter a choice to continue or anything else to back");
                                string choice2 = Console.ReadLine();
                                if (choice2 == "1")
                                {
                                    buyer.buy();
                                }
                                else if (choice2 == "2")
                                {
                                    buyer.RemoveItemFromCart();
                                }
                                else if (choice2 == "3")
                                {
                                    buyer.changeBuyerInfo();                            
                                }
                                else if (choice2 == "4")
                                {
                                    buyer.CArt.ViewCart();
                                }
                                else if (choice2 == "5")
                                {
                                    buyer.PlaceOrder();
                                }
                                else if (choice2 == "6")
                                {
                                    buyer.DisplayOrder();
                                }
                                else if (choice2 == "7")
                                {
                                    buyer.SearchForItem();
                                }
                                else break;

                            }
                        }
                        else
                        {
                            while (true)
                            {
                                Seller seller = (Seller)s.GetAcc(email);
                                s.MenuSeller();
                                Console.WriteLine("Enter a choice to continue or anything else to back");
                                string choice3 = Console.ReadLine();
                                if (choice3 == "1")
                                {
                                    seller.CreateListing();
                                }
                                else if (choice3 == "2")
                                {
                                    seller.PromptDeleteListing();
                                }
                                else if (choice3 == "3")
                                {
                                    seller.changeSellerInfo();
                                }
                                else if (choice3 == "4")
                                {
                                    seller.ViewSold();
                                }
                                else if (choice3 == "5") {
                                    seller.DisplayListings();
                                }
                                else if(choice3=="6")
                                {
                                    seller.EditListing();
                                }
                                else
                                    break;

                            }


                        }

                    }
                }
                else if (choice == "2")
                {
                    s.Register();
                }
                else if (choice == "3")
                    return;
            }
        }
    }
}
