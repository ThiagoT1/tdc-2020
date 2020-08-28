using FastBFF.HTTP.Server.Models;
using System.Threading.Tasks;

namespace FastBFF.HTTP.Server.Repositories
{
    public class ByteArrayRepository : IByteArrayRepository
    {
        
        public async ValueTask<byte[]> GetBalance(int accountNumber)
        {
            await Task.Yield();

            var bytes = AccountBalanceClass.SampleBytes;

            return GetBytesToResponse(bytes);
        }

        public async ValueTask<byte[]> GetOrders(int accountNumber)
        {
            await Task.Yield();

            var bytes = AccountOrderCollectionClass.SampleBytes;

            return GetBytesToResponse(bytes);
        }

        private byte[] GetBytesToResponse(byte[] bytes)
        {
            var resultBytes = new byte[bytes.Length];

            bytes.CopyTo(resultBytes, 0);

            return resultBytes;
        }


    }
}
