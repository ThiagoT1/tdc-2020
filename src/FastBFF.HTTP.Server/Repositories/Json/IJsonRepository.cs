using FastBFF.HTTP.Server.Models;
using System.Threading.Tasks;

namespace FastBFF.HTTP.Server.Repositories
{
    public interface IJsonRepository 
    {
        ValueTask<AccountBalanceClass> GetBalanceClass(int accountNumber);
        ValueTask<AccountOrderCollectionClass> GetOrdersClass(int accountNumber);

        
    }
}
