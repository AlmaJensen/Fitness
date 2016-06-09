using FitnessGame.DataModels;
using FreshMvvm;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FitnessGame
{
    public class App : Application
    {
        public App()
        {
            UpdateInitializePlayerInfo();
            var home = FreshPageModelResolver.ResolvePageModel<PageModels.HomePageModel>();
            var navigation = new FreshNavigationContainer(home);
            MainPage = navigation;
        }

        protected override void OnStart()
        {
            UpdateInitializePlayerInfo();

        }

        private static void UpdateInitializePlayerInfo()
        {
            var realm = Realm.GetInstance();
            var testLoad = realm.All<PlayerInfo>();

            if (testLoad.Count() == 0)
            {
                realm.Write(() =>
                {
                    var info = realm.CreateObject<PlayerInfo>();
                    info.ID = "34";
                    info.DailyTasks = new Daillies();
                    info.DateLastRun = DateTime.Today;
                });
            }
            else
            {
                var playerInfo = testLoad.First();
                if (playerInfo.DateLastRun != DateTime.Today)
                {
                    realm.Write(() =>
                    {
                        playerInfo.DateLastRun = DateTime.Today;
                        playerInfo.SearchesAvailable = 0;
                        playerInfo.SearchesCompleted = 0;
                    });
                }

            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            UpdateInitializePlayerInfo();
        }
    }
}
