using Microsoft.Graph;
using System.Drawing.Imaging;
using System.IO;
using System.Linq.Extensions;
using System.Security.Claims;
using Tsi.Erp.TestTracker.Abstractions.Helpers;

namespace Tsi.AspNetCore.Identity.AzureAD
{
    public class UserAzureADManager<TUser, TPermission>
        where TUser : class,new()
        where TPermission :class , new()
    {       
        public UserAzureADManager(IUserStore<TUser,TPermission> userStore,
            GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient ?? throw new ArgumentNullException();
            UserStore = userStore;
        }

        protected readonly IUserStore<TUser, TPermission> UserStore;
        private readonly GraphServiceClient _graphServiceClient;

        /// <summary>
        /// Finding and returns <see cref="Task{User}"/> with given claims principal <paramref name="principal"/>
        /// </summary>
        /// <param name="principal"></param>
        /// <returns>The returns value can be null <see cref="User"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual async Task<TUser?> GetUserAsync(ClaimsPrincipal principal)
        {
            ArgumentNullException.ThrowIfNull(principal);

            var id = GetUserId(principal);
            return id == null ? await Task.FromResult<TUser?>(null) : await FindbyIdAsync(id);
        }
        /// <summary>
        /// Get object id from <see cref="ClaimsPrincipal"/>
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public virtual string? GetUserId(ClaimsPrincipal principal)
        {
            ArgumentNullException.ThrowIfNull(principal);
            return principal.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        /// /// <exception cref="ArgumentNullException"></exception>
        public virtual string? GetUserName(ClaimsPrincipal principal)
        {
            ArgumentNullException.ThrowIfNull(principal);
            return principal.FindFirst(ClaimTypes.Name)?.Value;
        }
        public virtual async Task<IList<TUser>> GetAsync()
        {
            return await UserStore.GetAsync();
        }
        public virtual async Task<IList<TUser>> GetAsync(Query filter)
        {
            return await UserStore.GetAsync(filter);
        }
        public async Task CreateAsync(TUser user)
        {
            ArgumentNullException.ThrowIfNull(() => user);
            
            var userAd = MappingHelper.Map(user);
            var domain = await _graphServiceClient.Domains.Request().GetAsync();
            userAd.UserPrincipalName = userAd.GivenName + "@"+ domain[0].Id;
            userAd.AccountEnabled = true;
            
            var mail = MappingHelper.GetValue<string, TUser>(user, "Mail");
            if (string.IsNullOrWhiteSpace(mail))
            {
                MappingHelper.SetValue(user,"Mail",userAd.UserPrincipalName);
                userAd.Mail = userAd.UserPrincipalName;
            }
            userAd.PasswordProfile = new PasswordProfile
            {
                Password = MappingHelper.GetValue<string, TUser>(user, "Password")
            };


            var result = await _graphServiceClient.Users.Request().AddAsync(userAd);

            MappingHelper.Map(result,user);

            MappingHelper.SetValue(user, "MailNickname", result.MailNickname);

            await UserStore.CreateAsync(user);
        }
        public async Task UpdateAsync(string id ,TUser user)
        {
            var result = await _graphServiceClient.Users[id].Request().UpdateAsync(MappingHelper.Map(user));
            await UserStore.UpdateAsync(user);

        }
        public async Task DeleteAsync(string id)
        {
            var user = await FindbyIdAsync(id);
            await _graphServiceClient.Users[id].Request().DeleteAsync();            
            if(user != null)
            {
                await UserStore.DeleteAsync(user);
            }
            
        }
        public async Task<TUser?> FindbyIdAsync(object id)
        {
            return await UserStore.FindByIdAsync(id);
        }

        public async Task<object> InviteUser(InvitationUserModel invitationUserModel)
        {
            var requestBody = new Invitation
            {
                InvitedUserEmailAddress = invitationUserModel.InvitedUserEmailAddress,
                InviteRedirectUrl = "http://localhost:4200/",
                SendInvitationMessage = invitationUserModel.SendMessage,
                InvitedUser = new Microsoft.Graph.User
                {
                    DisplayName = invitationUserModel.DisplayName,
                    GivenName = invitationUserModel.FirstName,
                    Surname = invitationUserModel.LastName
                }
            };
            var result = await _graphServiceClient.Invitations.Request()
                .AddAsync(requestBody);

            return result;

        }
        //public bool IsUserAdmin(ClaimsPrincipal principal)
        //{
        //   var Upn=principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn")?.Value;
        //    if (Upn == "admin@testtracker.onmicrosoft.com") { return true; } else { return false; }

        //}
        
        public async Task<bool> IsUserGlobalAdministrator(string userId)
        {
            var roleAssignments = await _graphServiceClient.Users[userId].AppRoleAssignments.Request().GetAsync();

            Guid globalAdminRoleId = new Guid("00000000-0000-0000-0000-000000000000");

            return roleAssignments.Any(ra => ra.AppRoleId == globalAdminRoleId);
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<TUser?> FindByLoginAsync(string login)
        {
            return await UserStore.FindByLoginAsync(login);
        }
        
        public async Task<bool> HasPermissionAsync(TUser user, string permessionKey)
        {
            return await UserStore.HasPermissionAsync(user, permessionKey);
        }
        public async Task<IList<object>> HasPermissionsAsync(TUser user, string permessionKey)
        {
            return await UserStore.HasPermissionsAsync(user, permessionKey);
        }
        public async Task<IList<TPermission>> GetUserPermissionsAsync(TUser user)
        {
            return await UserStore.GetUserPermissionsAsync(user);
        }
        public async Task<IList<TUser>> GetUsersNotInGroupAsync(object groupId)
        {
            return await UserStore.GetUsersNotInGroupAsync(groupId);
        }
        public async Task<IList<TUser>> GetUsersInGroupAsync(object groupId)
        {
            return await UserStore.GetUsersInGroupAsync(groupId);
        }

        public async Task AddToGroupAsync(TUser user, object groupId)
        {
            await UserStore.AddToGroupAsync(user, groupId);
        }
        public async Task RemoveFromGroupAsync(TUser user, object groupId)
        {
            await UserStore.RemoveFromGroupAsync(user, groupId);
        }

        public async Task SyncStoreAsync()
        {
            var users = await _graphServiceClient.Users.Request()
                .Select(u => new
                {
                    u.Id,
                    u.UserPrincipalName,
                    u.AccountEnabled,
                    u.City,
                    u.CompanyName,
                    u.Country,
                    u.Department,
                    u.DisplayName,
                    u.EmployeeType,
                    u.JobTitle,
                    u.Mail,
                    u.MobilePhone,
                    u.OfficeLocation,
                    u.PostalCode,
                    u.StreetAddress,
                    u.Surname,
                    u.MailNickname,
                    u.GivenName,
                    u.CreationType,
                }).GetAsync();

            foreach (var user in users)
            {
                var storedUser = await UserStore.FindByIdAsync(user.Id);
                if(storedUser != null)
                {
                    await UserStore.UpdateAsync(MappingHelper.Map(user, storedUser));
                }
                else
                {
                    await UserStore.CreateAsync(MappingHelper.Map<TUser>(user));
                }
                
            }
        }

        public async Task<Dictionary<string, Stream>> UsersPhotoAsync() 
        {
            var result = new Dictionary<string, Stream>();  
            var users = await _graphServiceClient.Users.Request()
                .Select(u => new
                {
                    u.Id,
                    u.DisplayName,
                }
                ).GetAsync();

            foreach (var user in users)
            {
                try
                {
                    var photo = await _graphServiceClient.Users[user.Id].Photo.Content.Request().GetAsync() as MemoryStream;

                    result.Add($"{user.Id}.png", photo);
                }
                catch (Exception ex)
                {
                    var stream  = new MemoryStream();

                    var names = user.DisplayName.Split(' ');

                    var letter = names.Where(n => Array.IndexOf(names,n)<2).Select(x => x.ToUpper().FirstOrDefault()).ToArray();

                    using var avatar = AvatarGenerator.GenerateAvatar(string.Concat(letter));

                    avatar.Save(stream, ImageFormat.Png);

                    stream.Seek(0, SeekOrigin.Begin);

                    result.Add($"{user.Id}.png", stream);

                    
                }
                
            }
            return result;
        }
    }
}

