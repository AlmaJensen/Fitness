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
        }

        private void _realmdb_RealmChanged(object sender, EventArgs e)
        {
            var playerLoad = _realmdb.All<PlayerInfo>();
            PlayerData = playerLoad.First();
        }

        public Command StartPedometer
        {
            get
            {
                return new Command(async () => {
                    var ped = Xamarin.Forms.DependencyService.Get<IStepSensor>();
                    ped.Start();
                    //await CoreMethods.PushPageModel<CollectionsPageModel>();
                });
            }
        }
    }
}
