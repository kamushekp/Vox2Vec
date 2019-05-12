using Vox2Vec.Models;

namespace Vox2Vec.Services
{
    public interface IEmbeddingExtractor
    {
        Embedding Extract(Vox vox);
    }
}