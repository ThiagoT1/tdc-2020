using FastBFF.HTTP.Server.Models;
using System;
using System.IO.Pipelines;
using System.Threading.Tasks;

namespace FastBFF.HTTP.Server.Repositories
{
    public class ResponseWriterRepository : IResponseWriterRepository
    {
        public async ValueTask WriteBalance(int accountNumber, PipeWriter pipeWriter)
        {
            await Task.Yield();

            var bytes = AccountBalanceClass.SampleBytes;

            WriteBytesToResponse(pipeWriter, bytes);
        }

        public async ValueTask WriteOrders(int accountNumber, PipeWriter pipeWriter)
        {
            await Task.Yield();

            var bytes = AccountOrderCollectionClass.SampleBytes;

            WriteBytesToResponse(pipeWriter, bytes);
        }

        private void WriteBytesToResponse(PipeWriter pipeWriter, byte[] bytes)
        {
            var memory = pipeWriter.GetMemory(bytes.Length);
            bytes.CopyTo(memory);

            pipeWriter.Advance(bytes.Length);
        }
    }
}
