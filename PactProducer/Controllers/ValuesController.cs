using System.Web.Http;
using PactProducer.Models;
using PactProducer.Models.Services;

namespace PactProducer.Controllers {
    /// <summary>
    /// All the values
    /// </summary>
    [NullObjectActionFilter]
    public class ValuesController : ApiController {
        private readonly IResultService _service;

        public ValuesController(IResultService service) {
            _service = service;
        }

        /// <summary>
        /// Test endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ProducerModel Product(int id) {
            return _service.GetProduct(id);

        }

        [HttpPost]
        public IHttpActionResult CreateProduct([FromBody]Parameter type) {
            return Created("", _service.CreateProduct(type));
        }
    }
}
