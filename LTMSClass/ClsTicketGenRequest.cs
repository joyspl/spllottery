using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class ClsTicketGenRequest
    {
        public Int64 ID { get; set; }
        public Int64 DataUniqueId { get; set; }
        public Int32 RowNo { get; set; }
        public int FnStart { get; set; }
        public int FnEnd { get; set; }
        public Int64 TnStart { get; set; }
        public Int64 TnEnd { get; set; }
        public string AlphabetSeries { get; set; }
        public Int64 InsertedId { get; set; }
    }
}
