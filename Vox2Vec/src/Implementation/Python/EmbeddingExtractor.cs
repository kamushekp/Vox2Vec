using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;
using TensorFlow;
using Vox2Vec.Models;
using Vox2Vec.Services;

namespace Vox2Vec.Implementation.Python
{
    public class EmbeddingExtractor : IEmbeddingExtractor, IDisposable
    {
        private readonly string inputLayerName;
        private readonly string outputLayerName;
        private readonly TFSession.Runner runner;
        private readonly TFGraph tfGraph;

        public EmbeddingExtractor()
        {
            this.tfGraph = new TFGraph();
            var pathToModel = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "bin", "src", "Implementation",
                "Python", "base_model.pb");
            var model = File.ReadAllBytes(pathToModel);
            this.tfGraph.Import(model);
            var session = new TFSession(this.tfGraph);
            this.runner = session.GetRunner();

            var layerNames = this.tfGraph.GetEnumerator().Select(x => x.Name).ToArray();
            this.inputLayerName = layerNames[0];
            this.outputLayerName = layerNames[layerNames.Length - 1];
        }

        public void Dispose()
        {
            this.tfGraph.Dispose();
        }

        public Embedding Extract(Vox vox)
        {
            var voiceTensor = new TFTensor(vox.Values);

            this.runner.AddInput(this.tfGraph[this.inputLayerName][0], voiceTensor);
            this.runner.Fetch(this.tfGraph[this.outputLayerName][0]);

            var output = this.runner.Run();
            var result = output[0];
            var embedding = ((float[][]) result.GetValue(true))[0];
            return new Embedding() {Length = embedding.Length, Values = embedding};
        }
    }
}