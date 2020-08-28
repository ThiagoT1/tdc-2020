using System.Text.Json;

namespace FastBFF.HTTP.Server.Models
{
    public class AccountSummaryClass
    {

        public static readonly byte[] SampleBytes;
        public static readonly AccountSummaryClass SampleInstance;

        public int Account { get; set; }

        public AccountBalanceClass Balance { get; set; }

        public AccountOrderCollectionClass Orders { get; set; }

        static AccountSummaryClass()
        {
            SampleInstance = new AccountSummaryClass()
            {
                Account = 3,
                Balance = AccountBalanceClass.SampleInstance,
                Orders = AccountOrderCollectionClass.SampleInstance,
            };

            SampleBytes = JsonSerializer.SerializeToUtf8Bytes(SampleInstance);
        }
    }
}
