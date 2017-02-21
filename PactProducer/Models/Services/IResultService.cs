using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PactProducer.Models.Services {
    public interface IResultService {
        ProducerModel GetProduct(int id);
        ProducerModel CreateProduct(Parameter type);
    }
}
