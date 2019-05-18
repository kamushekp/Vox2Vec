using System.Linq;
using NUnit.Framework;
using Vox2Vec.Implementation.Python;
using Vox2Vec.Models;
using Vox2Vec.Services;

namespace PythonicWayTests
{
    [TestFixture]
    public class VoicePreprocessorTests
    {
        [SetUp]
        public void SetUp()
        {
            voicePreprocessor = new VoicePreprocessor();
            VoiceValues = Enumerable.Range(0, 100000)
                .Select(_ => TestContext.CurrentContext.Random.NextDouble(-0.01, 0.01))
                .ToArray();
        }

        private IVoicePreprocessor voicePreprocessor;
        private double[] VoiceValues;

        [Test]
        public void PreprocessVoice_should_preprocess_voice()
        {
            var voice = new Voice {Values = VoiceValues};
            var feature = this.voicePreprocessor.PreprocessVoice(voice);

            Assert.AreEqual(1, feature.Values.Length);
            Assert.AreEqual(512, feature.Values[0].Length);
            Assert.AreEqual(300, feature.Values[0][0].Length);
            Assert.AreEqual(1, feature.Values[0][0][0].Length);
        }
    }
}