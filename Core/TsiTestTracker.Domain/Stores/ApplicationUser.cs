using Tsi.Extensions.Identity.Stores;

namespace Tsi.Erp.TestTracker.Domain.Stores
{
    public class ApplicationUser : TsiIdentityUser
    {
        public string UserPrincipalName { get; set; }
        public bool AccountEnabled { get; set; }
        public string? City { get; set; }
        public string? CompanyName { get; set; }
        public string? Country { get; set; }
        public string? Department { get; set; }
        public string DisplayName { get { return UserName; } set { UserName = value; } }
        public string? EmployeeType { get; set; }
        public string? JobTitle { get; set; }
        public string? OfficeLocation { get; set; }
        public string? PostalCode { get; set; }
        public string? StreetAddress { get; set; }
        public string? Surname
        {
            get
            {
                return LastName;
            }
            set { LastName = value; }
        }
        private string mailNickname;
        public string MailNickname
        {

            get
            {
                if (string.IsNullOrWhiteSpace(mailNickname))
                {
                    mailNickname = $"{GivenName}{Surname}";
                }
                return mailNickname;
            }
            set
            {
                mailNickname = value;
            }
        }
        public string? GivenName
        {
            get { return FirstName; }
            set { FirstName = value; }
        }

        public string? CreationType { get; set; }
    }
}
