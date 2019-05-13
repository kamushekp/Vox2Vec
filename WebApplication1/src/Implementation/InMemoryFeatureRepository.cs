using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vox2Vec.Models;
using Vox2Vec.Services;

namespace Vox2Vec.Implementation
{
    public class InMemoryFeatureRepository: IFeatureRepository
    {
        private class StorageModel
        {
            public UserInfo UserInfo { get; set; }
            public Embedding Embedding { get; set; }
        }

        private readonly List<StorageModel> storedValues;

        public InMemoryFeatureRepository()
        {
            this.storedValues = new List<StorageModel>();
        }

        public Task AddVoiceVecAsync(Embedding embedding, UserInfo userInfo)
        {
            this.storedValues.Add(new StorageModel(){UserInfo = userInfo, Embedding = embedding});
            return Task.Delay(0);// ..CompletedTask;
        }

        public Task<UserInfo[]> GetNearestNeighbors(Embedding vector, int count, IDistanceProvider distanceProvider)
        {
            var result = this.storedValues
                .Select(storedValue => new
                    {name = storedValue.UserInfo, distance = distanceProvider.Measure(storedValue.Embedding, vector)})
                .OrderBy(elem => elem.distance)
                .Take(count)
                .Select(elem => elem.name)
                .ToArray();

            return Task.FromResult(result); 
        }
    }
}
