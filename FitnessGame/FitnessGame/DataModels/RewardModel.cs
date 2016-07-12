using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessGame.DataModels
{
    public class RewardModel
    {
        public string RewardName { get; set; }
        public int CashValue { get; set; }
        public int RubyValue { get; set; }
        public bool IsCollectionItem { get; set; }
        public int Rarity { get; set; }
    }
}
