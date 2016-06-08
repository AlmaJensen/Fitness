using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Hardware;
using FitnessGame.Interfaces;
using FitnessGame.Droid.Services;
using Realms;
using FitnessGame.DataModels;

[assembly: Xamarin.Forms.Dependency(typeof(StepSensorServiceAndroid))]
namespace FitnessGame.Droid.Services
{
    public class StepSensorServiceAndroid : Activity,  IStepSensor, ISensorEventListener
    {
        //public IntPtr Handle
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public void Dispose()
        //{

        //}
        private PlayerInfo playerData { get; set; }
        private Realm _realmdb;
        private static String TAG = "StepDetector";
        private float mLimit = 10;
        private float[] mLastValues = new float[3 * 2];
        private float[] mScale = new float[2];
        private float mYOffset;

        private float[] mLastDirections = new float[3 * 2];
        private float[][] mLastExtremes = { new float[3 * 2], new float[3 * 2] };
        private float[] mLastDiff = new float[3 * 2];
        private int mLastMatch = -1;

        SensorManager _sensorManager;

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {

        }

        public void OnSensorChanged(SensorEvent e)
        {
            Sensor sensor = e.Sensor;
            if (sensor.Type == SensorType.Accelerometer)
            {
                int j = 1;
                float vSum = 0;
                for (int i = 0; i < 3; i++)
                {
                    var ve = mYOffset + e.Values[i] * mScale[j];
                    vSum += ve;
                }
                int k = 0;
                var v = vSum / 3;

                float direction = (v > mLastValues[k] ? 1 : (v < mLastValues[k] ? -1 : 0));
                if (direction == -mLastDirections[k])
                {
                    // Direction changed
                    int extType = (direction > 0 ? 0 : 1);
                    mLastExtremes[extType][k] = mLastValues[k];
                    float diff = Math.Abs(mLastExtremes[extType][k] - mLastExtremes[1 - extType][k]);

                    if (diff > mLimit)
                    {
                        Console.WriteLine(TAG + "step");
                        _realmdb.Write(() => { playerData.DailyTasks.StepCount++; });                        
                        mLastMatch = extType;
                    }
                    else
                    {
                        mLastMatch = -1;
                    }
                    mLastDiff[k] = diff;
                }
                mLastDirections[k] = direction;
                mLastValues[k] = v;
            }
        }

        public void Start()
        {
            _realmdb = Realm.GetInstance();
            playerData = _realmdb.All<PlayerInfo>().First();

            int h = 480; // TODO: remove this constant
            mYOffset = h * 0.5f;
            mScale[0] = -(h * 0.5f * (1.0f / (SensorManager.StandardGravity * 2)));
            mScale[1] = -(h * 0.5f * (1.0f / (SensorManager.MagneticFieldEarthMax)));

            _sensorManager = (SensorManager)Xamarin.Forms.Forms.Context.GetSystemService(Context.SensorService);
            _sensorManager.RegisterListener(this, _sensorManager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Game);
        }


        public void Stop()
        {
            _sensorManager.UnregisterListener(this, _sensorManager.GetDefaultSensor(SensorType.Accelerometer));
        }
    }
}