using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tsi.Erp.TestTracker.Abstractions.Models.Job
{
    public class InvocationData
    {
        private class JobPayload
        {
            public string TypeName { get; set; }

            public string MethodName { get; set; }

            public string[] ParameterTypes { get; set; }

            public string[] Arguments { get; set; }

            public string Queue { get; set; }
        }

        private static readonly object[] EmptyArray = new object[0];

        public string Type { get; }

        public string Method { get; }

        public string ParameterTypes { get; }

        public string Arguments { get; set; }

        public string Queue { get; }


        public InvocationData(string type, string method, string parameterTypes, string arguments)
            : this(type, method, parameterTypes, arguments, null)
        {
        }

        public InvocationData(string type, string method, string parameterTypes, string arguments, string queue)
        {
            Type = type;
            Method = method;
            ParameterTypes = parameterTypes;
            Arguments = arguments;
            Queue = queue;
        }
    }
}
