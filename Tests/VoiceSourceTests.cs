using System.IO;
using NUnit.Framework;
using Vox2Vec.Implementation.Python;
using Vox2Vec.Services;

namespace PythonicWayTests
{
    [TestFixture]
    public class VoiceSourceTests
    {
        private IVoicePathSource voiceSource;
        private string homeDir;

        [SetUp]
        public void SetUp()
        {
            this.voiceSource = new VoicePathSource();
            this.homeDir = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
        }

        [Test]
        public void Get_should_download_voice()
        {
            var filepath = Path.Combine(this.homeDir, "00001.wav");
            var voice = this.voiceSource.Get(filepath);
            Assert.AreEqual(133761, voice.Values.Length);
        }
    }
}