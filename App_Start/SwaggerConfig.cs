using System.Web.Http;
using WebActivatorEx;
using WebAPIProjectDemo;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebAPIProjectDemo
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>                 {                        
                    c.SingleApiVersion("v1", "WebAPIProjectDemo");
                    c.IncludeXmlComments(string.Format(@"{0}\bin\WebAPIProjectDemo.xml",
                        System.AppDomain.CurrentDomain.BaseDirectory));
                    })
                .EnableSwaggerUi();
        }
    }
}
