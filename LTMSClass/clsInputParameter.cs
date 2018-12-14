using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class clsInputParameter
    {
        public Int64 DataUniqueId { get; set; }
        public DateTime InFromDate { get; set; }
        public DateTime InToDate { get; set; }
        public int InStatus { get; set; }
        public string RequestUrl { get; set; }
    }
}
