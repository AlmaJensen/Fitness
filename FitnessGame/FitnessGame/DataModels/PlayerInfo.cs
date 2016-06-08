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
        public PlayerInfo()
        {
            DailyTasks = new Daillies();
        }
        public double ID { get; set; }
        public double MoneyEarned { get; set; }
        public double Rubies { get; set; }
        public Daillies DailyTasks { get; set; }
    }
}
