using Vox2Vec.Models;
using Vox2Vec.Services;

namespace Vox2Vec.Implementation.Python
{
    public class VoicePipeline:IVoicePipeline
    {
        private readonly IEmbeddingExtractor embeddingExtractor;
        private readonly IVoicePreprocessor voicePreprocessor;
        private readonly IVoicePathSource voicePathSource;
        public VoicePipeline(IEmbeddingExtractor embeddingExtractor, IVoicePreprocessor voicePreprocessor, IVoicePathSource voicePathSource)
        {
            this.embeddingExtractor = embeddingExtractor;
            this.voicePreprocessor = voicePreprocessor;
            this.voicePathSource = voicePathSource;
        }
        public Embedding Extract(string filename)
        {
            var rowVoice = this.voicePathSource.Get(filename);
            var preprocessedVoice = this.voicePreprocessor.PreprocessVoice(rowVoice);
            var embedding = this.embeddingExtractor.Extract(preprocessedVoice);
            return embedding;
        }
    }
}