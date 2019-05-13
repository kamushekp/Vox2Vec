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

            //var container = new UnityContainer();
            //container.AddService(typeof(IEmbeddingExtractor), new EmbeddingExtractor());
            //container.AddService(typeof(IVoicePreprocessor), new VoicePreprocessor());
            //container.AddService(typeof(IVoicePathSource), new VoicePathSource());
            //container.RegisterType<IEmbeddingExtractor, EmbeddingExtractor>();
            //container.RegisterType<IVoicePreprocessor, VoicePreprocessor>();
            //container.RegisterType<IVoicePathSource, VoicePathSource>();
            //container.RegisterType<IVoicePipeline, VoicePipeline>();
            //container.RegisterType<IFeatureRepository, InMemoryFeatureRepository>();
            //container.RegisterType<IDistanceProvider, CosineDistanceProvider>();
            //config.DependencyResolver = new UnityResolver(container);
            //container.Register<IEmbeddingExtractor, EmbeddingExtractor>();
            //container.Register<IVoicePreprocessor, VoicePreprocessor>();
            //container.Register<IVoicePathSource, VoicePathSource>();
            //container.Register<IVoicePipeline, VoicePipeline>();
            //container.Register<IFeatureRepository, InMemoryFeatureRepository>();
            //container.Register<IDistanceProvider, CosineDistanceProvider>();

            //container.EnableMvc();
        }
    }
}