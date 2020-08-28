using System.IO.Pipelines;
using System.Threading.Tasks;

namespace FastBFF.HTTP.Server.Repositories
{
    public interface IResponseWriterRepository
    {
        ValueTask WriteBalance(int accountNumber, PipeWriter pipeWriter);
        ValueTask WriteOrders(int accountNumber, PipeWriter pipeWriter);

    } 
}
