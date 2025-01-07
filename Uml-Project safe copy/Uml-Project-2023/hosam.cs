using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using mones;
using ahmad;
using kanan;
namespace hosam
{
    [Serializable]
    class User
    {
        string name;
        string email;
        string phone;
        string password;
        string adress;
        public User() : this("", "", "", "", "") { }
        public User(string name, string email, string phone, string password, string adress)
        {
            Name = name;
            if (IsValidEmail(email))
            {
                Email = email;
                //Console.WriteLine("The email address is valid.");
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("The email address is invalid.");
                    Console.WriteLine("please re-enter your email.");
                    string email1 = Console.ReadLine();
                    Email = email1;
                    if (IsValidEmail(email1))
                        break;
                }
            }

            if (IsValidPhoneNumber( phone))
            {
                Phone = phone;
                //Console.WriteLine("The phone number is valid.");
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("The phone number is invalid.");
                    Console.WriteLine("please re-enter your phone number.");
                    phone = Console.ReadLine();
                    Phone = phone;
                    if (IsValidPhoneNumber(phone))
                        break;
                }
            }

            //Userid = userid;
            if (IsValidPassword(password))
            {
                Password = password;
                Console.WriteLine("account created successfully");
            }
            else
            {

                while (true)
                {
                    Console.WriteLine("The password is invalid. ");
                    Console.WriteLine("your password should meets the following requirements:");
                    Console.WriteLine("  It must contain at least 8 character");
                    Console.WriteLine("  It must contain at least one lowercase letter.");
                    Console.WriteLine("  It must contain at least one uppercase letter." );
                    Console.WriteLine("  It must contain at least one number.");
                    Console.WriteLine("please re-enter your password number.");
                    string pw = Console.ReadLine();
                    Password = pw;
                    if (IsValidPassword(pw))
                    {
                        Console.WriteLine("aacount created sucssesfully");
                        break;
                    }

                }
            }
            Address = adress;
        }

        static bool IsValidEmail(string email)
        {
            if (email.Length == 0)
            {
                return false;
            }
            if (email.Length < 5)
            {
                return false;
            }
            if (email.IndexOf('@') == -1)
            {
                return false;
            }
            string firstPart = email.Substring(0, email.IndexOf('@'));
            string secondPart = email.Substring(email.IndexOf('@') + 1); 
            if (firstPart.Length == 0)
            {
                return false;
            }
            if (secondPart.Length == 0)
            {
                return false;
            }
            if (secondPart.IndexOf('.') == -1)
            {
                return false;
            }
            if (secondPart.IndexOf('.') >= secondPart.Length - 2)
            {
                return false;
            }
            return true;
        }
        public static bool IsValidPassword(string password)
        {
            // Check the length of the password
            if (password.Length < 8)
            {
                return false;
            }

            // Initialize variables to track the number of digit, lowercase, and uppercase characters
            int digitCount = 0;
            int lowerCount = 0;
            int upperCount = 0;
            // Check each character in the password
            foreach (char c in password)
            {
                // Increment the appropriate count for the character's type
                if (char.IsDigit(c))
                {
                    digitCount++;
                }
                else if (char.IsLower(c))
                {
                    lowerCount++;
                }
                else if (char.IsUpper(c))
                {
                    upperCount++;
                }
            }
            // Check if the password has at least one digit, one lowercase character, and one uppercase character
            if (digitCount > 0 && lowerCount > 0 && upperCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool IsValidPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length != 10)
            {
                return false;
            }
            else
            {
                foreach (char c in phoneNumber)
                {
                    if (!Char.IsNumber(c))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public bool VerifyPassword(string password)
        {
            if (Password == password)
                return true;
            return false;
        }
        public void ChangeAccountInfo(string password)
        {
            Password = password;
        }
        public virtual void print()
        {
            Console.WriteLine("your name is :" + name);
            Console.WriteLine("your email is :" + email);
            Console.WriteLine("your phone is :" + phone);
            //Console.WriteLine("your userid is :" + userid);deleted
            Console.WriteLine("your password is :" + password);
            Console.WriteLine("your adress is :" + adress);
        }
        public string Name { get { return name; } set { name = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Phone { get { return phone; } set { phone = value; } }
        //public string Userid { get { return userid; } set { userid = value; } }deleted
        public string Password { get { return password; } set { password = value; } }
        public string Address { get { return adress; } set { adress = value; } }
    }
}

