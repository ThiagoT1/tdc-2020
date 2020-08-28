using FastBFF.HTTP.Server.Models;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace FastBFF.HTTP.Server.Repositories
{


    public class JsonNETRepository : IJsonNETRepository
    {

        IByteArrayRepository _byteArrayRepository;

        public JsonNETRepository(IByteArrayRepository byteArrayRepository)
        {
            _byteArrayRepository = byteArrayRepository;
        }

        public async ValueTask<AccountBalanceClass> GetBalanceClass(int accountNumber)
        {
            var bytes = await _byteArrayRepository.GetBalance(accountNumber);

            var json = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject<AccountBalanceClass>(json);
        }

        public async ValueTask<AccountOrderCollectionClass> GetOrdersClass(int accountNumber)
        {
            var bytes = await _byteArrayRepository.GetOrders(accountNumber);

            var json = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject<AccountOrderCollectionClass>(json);
        }


        public async ValueTask<AccountBalanceStruct> GetBalanceStruct(int accountNumber)
        {
            var bytes = await _byteArrayRepository.GetBalance(accountNumber);

            var json = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject<AccountBalanceStruct>(json);
        }

        public async ValueTask<AccountOrderCollectionStruct> GetOrdesStruct(int accountNumber)
        {
            var bytes = await _byteArrayRepository.GetOrders(accountNumber);

            var json = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject<AccountOrderCollectionStruct>(json);
        }


    }
}
