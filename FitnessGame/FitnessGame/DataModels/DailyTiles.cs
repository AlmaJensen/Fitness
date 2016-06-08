using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessGame.DataModels
{
    public class DailyTiles
    {
        public int ID { get; set; }
        public string ImageName { get; set; }
        public string RewardType { get; set; }
        public bool Claimed { get; set; }
    }
}
