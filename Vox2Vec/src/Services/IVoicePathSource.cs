using Vox2Vec.Models;

namespace Vox2Vec.Services
{
    public interface IVoicePathSource
    {
        Voice Get(string filename);
    }
}