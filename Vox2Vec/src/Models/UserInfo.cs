namespace Vox2Vec.Models
{
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