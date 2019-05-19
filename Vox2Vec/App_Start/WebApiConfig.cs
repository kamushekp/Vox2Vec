using System.ComponentModel.Design;
using System.Web.Http;
using Vox2Vec.DistanceProviders;
using Vox2Vec.Implementation;
using Vox2Vec.Implementation.Python;
using Vox2Vec.Services;

namespace Vox2Vec
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );
        }
    }
}