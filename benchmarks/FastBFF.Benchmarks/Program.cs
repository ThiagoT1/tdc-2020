using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using FastBFF.HTTP.Client;
using FastBFF.HTTP.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FastBFF.Benchmarks
{


    [MemoryDiagnoser]
    public class FastBFFBenchmarks
    {
        private readonly FastBFFClient _client;
        private IHost _host;

        public FastBFFBenchmarks()
        {
            _client = new FastBFFClient("http://localhost:5000", 3);
        }



        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.ConfigureLogging(config =>
                    {
                        config.ClearProviders();
                    });
                    webBuilder.UseStartup<Startup>();
                });



        [Benchmark(Baseline = true, OperationsPerInvoke = FastBFFClient.OperationsPerInvoke)]
        public Task GetJsonNetClass() => _client.GetJsonNetClass();

        [Benchmark(OperationsPerInvoke = FastBFFClient.OperationsPerInvoke)]
        public Task GetSystemTextJsonClass() => _client.GetSystemTextJsonClass();




        [Benchmark(OperationsPerInvoke = FastBFFClient.OperationsPerInvoke)]
        public Task GetJsonNetStruct() => _client.GetJsonNetStruct();







        [Benchmark(OperationsPerInvoke = FastBFFClient.OperationsPerInvoke)]
        public Task WriteToResponseBody() => _client.WriteToResponseBody();

        [Benchmark(OperationsPerInvoke = FastBFFClient.OperationsPerInvoke)]
        public Task WriteToBodyWriter() => _client.WriteToBodyWriter();



        [Benchmark(OperationsPerInvoke = FastBFFClient.OperationsPerInvoke)]
        public Task RawGetJsonNetClass() => _client.RawGetJsonNetClass();        

        [Benchmark(OperationsPerInvoke = FastBFFClient.OperationsPerInvoke)]
        public Task RawGetSystemTextJsonClass() => _client.RawGetSystemTextJsonClass();


        [Benchmark(OperationsPerInvoke = FastBFFClient.OperationsPerInvoke)]
        public Task RawGetJsonNetStruct() => _client.RawGetJsonNetStruct();


        [Benchmark(OperationsPerInvoke = FastBFFClient.OperationsPerInvoke)]
        public Task RawWriteToResponseBody() => _client.RawWriteToResponseBody();

        [Benchmark(OperationsPerInvoke = FastBFFClient.OperationsPerInvoke)]
        public Task RawWriteToBodyWriter() => _client.RawWriteToBodyWriter();


        [GlobalSetup]
        public void IterationSetup()
        {
            _host = CreateHostBuilder(new string[0]).Build();
            Task.Run(async () => await _host.StartAsync()).Wait();
        }

        [GlobalCleanup]
        public void IterationCleanup()
        {
            Task.Run(async () => await _host.StopAsync()).Wait();
            _host.Dispose();
        }

    }

    public class Program
    {
        public static async Task Main(string[] args)
        {

            await Task.Delay(1);


#if DEBUG

            //Debug Only
            var bench = new FastBFFBenchmarks();

            bench.IterationSetup();

            await bench.GetJsonNetClass();
            await bench.GetJsonNetStruct();
            await bench.GetSystemTextJsonClass();

            await bench.WriteToResponseBody();

            await bench.WriteToBodyWriter();

            bench.IterationCleanup();


#else
            BenchmarkSwitcher.FromAssemblies(new[] { typeof(Program).Assembly }).Run(args);
#endif



        }
    }

}

