using FitnessGame.DataModels;
using FitnessGame.Interfaces;
using FreshMvvm;
using PropertyChanged;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FitnessGame.PageModels
{
    [ImplementPropertyChanged]
    public class HomePageModel : NavCommands
    {
        private Realm _realmdb;
        public PlayerInfo PlayerData { get; set; }
        public override void Init(object initData)
        {           
            base.Init(initData);
            _realmdb = Realm.GetInstance();
            var playerLoad = _realmdb.All<PlayerInfo>();

            PlayerData = playerLoad.First();

            _realmdb.RealmChanged += _realmdb_RealmChanged;

            StartStepCounter();

            //var f = new Frame();
            //f.OutlineColor = Color.Purple
        }

        private void _realmdb_RealmChanged(object sender, EventArgs e)
        {
            var playerLoad = _realmdb.All<PlayerInfo>();
            PlayerData = playerLoad.First();
            if(PlayerData.Steps.First().UnprocessedStepCount > 0)
            {
                ProcessSteps();
            }
        }

        private void ProcessSteps()
        {
            var pastDays = PlayerData.Steps.Where(x => x.DateOfSteps != DateTime.Today);
            if(pastDays.Count() > 0)
            {
                var f = new Frame();
                foreach (var s in pastDays)
                    _realmdb.Write(() => 
                    {
                        var p = _realmdb.All<PlayerInfo>().First();
                        p.MoneyEarned += s.UnprocessedStepCount;
                        p.Steps.Remove(s);
                    });
            }
            try
            {
                var playerInfo = _realmdb.All<PlayerInfo>().First();
                var todaysSteps = playerInfo.Steps.Where(x => x.DateOfSteps == DateTime.Today).First();
                if(todaysSteps.UnprocessedStepCount > 0)
                {
                    var tempCount = todaysSteps.UnprocessedStepCount;
                    _realmdb.Write(() =>
                    {
                        todaysSteps.UnprocessedStepCount = 0;
                        playerInfo.MoneyEarned += tempCount;
                        todaysSteps.ProcessedStepCount += tempCount;
                        todaysSteps.AvailableSearches = CalculateAvailableSearches(todaysSteps.ProcessedStepCount);
                        playerInfo.SearchesAvailable = todaysSteps.AvailableSearches;
                    });
                }
                    
            }
            catch { }
        }

        private int CalculateAvailableSearches(double processedStepCount)
        {
            if (processedStepCount < 400)
                return 0;
            else
            {
                var s = processedStepCount / 400;
                if (s >= 25)
                    return 25;
                else
                    return (int)s;
            }
        }

        public Command TileTapped
        {
            get
            {
                return new Command<string>((key) => 
                {
                    _realmdb.Write(()=> {
                        var playerInfo = _realmdb.All<PlayerInfo>().First();
                        playerInfo.SearchesCompleted++;
                    });
                },
                (nothing)=> { return PlayerData.SearchesAvailable > PlayerData.SearchesCompleted; });
            }
        }

        private void StartStepCounter()
        {
            var ped = Xamarin.Forms.DependencyService.Get<IStepSensor>();
            ped.Start();
        }
    }
}
