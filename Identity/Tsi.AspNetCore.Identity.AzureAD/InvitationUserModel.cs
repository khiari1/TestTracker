using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.AspNetCore.Identity.AzureAD
{
    public class InvitationUserModel
    {
        public string InvitedUserEmailAddress { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DisplayName { get; set; }
        public bool SendMessage { get; set; }
    }
}
