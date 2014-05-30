using System.Linq;
using System.Web.Mvc;
using MvcSample.Core.Mvc;
using MvcSample.Core.Serialization;
using MvcSample.Core.Storage;

namespace MvcSample.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IStore _store;
        private readonly ISerializer _jsonSerializer;

        public CustomersController()
        {
            _store = MvcApplication.Store;
            _jsonSerializer = MvcApplication.JsonSerializer;
        }

        [HttpGet]
        public RawJsonResult Index()
        {
            var customers = _store.Query<Models.Customer>().OrderBy(c => c.Id);

            return new RawJsonResult(_jsonSerializer.Serialize(customers));
        }
    }
}