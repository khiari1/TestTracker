namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Request
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Login { get; set; }
        public string Password { get; set; }
    }

    public class UserRequest
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }    
        public bool? AccountEnabled { get; set; }
        public string? City { get; set; } 
        public string? CompanyName { get; set; } 
        public string? Country { get; set; } 
        public string? Departement { get; set; } 
        public string? EmployeeType { get; set; } 
        public string? JobTitle { get; set; }
        public string? Mail { get; set; }
        public string? MobilePhone { get; set; }
        public string? OfficeLocation { get; set; }
        public string? PostalCode { get; set; }
        public string? StreetAddress { get; set; }
    }

    public class UsersGpDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Login { get; set; }
        public IEnumerable<GroupRequest> Groups { get; set; }

    }
    public class UserLoginModel
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class UserResult
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }

    public class InviteUserRequest
    {
        public string InvitedUserEmailAddress { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DisplayName { get; set; }
        public bool SendMessage { get; set; }
    }
}
