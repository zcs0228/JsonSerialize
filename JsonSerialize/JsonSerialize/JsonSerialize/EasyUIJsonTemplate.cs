using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace JsonSerialize
{
    [DataContract]
    public class EasyUIJsonTemplate<T>
    {
        [DataMember(Name = "total", Order = 1)]
        public int total { get; set; }
        [DataMember(Name="rows", Order=2)]
        public IEnumerable<T> rows { get; set; }
    }
}
