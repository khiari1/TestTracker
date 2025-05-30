namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Response;
public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool AccountEnabled { get; set; } = true;
    public string? PhotoPath { get; set; } = string.Empty;
    public string ObjectId { get; set; } = string.Empty;
    public string Password { get; set; }

    public string UserPrincipalName { get; set; } = string.Empty;
    public string MailNickname { get; set; } = string.Empty;
}

public class UsersGpDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public IEnumerable<GroupDto> Groups { get; set; }

}
public class UserLoginRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
}

public class UserResult
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}

public class UserResponse{
    public string Id { get; set; }
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
    public string MailNickname { get; set; }
    public string GivenName { get; set; }
    public string? CreationType { get; set; }
    public string UserPrincipalName { get; set; }
    public bool IsAdmin { get; set; }
}
