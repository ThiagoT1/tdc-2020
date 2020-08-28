using Newtonsoft.Json;
using System;

namespace FastBFF.HTTP.Server.Models
{
    public readonly struct AccountOrderStruct
    {
        public readonly int Account;
        public readonly string Symbol;

        public readonly DateTime Created;

        public readonly string Id;


        public readonly double Price;

        public readonly int Qty;

        public readonly DateTime Expiry;

        [JsonConstructor]
        public AccountOrderStruct(int account, string symbol, DateTime created, string id, double price, int qty, DateTime expiry)
        {
            Account = account;
            Symbol = symbol;
            Created = created;
            Id = id;
            Price = price;
            Qty = qty;
            Expiry = expiry;
        }
    }


}
