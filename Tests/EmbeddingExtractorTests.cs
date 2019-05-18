using System.Linq;
using NUnit.Framework;
using PythonicWayTests.Helpers;
using Vox2Vec.Implementation.Python;
using Vox2Vec.Models;
using Vox2Vec.Services;

namespace PythonicWayTests
{
    [TestFixture]
    public class EmbeddingExtractorTests
    {
        [SetUp]
        public void SetUp()
        {
            this.embeddingExtractor = new EmbeddingExtractor();
        }

        private IEmbeddingExtractor embeddingExtractor;

        [Test]
        public void Extract_should_extract_voice()
        {
            var values = new[]
            {
                TestsExtensions.RandomArray(512).Select(array =>
                    TestsExtensions.RandomArray(300).Select(value => new[] {value}).ToArray()).ToArray()
            };

            var vox = new Vox {Values = values};

            var embedding = this.embeddingExtractor.Extract(vox);
            Assert.AreEqual(128, embedding.Length);
            Assert.AreEqual(128, embedding.Values.Length);
        }
    }
}