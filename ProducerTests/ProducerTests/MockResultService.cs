using System;
using PactProducer.Models;
using PactProducer.Models.Services;

namespace ProducerTests.ProducerTests {
    public class MockResultService : IResultService {
        public ProducerModel GetProduct(int id) {
            if (id == 8) return null;
            return new ProducerModel {
                Time = new DateTime(1984, 01, 01),
                Type = ProducerType.TypEtt,
                Id = id
            };
        }

        public ProducerModel CreateProduct(Parameter type) {
            ProducerType producerType;
            if (Enum.TryParse(type.Type, out producerType))
                return new ProducerModel {
                    Time = new DateTime(1984, 01, 01),
                    Type = producerType,
                    Id = 10
                };
            throw new ArgumentException();
        }
    }
}