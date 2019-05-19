using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vox2Vec.Models;
using Vox2Vec.Services;

namespace Vox2Vec.Implementation
{
    public class InMemoryFeatureRepository: IFeatureRepository
    {
        private readonly IResourcePaths resourcePaths;

        [Serializable]
        private class StorageModel
        {
            public UserInfo UserInfo { get; set; }
            public Embedding Embedding { get; set; }
        }

        private readonly List<StorageModel> storedValues;

        public InMemoryFeatureRepository(IResourcePaths resourcePaths)
        {
            this.resourcePaths = resourcePaths;
            this.storedValues = Serialization.ReadFromBinaryFile<List<StorageModel>>(resourcePaths.DataDumpFile);

            if (this.storedValues == null)
            {
                this.storedValues = new List<StorageModel>();
            }
        }

        public Task AddVoiceVecAsync(Embedding embedding, UserInfo userInfo)
        {
            this.storedValues.Add(new StorageModel(){UserInfo = userInfo, Embedding = embedding});
            Serialization.WriteToBinaryFile(this.resourcePaths.DataDumpFile, this.storedValues, false);
            return Task.Delay(0);
        }

        public Task<NearestUser[]> GetNearestNeighbors(Embedding vector, int count, IDistanceProvider distanceProvider)
        {
            var result = this.storedValues
                .Select(storedValue => new NearestUser()
                {
                    Name = storedValue.UserInfo.UserName,
                    Distance = distanceProvider.Measure(storedValue.Embedding, vector)
                })
                .OrderBy(elem => elem.Distance)
                .Take(count)
                .ToArray();

            return Task.FromResult(result); 
        }
    }
}
