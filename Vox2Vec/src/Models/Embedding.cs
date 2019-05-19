using System;

namespace Vox2Vec.Models
{
    [Serializable]
    public class Embedding
    {
        public int Length { get; set; }
        public float[] Values { get; set; }
    }
}