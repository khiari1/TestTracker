namespace Tsi.Extensions.Identity.Stores
{

    public class TsiIdentityUser : TsiIdentityUser<string>
    {
        public TsiIdentityUser()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
    public class TsiIdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        public TsiIdentityUser()
        {

        }

        public TKey Id { get; set; }
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Mail { get; set; }
        public string? MobilePhone { get; set; }
        public bool? IsAdmin { get; set; }

    }
}