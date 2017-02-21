using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using Autofac;
using JetBrains.Annotations;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Owin;
using Autofac.Integration.WebApi;

[assembly: OwinStartup(typeof(PactProducer.Startup))]
namespace PactProducer {
    public class Startup {
        [UsedImplicitly]
        public void Configuration(IAppBuilder app) {
            var configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
            SwaggerConfig.Register(configuration);
            configuration.Formatters.Clear();
            configuration.Formatters.Add(new JsonMediaTypeFormatter {
                SerializerSettings = new JsonSerializerSettings {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented,
                    Converters = new List<JsonConverter> {
                        new StringEnumConverter()
                    }
                }
            });
            
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            RegisterServices(builder);


            var container = builder.Build();
               
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(configuration);
            app.UseWebApi(configuration);
        }

        protected virtual void RegisterServices(ContainerBuilder builder) {
            //todo service here
        }
    }
}
