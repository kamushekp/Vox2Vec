using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Vox2Vec.DistanceProviders;
using Vox2Vec.Implementation;
using Vox2Vec.Implementation.Python;
using Vox2Vec.Services;

namespace Vox2Vec
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterSingleton<IDistanceProvider, CosineDistanceProvider>();
            container.RegisterSingleton<IResourcePaths, ResourcePaths>();
            container.RegisterType<IEmbeddingExtractor, EmbeddingExtractor>();
            container.RegisterSingleton<IVoicePreprocessor, VoicePreprocessor>();
            container.RegisterSingleton<IVoicePathSource, VoicePathSource>();
            container.RegisterType<IVoicePipeline, VoicePipeline>();
            container.RegisterSingleton<IFeatureRepository, InMemoryFeatureRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}