using FastBFF.HTTP.Server;
using FastBFF.HTTP.Server.Controllers;
using FastBFF.HTTP.Server.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FastBFF.HTTP.Client
{


    public class FastBFFClient
    {
        public static string GetJsonNetClassURL(int accountNumber)
        {
            return $"account-summary/json-net/class/{accountNumber}";
        }

        public static string GetSystemTextJsonClassURL(int accountNumber)
        {
            return $"account-summary/system-text-json/class/{accountNumber}";
        }

        public static string GetJsonNetStructURL(int accountNumber)
        {
            return $"account-summary/json-net/struct/{accountNumber}";
        }

        public static string WriteToResponseBodyURL(int accountNumber)
        {
            return $"account-summary/write-response-body/{accountNumber}";
        }

        public static string WriteToBodyWriterURL(int accountNumber)
        {
            return $"account-summary/write-body-writer/{accountNumber}";
        }


        private readonly string _baseUrl;
        private readonly int _accountNumber;
        public FastBFFClient(string baseUrl, int accountNumber)
        {
            _baseUrl = baseUrl;
            _accountNumber = accountNumber;
            
            _invoker = new HttpMessageInvoker(new SocketsHttpHandler() { UseCookies = false, UseProxy = false, AllowAutoRedirect = false });

            _getJsonNetClass = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_baseUrl}/{GetJsonNetClassURL(_accountNumber)}")) { Version = HttpVersion.Version20 };
            _getSystemTextJsonClass = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_baseUrl}/{GetSystemTextJsonClassURL(_accountNumber)}")) { Version = HttpVersion.Version20 };

            _getJsonNetStruct = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_baseUrl}/{GetJsonNetStructURL(_accountNumber)}")) { Version = HttpVersion.Version20 };

            _writeToResponseBody = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_baseUrl}/{WriteToResponseBodyURL(_accountNumber)}")) { Version = HttpVersion.Version20 };
            _writeToBodyWriter = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_baseUrl}/{WriteToBodyWriterURL(_accountNumber)}")) { Version = HttpVersion.Version20 };
            
            _byteArrayRepository = new ByteArrayRepository();
            _responseWriterRepository = new ResponseWriterRepository();

            _jsonNetRepository = new JsonNETRepository(_byteArrayRepository);
            _systemTextJsonRepository = new SystemTextJsonRepository(_byteArrayRepository);


        }

        private readonly HttpMessageInvoker _invoker;

        private readonly HttpRequestMessage _getJsonNetClass;
        private readonly HttpRequestMessage _getSystemTextJsonClass;
        
        private readonly HttpRequestMessage _getJsonNetStruct;

        private readonly HttpRequestMessage _writeToResponseBody;
        private readonly HttpRequestMessage _writeToBodyWriter;


        private readonly JsonNETRepository _jsonNetRepository;
        private readonly SystemTextJsonRepository _systemTextJsonRepository;

        private readonly ByteArrayRepository _byteArrayRepository;
        private readonly ResponseWriterRepository _responseWriterRepository;

        public const int ConcurrentTaskCount = 200;

        public const int RequestCountPerCount = 500;

        //public const int OperationsPerInvoke = ConcurrentTaskCount * RequestCountPerCount;
        public const int OperationsPerInvoke = 1;

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




        private async Task MakeRequest(HttpRequestMessage request)
        {
            await Task.WhenAll(Enumerable.Range(0, ConcurrentTaskCount).Select(async _ =>
            {
                for (int i = 0; i < RequestCountPerCount; i++)
                {
                    using (HttpResponseMessage r = await _invoker.SendAsync(request, default))
                    {
                        r.EnsureSuccessStatusCode();
                        using (Stream s = await r.Content.ReadAsStreamAsync())
                            await s.CopyToAsync(Stream.Null);
                    }
                }
            }));
        }

        private Task RawMakeRequest(Func<Task> task) => Task.WhenAll(Enumerable.Range(0, ConcurrentTaskCount).Select(async _ =>
        {
            for (int i = 0; i < RequestCountPerCount; i++)
            {
                await task();
            }
        }));


        public Task GetJsonNetClass() => MakeRequest(_getJsonNetClass);
        public Task GetSystemTextJsonClass() => MakeRequest(_getSystemTextJsonClass);

        public Task GetJsonNetStruct() => MakeRequest(_getJsonNetStruct);

        public Task WriteToResponseBody() => MakeRequest(_writeToResponseBody);
        public Task WriteToBodyWriter() => MakeRequest(_writeToBodyWriter);



        public Task RawGetJsonNetClass() => RawMakeRequest(() => ActionHelper.GetDeserializedClass(_jsonNetRepository, _accountNumber));
        public Task RawGetSystemTextJsonClass() => RawMakeRequest(() => ActionHelper.GetDeserializedClass(_systemTextJsonRepository, _accountNumber));

        public Task RawGetJsonNetStruct() => RawMakeRequest(() => ActionHelper.GetDeserializedStruct(_jsonNetRepository, _accountNumber));


        public Task RawWriteToResponseBody() => RawMakeRequest(async () =>
        {
            var response = new DefaultHttpContext().Response;
            await ActionHelper.WriteToResponseBody(response, _byteArrayRepository, _accountNumber);
            var pipeReader = PipeReader.Create(response.Body);
            while (pipeReader.TryRead(out var _)) ;
        });

        public Task RawWriteToBodyWriter() => RawMakeRequest(async () =>
        {
            var response = new DefaultHttpContext().Response;
            await ActionHelper.WriteToBodyWiter(response, _responseWriterRepository, _accountNumber);
            var pipeReader = PipeReader.Create(response.Body);
            while (pipeReader.TryRead(out var _)) ;
        });

    }

}
