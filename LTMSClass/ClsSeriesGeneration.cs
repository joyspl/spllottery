using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class ClsSeriesGeneration
    {
        #region Busness Objects
        public long ID { get; set; }
        public long Series1Start { get; set; }
        public long Series1End { get; set; }
        public string Series2Start { get; set; }
        public string Series2End { get; set; }
        public long NumStart { get; set; }
        public long NumEnd { get; set; }
        public int ReqId { get; set; }
        public int Opmode { get; set; }
        #endregion
    }
}
