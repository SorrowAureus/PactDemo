using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject.Model;

namespace UnitTestProject.Consumer1Tests {
    public class Pact1Consumer : PactConsumer {
        public Pact1Consumer() : base("Pact1Consumer", "PactProducer") { }
    }
}
