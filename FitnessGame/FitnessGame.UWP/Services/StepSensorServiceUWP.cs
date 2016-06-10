using FitnessGame.DataModels;
using FitnessGame.Interfaces;
using FitnessGame.UWP.Services;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(StepSensorServiceUWP))]
namespace FitnessGame.UWP.Services
{
    public class StepSensorServiceUWP : IStepSensor
    {
        private PlayerInfo playerData { get; set; }
        private Realm _realmdb;
        private bool timerRunning = false;
        public void Start()
        {
            _realmdb = Realm.GetInstance();
            timerRunning = true;
            Device.StartTimer(new TimeSpan(30), () => 
            {
                AddStep();
                return timerRunning;
            });
        }

        public void Stop()
        {
            timerRunning = false;
        }

        private void AddNewDay()
        {
            _realmdb.Write(() =>
            {
                var stepCounter = _realmdb.CreateObject<FootSteps>();
                stepCounter.UnprocessedStepCount = 1;
                stepCounter.DateOfSteps = DateTime.Today;
                playerData.Steps.Add(stepCounter);
            });
        }
        private void AddStep()
        {
            playerData = _realmdb.All<PlayerInfo>().First();
            if (playerData.Steps.Count == 0 || playerData.Steps == null)
            {
                AddNewDay();
            }
            else
            {
                var stepCounter = playerData.Steps.Where<FootSteps>(x => x.DateOfSteps == DateTime.Today).First();
                if (stepCounter != null)
                    _realmdb.Write(() => { stepCounter.UnprocessedStepCount++; });
                else
                {
                    AddNewDay();
                }
            }
        }
    }
}
