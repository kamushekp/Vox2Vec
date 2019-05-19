using System;

namespace Vox2Vec.Models
{
    [Serializable]
    public class UserInfo
    {
        private string userName;

        public string UserName
        {
            get => this.userName;

            set => this.userName = value.ToLowerInvariant();
        }
    }
}