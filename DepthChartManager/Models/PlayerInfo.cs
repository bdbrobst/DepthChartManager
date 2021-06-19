using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepthChartManager.Models
{
    public class PlayerInfo
    {
        public int PlayerID { get; set; }
        public int CfbdapiID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int JerseyNumber { get; set; }
        public string Position { get; set; }

        public int PosRank { get; set;}
        public int Height { get; set; }
        public int Weight { get; set; }

        public int YearsExperience { get; set; }
        public int IsActive { get; set; }
    }
}
