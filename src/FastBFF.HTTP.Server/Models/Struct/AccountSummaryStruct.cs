using Newtonsoft.Json;

namespace FastBFF.HTTP.Server.Models
{

    public readonly struct AccountSummaryStruct
    {

        public readonly int Account;

        public readonly AccountBalanceStruct Balance;

        public readonly AccountOrderCollectionStruct Orders;

        [JsonConstructor]
        public AccountSummaryStruct(int account, AccountBalanceStruct balance, AccountOrderCollectionStruct orders)
        {
            Account = account;
            Balance = balance;
            Orders = orders;
        }
    }

}
