using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Math;
using Vox2Vec.Models;
using Vox2Vec.Services;

namespace Vox2Vec.DistanceProviders
{
    public class CosineDistanceProvider:IDistanceProvider
    {
        public double Measure(Embedding v1, Embedding v2)
        {
            return (1f - GetCosineSimilarity(v1.Values, v2.Values)) * 0.5f;
        }
        public static double GetCosineSimilarity(float[] v1, float[] v2)
        {
            var dot = 0.0d;
            var mag1 = 0.0d;
            var mag2 = 0.0d;
            for (var n = 0; n < v1.Length; n++)
            {
                dot += v1[n] * v2[n];
                mag1 += Math.Pow(v1[n], 2);
                mag2 += Math.Pow(v2[n], 2);
            }
            return dot / (Math.Sqrt(mag1) * Math.Sqrt(mag2));
        }
    }
}