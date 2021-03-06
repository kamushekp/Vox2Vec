﻿using System.Threading.Tasks;
using Vox2Vec.Models;

namespace Vox2Vec.Services
{
    public interface IFeatureRepository
    {
        Task AddVoiceVecAsync(Embedding embedding, UserInfo userInfo);
        Task<NearestUser[]> GetNearestNeighbors(Embedding vector, int count, IDistanceProvider distanceProvider);
    }
}