using FastBFF.HTTP.Server.Models;
using FastBFF.HTTP.Server.Repositories;
using Microsoft.AspNetCore.Http;
using System.Buffers;
using System.Text;
using System.Threading.Tasks;

namespace FastBFF.HTTP.Server.Controllers
{
    public static class ActionHelper
    {

        public static async Task<AccountSummaryClass> GetDeserializedClass(
            IJsonRepository jsonNETRepository, int accountNumber)
        {
            var balanceTask = jsonNETRepository.GetBalanceClass(accountNumber);
            var ordersTask = jsonNETRepository.GetOrdersClass(accountNumber);
            

            var balance = await balanceTask;
            var orders = await ordersTask;
            

            var summary = new AccountSummaryClass()
            {
                Account = accountNumber,
                Balance = balance,
                Orders = orders
            };

            return summary;
        }

        public static async Task<AccountSummaryStruct> GetDeserializedStruct(
            IJsonNETRepository jsonNETRepository, int accountNumber)
        {
            var balanceTask = jsonNETRepository.GetBalanceStruct(accountNumber);
            var ordersTask = jsonNETRepository.GetOrdesStruct(accountNumber);

            var balance = await balanceTask;
            var orders = await ordersTask;

            var summary = new AccountSummaryStruct(accountNumber, balance, orders);

            return summary;
        }

        public static async Task WriteToResponseBody(HttpResponse response,
            IByteArrayRepository byteArrayRepository, int accountNumber)
        {
            response.Headers.Add("Content-Type", "application/json");
            response.StatusCode = 200;

            
            var balanceTask =  byteArrayRepository.GetBalance(accountNumber);
            var ordersTask = byteArrayRepository.GetOrders(accountNumber);


            await response.StartAsync();

            var encoder = Encoding.UTF8.GetEncoder();

            var stream = response.Body;

            await stream.WriteAsync(ResponseExtensions.StartObject);

            await stream.WriteProperty(ResponseExtensions.AccountName, accountNumber, encoder);

            await stream.WriteAsync(ResponseExtensions.Comma);

            var balance = await balanceTask;
            await stream.WriteProperty(ResponseExtensions.BalanceName, balance);

            await stream.WriteAsync(ResponseExtensions.Comma);

            var orders = await ordersTask;
            await stream.WriteProperty(ResponseExtensions.OrdersName, orders);


            await stream.WriteAsync(ResponseExtensions.EndObject);
        }

        public static async Task WriteToBodyWiter(HttpResponse response,
            IResponseWriterRepository responseWriterRepository, int accountNumber)
        {
            response.Headers.Add("Content-Type", "application/json");
            response.StatusCode = 200;

            await response.StartAsync();

            var encoder = Encoding.UTF8.GetEncoder();

            var pipeWriter = response.BodyWriter;

            pipeWriter.Write(ResponseExtensions.StartObject.Span);

            pipeWriter.WriteProperty(ResponseExtensions.AccountName, accountNumber, encoder);

            pipeWriter.Write(ResponseExtensions.Comma.Span);
            pipeWriter.Write(ResponseExtensions.BalanceName.Span);
            await responseWriterRepository.WriteBalance(accountNumber, response.BodyWriter);
            

            pipeWriter.Write(ResponseExtensions.Comma.Span);
            pipeWriter.Write(ResponseExtensions.OrdersName.Span);
            await responseWriterRepository.WriteOrders(accountNumber, response.BodyWriter);


            pipeWriter.Write(ResponseExtensions.EndObject.Span);

            await pipeWriter.CompleteAsync();

        }

    }
}
