using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessGame.DataModels
{
    public class PlayerInfo : RealmObject
    {
        [ObjectId]
        public string ID { get; set; }
        public DateTimeOffset DateLastRun { get; set; }
        public double MoneyEarned { get; set; }
        public double Rubies { get; set; }
        public RealmList<FootSteps> Steps { get; }
        public RealmList<FlippedTile> FlippedTiles { get; }
        public int SearchesAvailable { get; set; }
        public int SearchesCompleted { get; set; }
    }
}
