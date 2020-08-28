using FastBFF.HTTP.Server.Models;
using System.Text.Json;
using System.Threading.Tasks;

namespace FastBFF.HTTP.Server.Repositories
{

    public class SystemTextJsonRepository : ISystemTextJsonRepository
    {
        IByteArrayRepository _byteArrayRepository;

        public SystemTextJsonRepository(IByteArrayRepository byteArrayRepository)
        {
            _byteArrayRepository = byteArrayRepository;
        }

        public async ValueTask<AccountBalanceClass> GetBalanceClass(int accountNumber)
        {
            
            var bytes = await _byteArrayRepository.GetBalance(accountNumber);

            return JsonSerializer.Deserialize<AccountBalanceClass>(bytes);

        }

        public async ValueTask<AccountOrderCollectionClass> GetOrdersClass(int accountNumber)
        {
            
            var bytes = await _byteArrayRepository.GetOrders(accountNumber);

            return JsonSerializer.Deserialize<AccountOrderCollectionClass>(bytes);

        }


    }
}
