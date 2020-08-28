using System.Threading.Tasks;

namespace FastBFF.HTTP.Server.Repositories
{
    public interface IByteArrayRepository
    {
        ValueTask<byte[]> GetBalance(int accountNumber);
        ValueTask<byte[]> GetOrders(int accountNumber);


    } 
}
