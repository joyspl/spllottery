using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class ClsMenuInfo
    {
        public string MenuCode { get; set; }
        public string MenuDesc { get; set; }
        public string PageToNavigate { get; set; }
        public bool AllowEntry { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowDelete { get; set; }
        public bool AllowView { get; set; }
    }
}
