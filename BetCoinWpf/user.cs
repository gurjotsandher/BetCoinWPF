using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetCoinWpf
{
    public class User
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string Bank { get; set; }
        public string Iban { get; set; }
        public string Id { get; set; }
        public double Balance { get; set; }
        public double Wallet { get; set; }

        public User()
        {
            this.Username = "Default";
            this.Password = "Default";
            this.Bank = "Default";
            this.Iban = "Default";
            this.Id = "Default";
            this.Balance = 0;
            this.Wallet = 0;
        }

        //public User(double balance, string bank, string iban, string password, string username)
        //{
        //    this.Username = username;
        //    this.Password = password;
        //    this.Bank = bank;
        //    this.Iban = iban;
        //    this.Id = Guid.NewGuid().ToString();
        //    this.Balance = balance;
        //    this.Wallet = 0;

        //}

        public User(double balance, string bank, string iban, string id, string password, string username)
        {
            this.Username = username;
            this.Password = password;
            this.Bank = bank;
            this.Iban = iban;
            this.Id = id;
            this.Balance = balance;
            this.Wallet = 0;

        }

        //public User(string username, string password, string bank, string iban, double balance)
        //{
        //    this.Username = username;
        //    this.Password = password;
        //    this.Bank = bank;
        //    this.Iban = iban;
        //    this.Id = Guid.NewGuid().ToString();
        //    this.Balance = balance;
        //    this.Wallet = 0;
        //}

        public User(string username, string password, string bank, string id, string iban, double balance)
        {
            this.Username = username;
            this.Password = password;
            this.Bank = bank;
            this.Iban = iban;
            this.Id = id;
            this.Balance = balance;
            this.Wallet = 0;
        }


    }
}
