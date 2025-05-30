using Microsoft.Graph;

namespace Tsi.AspNetCore.Identity.AzureAD
{
    public static class MappingHelper
    {       
        public static TUserTarget Map<TUserSource,TUserTarget>(TUserSource source, TUserTarget target)
            where TUserTarget : class,new()
        {
            var userAdProperties = typeof(TUserSource).GetProperties();

            foreach (var propertyAd in userAdProperties)
            {
                var property = typeof(TUserTarget).GetProperty(propertyAd.Name);

                if (property != null && property.CanWrite)
                {
                    property.SetValue(target, propertyAd.GetValue(source), null);
                }

            }
            return target;
        }

        public static User Map<TUserSource>(TUserSource source)
            where TUserSource : class
        {
            return Map(source, new User());            
        }

        public static TUserTarget Map<TUserTarget>(User source)
            where TUserTarget : class,new()
        {
            return Map(source, new TUserTarget());
        }

        public static TValue? GetValue<TValue,TUser>(TUser user,string propertyName)
            where TUser : class
            where TValue : class
        {
            var propertyFromTUser = typeof(TUser).GetProperty(propertyName);

            if(propertyFromTUser is null)
            {
                throw new Exception();
            }
            return (TValue?) propertyFromTUser.GetValue(user, null);

        }
        public static void SetValue<TUser>(TUser user, string propertyName,object value)
            where TUser : class
        {
            var propertyFromTUser = typeof(TUser).GetProperty(propertyName);

            if (propertyFromTUser is null)
            {
                throw new Exception();
            }
            propertyFromTUser.SetValue(user, value);

        }
    }
}
