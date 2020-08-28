using Newtonsoft.Json;
using System;

namespace FastBFF.HTTP.Server.Models
{
    public readonly struct AccountBalanceStruct
    {
        public readonly int Account;

        public readonly DateTime Created;


        public readonly decimal Open;
        public readonly decimal Credit;

        public readonly decimal Debit;

        public readonly decimal Blocked;
        public readonly decimal Available;

        [JsonConstructor]
        public AccountBalanceStruct(int account, DateTime created, decimal open, decimal credit, decimal debit, decimal blocked, decimal available)
        {
            Account = account;
            Created = created;
            Open = open;
            Credit = credit;
            Debit = debit;
            Blocked = blocked;
            Available = available;
        }
    }


}
