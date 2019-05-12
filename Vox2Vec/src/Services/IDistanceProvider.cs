using Vox2Vec.Models;

namespace Vox2Vec.Services
{
    public interface IDistanceProvider
    {
        double Measure(Embedding v1, Embedding v2);
    }
}