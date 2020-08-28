using System;

namespace FastBFF.HTTP.Server.Models
{
    public class AccountOrderClass
    {
        public int Account { get; set; }
        public string Symbol { get; set; }

        public DateTime Created { get; set; }

        public string Id { get; set; }


        public decimal Price { get; set; }

        public decimal Qty { get; set; }

        public decimal ExecQty { get; set; }


        public DateTime Expiry { get; set; }

    }


}
