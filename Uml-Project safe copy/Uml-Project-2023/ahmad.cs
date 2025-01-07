using System;
using System.Collections.Generic;
using kanan;
using mones;
using Uml_Project_2023;
namespace ahmad
{
    [Serializable]
    class Cart
    {
        private string description;
        private List<Listings> listingCartList;
        private Payment pay;
        public Cart(List<Listings> listings, string itemName, string description)
        {
            this.listingCartList = listings;
            this.description = description;
            pay = new Payment();
        }
        public Cart() : this(new List<Listings>(), "", "") { }
        public List<Listings> Lists { get { return listingCartList; } set { this.listingCartList = value; } }
        public string Description { set { this.description = value; } get { return this.description; } }
        public void AddCartItem(Seller sell, int itemNumber)
        {
            if (sell.ListOfListings[itemNumber] != null)
            {
                listingCartList.Add(sell.ListOfListings[itemNumber]);// if we had an error check the index here
            }
            else
            {
                Console.WriteLine("This item is not available");
                return;
            }
        }
        public void AddSearchedItem(Listings Item)
        {
            listingCartList.Add(Item);
            Factory.Store();
            Console.WriteLine("The item has been added to your cart");
        }
        public void ViewCart()
        {
            if (listingCartList.Count != 0)
            {
                Console.WriteLine("This is a Display of items in your Cart : ");
                int counter = 1;
                foreach (Listings s in listingCartList)
                {
                    Console.WriteLine("Item number " + counter);
                    s.ViewListing();
                    counter++;
                }
                Console.WriteLine("End of cart");
            }
            else
                Console.WriteLine("Your cart is empty.");
        }
        public void AddToSoldListings()
        {
            Seller s;
            foreach (Listings l in listingCartList) {
                s = Factory.ListOfSellers.Find(q =>q.Email==l.SellerEmail );
                if (s != null)
                {
                    s.listOfSold.Add(l);
                }
            }
        }
        public void CheckOut()
        {
            if (this.listingCartList.Count == 0)
            {
                Console.WriteLine("Your cart is Empty!");
                return;
            }
            double sumOfPrices = 0;
            foreach (Listings s in listingCartList)
            {
                sumOfPrices += s.Price;
            }

            Console.WriteLine("Your cart items total price is : " + sumOfPrices);
            AddToSoldListings();
            listingCartList.Clear();
        }
        public void RemoveFromCart()
        {
            ViewCart();
            if (listingCartList.Count != 0)
            {
                Console.WriteLine("which Item you want to remove from your cart?.. please enter the number.");
                int index = Convert.ToInt32(Console.ReadLine()) - 1;
                listingCartList.Remove(listingCartList[index]);
                Console.WriteLine("Item deleted successfully.");
            }
        }
    }
    [Serializable]
    class Payment
    {
        private string cardNumber;
        private string pinCode;
        private string billingAddress;
        private bool valid;
        public Payment() { cardNumber = ""; pinCode = ""; billingAddress = ""; }
        public bool Valid { get { return valid; } }
        public bool CheckIfValid(string cardNumber, string pinCode, string billingAdress)
        {
            bool flag1 = true;
            bool flag2 = true;
            foreach (char s in cardNumber) { if (Char.IsDigit(s) != true) flag1 = false; }
            foreach (char s in pinCode) { if (Char.IsDigit(s) != true) flag1 = false; }
            if (flag1 == false || flag2 == false)
            {
                Console.WriteLine("CardNumber and pinCode must consist of numbers Only!");
                return false;
            }
            if (cardNumber.Length != 10)
            {
                Console.WriteLine("CardNumber must have a length of 10 digits!");
                return false;
            }
            if (pinCode.Length != 4)
            {
                Console.WriteLine("PinCode must have a length of 4 digits!");
                return false;
            }
            return true;
        }
        public void AddNewPaymentMethod()
        {
            if (valid)
                return;
            Console.WriteLine("Enter your payment credintials : ");
            Console.WriteLine("Enter your CardNumber :");
            string cardNumber = Console.ReadLine();
            Console.WriteLine("Enter your PinCode :");
            string pinCode = Console.ReadLine();
            Console.WriteLine("Enter your BillingAddress :");
            string billingAdress = Console.ReadLine();
            if (CheckIfValid(cardNumber, pinCode, billingAdress))
            {
                this.cardNumber = cardNumber;
                this.pinCode = pinCode;
                this.billingAddress = billingAdress;
                valid = true;
                Console.WriteLine("Your Payment Method Was Added Successfully");
            }
            else
            {
                valid = false;
                Console.WriteLine("Your payment method is not valid");
            }
            Factory.Store();
        }

    }
}