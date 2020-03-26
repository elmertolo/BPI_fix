using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPI_fix.Models
{
    class BranchModel
    {
        public string BRSTN { get; set; }
        public string BranchName { get; set; }
        public Int64? LastNo_MC { get; set; }
        public Int64? LastNo_Regular { get; set; }
        public Int64? LastNo_B4 { get; set; }
        public Int64? LastNo_B3 { get; set; }
        public Int64? LastNo_B6 { get; set; }
        
    }
}
