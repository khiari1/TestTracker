namespace Tsi.Extensions.Identity.Stores
{
    public class TsiIdentityPermission : TsiIdentityPermission<string>
    {
        public TsiIdentityPermission()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

    public class TsiIdentityPermission<TKey>
    {
        public TKey Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string? Description { get; set; } = string.Empty;
        public TKey GroupId { get; set; }
    }
}
