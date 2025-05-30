using System.ComponentModel;
using System.Reflection;

namespace Tsi.Erp.TestTracker.Api.Helpers
{
    public static class EnumHelper
    {
        public static List<KeyValuePair<string,string>> ParseValues<TEnum>() 
            where TEnum : Enum {

            Array values = Enum.GetValues(typeof(TEnum));
            var keyValuePaires = new List<KeyValuePair<string, string>>();

            foreach ( object value in values) {
                var key = value?.ToString();
                if(key is not null)
                {
                    var field = value?.GetType().GetField(key);
                    if (field != null)
                    {
                        var attribute = field.GetCustomAttribute<DescriptionAttribute>();

                        var attributeValue = attribute?.Description ?? string.Empty;

                        keyValuePaires.Add(new KeyValuePair<string, string>(key, attributeValue));
                    }                    
                }                
            }
            return keyValuePaires;            
        }
    }
}
