using FitnessGame.Interfaces;
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
    public class HomePageModel : NavCommands
    {
        public override void Init(object initData)
        {
            
            base.Init(initData);
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
