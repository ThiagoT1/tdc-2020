using System;
using System.Text.Json;

namespace FastBFF.HTTP.Server.Models
{
    public class AccountBalanceClass
    {
        public readonly static AccountBalanceClass SampleInstance;
        public readonly static byte[] SampleBytes;
        public readonly static int MaxJsonLength;

        static AccountBalanceClass()
        {
            var date = new DateTime(2020, 08, 17);
            SampleInstance = new AccountBalanceClass()
            {
                Account = 3,
                AvailableBalance = 300909,
                AvailableOptions = 300909,
                AvailableStock = 300909,
                AvailableWithdrawMoney = 300909,
                Balance = 300909,
                BalanceD1 = 9,
                BalanceD2 = 900,
                BalanceD3 = 300000,
                BlockedBalance = 1,
                D1 = date.AddDays(1),
                D2 = date.AddDays(2),
                DayBalanceMovement = 1000,
                ExecutedBuy = 800,
                ExecutedSell = 800,
                InitialBalance = 300,
                InitialMargin = 800,
                Margin = 800,
                PendingBuy = 1000,
                PendingSell = 1000,
                RequiredMargin = 800
            };

            SampleBytes = JsonSerializer.SerializeToUtf8Bytes(SampleInstance);
            MaxJsonLength = SampleBytes.Length;
        }

        public int Account { get; set; }

        public decimal Margin { get; set; }
        public decimal Balance { get; set; }
        public decimal InitialBalance { get; set; }
        public decimal DayBalanceMovement { get; set; }
        public DateTime D1 { get; set; }
        public DateTime D2 { get; set; }
        public decimal InitialMargin { get; set; }
        public decimal RequiredMargin { get; set; }
        public decimal BalanceD1 { get; set; }
        public decimal BalanceD2 { get; set; }
        public decimal BalanceD3 { get; set; }
        public decimal ExecutedBuy { get; set; }
        public decimal PendingBuy { get; set; }
        public decimal ExecutedSell { get; set; }
        public decimal PendingSell { get; set; }
        public decimal AvailableOptions { get; set; }
        public decimal AvailableWithdrawMoney { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal AvailableStock { get; set; }
        public decimal TotalNonExecutedBuys { get; set; }
        public decimal TotalNonExecutedSells { get; set; }
        public decimal BlockedBalance { get; set; }
        public decimal TradeCost { get; set; }


    }


}
