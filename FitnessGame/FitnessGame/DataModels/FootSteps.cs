using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessGame.DataModels
{
    public class FootSteps : RealmObject
    {
        public string PlayerID { get; set; }
        public DateTimeOffset DateOfSteps { get; set; }
        public double UnprocessedStepCount { get; set; }
        public double ProcessedStepCount { get; set; }
        public int AvailableSearches { get; set; }
        public int UsedSearches { get; set; }
    }
}
