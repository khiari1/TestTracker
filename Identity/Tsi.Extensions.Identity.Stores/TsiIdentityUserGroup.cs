namespace Tsi.Extensions.Identity.Stores
{
    public class TsiIdentityUserGroup : TsiIdentityUserGroup<string>
    {
        public TsiIdentityUserGroup()
        {

        }
    }
    public class TsiIdentityUserGroup<TKey> where TKey : IEquatable<TKey>
    {
        public TsiIdentityUserGroup() { }
        public TKey UserId { get; set; }
        public TKey GroupId { get; set; }
    }
}
