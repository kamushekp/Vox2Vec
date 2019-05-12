using Vox2Vec.Models;

namespace Vox2Vec.Services
{
    public interface IVoicePipeline
    {
        Embedding Extract(string filename);
    }
}