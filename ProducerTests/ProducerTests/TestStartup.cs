using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using PactProducer;
using PactProducer.Models.Services;
using ProducerTests.ProducerTests;

namespace ProducerTests {
    public class TestStartup : Startup {
        protected override void RegisterServices(ContainerBuilder builder) {
            builder.RegisterType<MockResultService>().As<IResultService>();
        }
    }
}
