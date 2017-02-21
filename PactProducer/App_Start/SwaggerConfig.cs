using System.Web.Http;
using PactProducer;
using Swashbuckle.Application;

namespace PactProducer {
    public class SwaggerConfig {
        public static void Register(HttpConfiguration configuration) {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            configuration
                .EnableSwagger(c => {
                    c.SingleApiVersion("v1", "PactProducer");
                    c.IncludeXmlComments(GetXmlCommentsPath());
                    c.UseFullTypeNameInSchemaIds();
                })
                .EnableSwaggerUi(c => c.DisableValidator());
        }

        private static string GetXmlCommentsPath() {
            return $@"{System.AppDomain.CurrentDomain.BaseDirectory}bin\PactProducer.XML";
        }
    }
}
