using System;
using hosam;
using kanan;
using ahmad;
using Uml_Project_2023;
using System.Collections.Generic;
using System.Drawing;
namespace mones
{
    [Serializable]
    class Buyer:User
    {
        Cart cart;
        Payment payment;
        Order order;
        public Buyer ()
        {
            cart = new Cart();
            payment = new Payment();
        }
        public Buyer(string name ,string email,string phone ,string password ,string address): base(name, email, phone, password, address)
        {
            payment= new Payment();
            cart = new Cart();
        }
        public Cart CArt
        {
            get{
                return cart;
            }
        }
        public void changeBuyerInfo()
        {
            try {
                Console.WriteLine("to change information press");
                Console.WriteLine("1-address\n2-name\n3-phone\n4-password");
                int x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the new value");
                string value = Console.ReadLine();
                switch (x)
                {
                    case 1:
                        base.Address = value;
                        Console.WriteLine("address is changed successful to " + value);
                        break;
                    case 2:
                        base.Name = value;
                        Console.WriteLine("name is changed successful to " + value);
                        break;
                    case 3:
                        base.Phone = value;
                        Console.WriteLine("phone is changed successful to " + value);
                        break;

                    case 4:
                        bool flag = false;
                        while (true)
                        {
                            Console.WriteLine("Your password is invalid Enter the new passowrd again or press -1 to exit");
                            value = Console.ReadLine(); ;
                            if (IsValidPassword(value))
                            {
                                base.Password = value;
                                Console.WriteLine("password is changed successfully to " + value);
                                flag = true;
                            }
                            if (flag || value == "-1")
                                break;
                        }
                        break;
                }
                Factory.Store();
            }
            catch
            {
                Factory.Store();
                Console.WriteLine("Your inputs are wrong");
                return;
            }
            
        } 
        public void DisplayListings()
        {
            Console.WriteLine("we are here to present our Items");
            foreach(Seller s in Factory.ListOfSellers)
            {
                s.DisplayListings();            
            }
        }
        public void buy () 
        {
            try
            {
                int counter = 1;
                while (true)
                {//counter so it wont display 0 for all or 1 for all
                 // int counter1 = 0;
                    Console.WriteLine("choose the Seller Number you want to buy from (1-" + Factory.ListOfSellers.Count + ")");
                    foreach (Seller s in Factory.ListOfSellers)
                    {
                        Console.WriteLine(counter + "))" + s.Name);
                        counter++;
                        /*1))monesphones
                         ahmads bakery*/
                    }
                    Console.WriteLine("Enter -1 to exit");
                    int numberOfShop = Convert.ToInt32(Console.ReadLine());
                    if (numberOfShop == -1)
                        break;
                    else
                    {
                        Seller thisseller = Factory.ListOfSellers[numberOfShop - 1];
                        if (thisseller.ListOfListings.Count != 0)
                        {
                            while (true)
                            {
                                Console.WriteLine("pleas choose the item you need to add into your cart");
                                Console.WriteLine("to go back press -1");
                                Console.WriteLine("*********************************");

                                thisseller.DisplayListings();
                                int thisItem = Convert.ToInt32(Console.ReadLine()) - 1;
                                if (thisItem == -2)
                                    break;
                                else
                                {
                                    cart.AddCartItem(thisseller, thisItem);
                                    Console.WriteLine("Item added to your cart");
                                    Console.WriteLine("*********************************");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("This seller has no items Yet");
                            Console.WriteLine("*********************************");
                        }
                    }
                    counter = 1;
                }
            }
            catch
            {
                Console.WriteLine("Your inputs are wrong");
                return;
            }

        }
        public void PlaceOrder()
        {   
            payment.AddNewPaymentMethod();
            if (payment.Valid == true)
            { 
                Order order = new Order();
                this.order = order;
                
                cart.CheckOut();
                Factory.Store();
                Console.WriteLine("Your items have been checked out successfuly");
            }
            else
            {
                Console.WriteLine("Your Order placment was not successful");
            }
        }
        public void DisplayOrder()
        {
            order.DisplayOrder();
        }
        public void RemoveItemFromCart()
        {
            try
            {
                cart.RemoveFromCart();
            }
            catch
            {
                Console.WriteLine("Your inputs are wrong");
                return;
            }
        }
        public void SearchForItem()
        {
            try {
                Console.WriteLine("Enter the name of the item ");
                string name = Console.ReadLine();
                name = name.ToLower();
                Listings searchedItem = new Listings();
                foreach (Seller s in Factory.ListOfSellers)
                {
                    Listings ls = s.ListOfListings.Find(q => q.ListingName.ToLower() == name);
                    if (ls != null)
                        searchedItem = ls; ;

                }
                if (searchedItem.ListingName.ToLower()==name)
                {
                    searchedItem.ViewListing();
                    Console.WriteLine("Do you want to buy the item? y or n");
                    string inp = Console.ReadLine();
                    if (inp == "y" || inp == "Y")
                    {
                        cart.AddSearchedItem(searchedItem);
                    }
                    else
                        return;
                }
                else
                    Console.WriteLine("This item does not exist");
            }
            catch
            {
                Console.WriteLine("Wrong inputs");
            }
            }

    }
    //end of calss buyer***************************
    [Serializable]
    class Order
    {
        int orderId=0;
        static int counter=1;
        string orderDate;
        string arriveDate = "unknown";

        public Order()
        {
            orderId += counter;
            counter++;
            DateNow();
            arrivalDate();
        }

        public int OrderId
        {
            //there is no setter
           get{return orderId;}
        }
        public void DateNow()
        {
            int MonthNow = System.DateTime.Now.Month;
            int YearNow = System.DateTime.Now.Year;
            int DayNow = System.DateTime.Now.Day;
            orderDate = (DayNow + "\\" + MonthNow + "\\" + YearNow);
        }
        public void arrivalDate()
        {
            int MonthNow = System.DateTime.Now.Month;
            int YearNow = System.DateTime.Now.Year;
            int DayNow = System.DateTime.Now.Day;
            arriveDate = ((DayNow+4) + "\\" + MonthNow + "\\" + YearNow);
        }
        public string OrderDate
        {
            set { orderDate = value; } get { return orderDate; }
        }
        public string ArriveDate
            {
                 set{arriveDate = value;}get{return arriveDate;}
            }
        public void DisplayOrder()
        {
            Console.WriteLine("Order ID: "+orderId);
            Console.WriteLine("order date:" + orderDate);
            Console.WriteLine("Order arrival date: " + arriveDate);
        }
    }
    //end of class
}

