using System.Linq;
using Accord.Math;
using Vox2Vec.Models;
using Vox2Vec.Services;

namespace Vox2Vec.DistanceProviders
{
    public class CosineDistanceProvider:IDistanceProvider
    {
        public double Measure(Embedding v1, Embedding v2)
        {
            double[] Convert(Embedding embedding) => embedding.Values.Select(elem => (double)elem).ToArray();
            return Distance.Cosine(Convert(v1), Convert(v1));
        }
    }
}