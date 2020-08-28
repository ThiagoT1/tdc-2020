using System;
using System.Text.Json;

namespace FastBFF.HTTP.Server.Models
{
    public class AccountOrderCollectionClass
    {
        public readonly static AccountOrderCollectionClass SampleInstance;
        public static readonly byte[] SampleBytes;

        static AccountOrderCollectionClass()
        {
            var date = new DateTime(2020, 08, 17);
            
            var id = Guid.NewGuid().ToString();

            var order = new AccountOrderClass() { Account = 3, Created = date, Expiry = date, Id = id, Price = 10.92m, Qty = 2000, Symbol = "FASTAF" };

            SampleInstance = new AccountOrderCollectionClass()
            {
                Account = 3,
                Orders = new AccountOrderClass[]
                    {
                        order, order, order, order,
                        order, order, order, order,
                        order, order, order, order
                    }
            };

            SampleBytes = JsonSerializer.SerializeToUtf8Bytes(SampleInstance);

        }
        public int Account { get; set; }

        public AccountOrderClass[] Orders { get; set; }

    }


}
