using Newtonsoft.Json;

namespace FastBFF.HTTP.Server.Models
{
    public readonly struct AccountOrderCollectionStruct
    {

        public readonly int Account;

        public readonly AccountOrderStruct[] Orders;

        [JsonConstructor]
        public AccountOrderCollectionStruct(int account, AccountOrderStruct[] orders)
        {
            Account = account;
            Orders = orders;
        }
    }


}
