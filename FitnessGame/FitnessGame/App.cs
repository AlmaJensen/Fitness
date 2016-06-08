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
            var realm = Realm.GetInstance();
            var testLoad = realm.All<PlayerInfo>();
            var info = new PlayerInfo();
            if (testLoad.Count() == 0)
            {
                realm.Write(() => {
                    info = realm.CreateObject<PlayerInfo>();
                    info.ID = 34;
                    info.DailyTasks = new Daillies();
                    info.DailyTasks.StepCount = 2;
                });
                //realm.BeginWrite();
                
                //realm.Close();
            }
                //
            var home = FreshPageModelResolver.ResolvePageModel<PageModels.HomePageModel>();
            var navigation = new FreshNavigationContainer(home);
            MainPage = navigation;
            // The root page of your application
            //MainPage = new ContentPage
            //{
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            new Label {
            //                XAlign = TextAlignment.Center,
            //                Text = "Welcome to Xamarin Forms!"
            //            }
            //        }
            //    }
            //};
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
