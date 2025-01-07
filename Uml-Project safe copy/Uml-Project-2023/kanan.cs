using System;
using hosam;
using mones;
using ahmad;
using Uml_Project_2023;
using System.Collections.Generic;
namespace kanan
{
    [Serializable]
    class Seller:User
    {
        List<Listings> listOfListings;
        public List<Listings> listOfSold;
        public List<Listings> ListOfListings { get { return listOfListings; } }
        //public List<Listings> ListOfSold { get { return ListOfSold; } set { listOfSold = value; } }
        public Seller() : this("", "", "", "", "") { }
        public Seller(string storeName, string email,  string phone, string password,string address) :base(storeName, email, phone, password, address)
        {
            listOfListings = new List<Listings>();
            listOfSold = new List<Listings>();
        }
        public void changeSellerInfo()
        {
            try
            {
                Console.WriteLine("to change information press");
                Console.WriteLine("1-address\n2-name\n3-phone\n4-password");
                int x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter a new password");
                string value = Console.ReadLine();
                switch (x)
                {
                    case 1:
                        base.Address = value;
                        Console.WriteLine("address is changed successfully to " + value);
                        break;
                    case 2:
                        base.Name = value;
                        Console.WriteLine("name is changed successfully to " + value);
                        break;
                    case 3:
                        base.Phone = value;
                        Console.WriteLine("phone is changed successfully to " + value);
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
                Console.WriteLine("Your inputs are wrong");
                return;
            }
             
        }
        public void CreateListing()
        {
            try
            {
                Console.WriteLine("please enter the listing name: ");
                string listingName = Console.ReadLine();
                Console.WriteLine("please enter the listing description: ");
                string description = Console.ReadLine();
                Console.WriteLine("please enter the listing price: ");
                double price = Convert.ToDouble(Console.ReadLine());
                Listings listing = new Listings(listingName, description, Email, price);
                this.AddListing(listing);
                Console.WriteLine("listing added sussesfully");
            }
            catch
            {
                Console.WriteLine("Your inputs are wrong");
                return;
            }
        }
        public void AddListing(Listings listing)
        {
            listOfListings.Add(listing);
            Factory.Store();
        }
        public void PromptDeleteListing()
        {
            try
            {
                this.DisplayListings();
                Console.WriteLine("please enter the number of the listing you want to delete.");
                int index = Convert.ToInt32(Console.ReadLine()) - 1;
                this.DeleteListing(index);
                Console.WriteLine("the listing has deleted.");
            }
            catch
            {
                Console.WriteLine("Your inputs are wrong");
                return;
            }
        }
        public void DeleteListing(int index)
        {
            listOfListings.Remove(listOfListings[index]);
            Factory.Store();
        }
        public void EditListing()
        {
            try
            {
                this.DisplayListings();
                int index = Convert.ToInt32(Console.ReadLine()) - 1;
                listOfListings[index].ViewListing();
                Console.WriteLine("What do you want to edit in this listing? (please use the numbers (1-3)");
                int info = Convert.ToInt32(Console.ReadLine());
                switch (info)
                {
                    case 1:
                        Console.WriteLine("please enter the new name of the listing: ");
                        listOfListings[index].ListingName = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("please enter the new description of the listing: ");
                        listOfListings[index].Description = Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("please enter the new price of the listing: ");
                        listOfListings[index].Price = Convert.ToDouble(Console.ReadLine());
                        break;
                }
                Console.WriteLine("Informations edited successfully!");
                listOfListings[index].ViewListing();
                Factory.Store();
            }
            catch
            {
                Console.WriteLine("Your inputs are wrong");
                return;
            }
        }
        public void DisplayListings()
        {
            int counter = 1;
            Console.WriteLine("*********************************");
            foreach (Listings l in listOfListings)
            {
                Console.WriteLine("Listing  " + counter+": ");
                l.ViewListing();
                counter++;
            }
            if (listOfListings.Count == 0)
                Console.WriteLine("There is no items");
            Console.WriteLine("*********************************");
        }
        public void DisplaySeller()
        {
            Console.WriteLine(Name);
            this.DisplayListings();
        }
        public void ViewSold()
        {
            foreach(Listings l in listOfSold)
            {
                l.ViewListing();
            }
        }
    }
    [Serializable]
    class Listings
    {
        string listingName;
        string description;
        string sellerEmail;
        double price;
        public string ListingName { get { return listingName; } set { listingName = value; } }
        public string Description { get { return description; } set { description = value; } }
        public double Price { get { return price; } set { price = value; } }
        public string SellerEmail { get { return sellerEmail; } set { sellerEmail = value; } }
        public Listings() : this("", "", "",0) { }
        public Listings(string listingName, string description, string email,double price)
        {
            ListingName = listingName;
            Description = description;
            Price = price;
            sellerEmail = email;
        }
        public void ViewListing()
        {
            //Console.WriteLine("seller info: " + sellerEmail);
            Console.WriteLine("1-Listing name: " + ListingName);
            Console.WriteLine("2-Listing description: " + Description);
            Console.WriteLine("3-Listing price: " + Price+"$");
            Console.WriteLine("************************************************");
        }
    }
}

