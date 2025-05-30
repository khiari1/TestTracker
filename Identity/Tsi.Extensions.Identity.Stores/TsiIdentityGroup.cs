namespace Tsi.Extensions.Identity.Stores
{
    public class TsiIdentityGroup : TsiIdentityGroup<string>
    {
        public TsiIdentityGroup()
        {
            Id = Guid.NewGuid().ToString();
        }

    }
    public class TsiIdentityGroup<TKey>
        where TKey : IEquatable<TKey>
    {
        public TsiIdentityGroup()
        {

        }

        public TKey Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
