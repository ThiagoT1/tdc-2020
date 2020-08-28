using FastBFF.HTTP.Server.Models;
using System.Threading.Tasks;

namespace FastBFF.HTTP.Server.Repositories
{
    public interface IJsonNETRepository : IJsonRepository
    {
        ValueTask<AccountBalanceStruct> GetBalanceStruct(int accountNumber);
        ValueTask<AccountOrderCollectionStruct> GetOrdesStruct(int accountNumber);
    }
}
