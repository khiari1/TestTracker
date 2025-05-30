using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Erp.TestTracker.Domain.MappingModel
{
    public class KeyValuePair<TKey,TValue> 
    {
        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}
