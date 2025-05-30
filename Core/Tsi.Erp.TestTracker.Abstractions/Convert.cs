using Microsoft.VisualStudio.Services.Common;
using System.ComponentModel;


namespace System;

public static partial class ConvertHelper
{

    public static object? ChangeType(string value, Type conversionType)
    {
        try
        {
            var isNullable = conversionType.IsOfType(typeof(Nullable<>));
            TypeConverter conv = TypeDescriptor.GetConverter(conversionType);

            var convertedValue =  conv.ConvertFrom(value);

            return convertedValue;            

        }
        catch
        {
            return null;
        }
    }
}


