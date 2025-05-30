using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Erp.TestTracker.Domain.Stores
{
    public class Settings : EntityBase
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

    }

}
