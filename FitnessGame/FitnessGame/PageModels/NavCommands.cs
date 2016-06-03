using FreshMvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FitnessGame.PageModels
{
    [ImplementPropertyChanged]
    public class NavCommands : FreshBasePageModel
    {
        public Command AchievementsTapped
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<AchievementsPageModel>();
                });
            }
        }
        public Command StoreTapped
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<StorePageModel>();
                });
            }
        }
        public Command CollectionTapped
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<CollectionsPageModel>();
                });
            }
        }
        public Command StatsTapped
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<StatsPageModel>();
                });
            }
        }
        public Command HomeTapped
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<HomePageModel>();
                });
            }
        }
    }
}
