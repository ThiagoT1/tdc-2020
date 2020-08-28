using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastBFF.HTTP.Server.Models;
using FastBFF.HTTP.Server.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FastBFF.HTTP.Server.Controllers
{
    [ApiController]
    public partial class AccountSummaryController : ControllerBase
    {

        private readonly IJsonNETRepository _jsonNETRepository;
        private readonly ISystemTextJsonRepository _systemTextJsonRepository;

        private readonly IByteArrayRepository _byteArrayRepository;
        private readonly IResponseWriterRepository _responseWriterRepository;


        public AccountSummaryController(
            IJsonNETRepository jsonNETRepository,
            ISystemTextJsonRepository systemTextJsonRepository,
            IByteArrayRepository byteArrayRepository,
            IResponseWriterRepository responseWriterRepository)
        {

            _jsonNETRepository = jsonNETRepository;

            _systemTextJsonRepository = systemTextJsonRepository;



            _byteArrayRepository = byteArrayRepository;

            _responseWriterRepository = responseWriterRepository;

        }

        [HttpGet]
        [Route("account-summary/json-net/class/{account:int}")]
        public Task<AccountSummaryClass> GetJsonNetClass(int account)
        {
            return ActionHelper.GetDeserializedClass(_jsonNETRepository, account);
        }

        [HttpGet]
        [Route("account-summary/system-text-json/class/{account:int}")]
        public Task<AccountSummaryClass> GetSystemTextJsonClass(int account)
        {
            return ActionHelper.GetDeserializedClass(_systemTextJsonRepository, account);
        }

        [HttpGet]
        [Route("account-summary/json-net/struct/{account:int}")]
        public Task<AccountSummaryStruct> GetJsonNetStruct(int account)
        {
            return ActionHelper.GetDeserializedStruct(_jsonNETRepository, account);
        }

        [HttpGet]
        [Route("account-summary/write-response-body/{account:int}")]
        public Task WriteToResponseBody(int account)
        {
            return ActionHelper.WriteToResponseBody(Response, _byteArrayRepository, account);
        }

        [HttpGet]
        [Route("account-summary/write-body-writer/{account:int}")]
        public Task WriteToBodyWriter(int account)
        {
            return ActionHelper.WriteToBodyWiter(Response, _responseWriterRepository, account);
        }
    }
}
