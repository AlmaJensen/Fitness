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

namespace FitnessGame.Droid.Services
{
    public class StepSensor
    {
        private static String TAG = "StepDetector";
        private float mLimit = 10;
        private float[] mLastValues = new float[3 * 2];
        private float[] mScale = new float[2];
        private float mYOffset;

        private float[] mLastDirections = new float[3 * 2];
        private float[][] mLastExtremes = { new float[3 * 2], new float[3 * 2] };
        private float[] mLastDiff = new float[3 * 2];
        private int mLastMatch = -1;
        public StepSensor()
        {
            int h = 480; // TODO: remove this constant
            mYOffset = h * 0.5f;
            mScale[0] = -(h * 0.5f * (1.0f / (SensorManager.StandardGravity * 2)));
            mScale[1] = -(h * 0.5f * (1.0f / (SensorManager.MagneticFieldEarthMax)));
        }
        public void SetSensitivity(float sensitivity)
        {
            mLimit = sensitivity; // 1.97  2.96  4.44  6.66  10.00  15.00  22.50  33.75  50.62
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
                if(direction == - mLastDirections[k])
                {
                    // Direction changed
                    int extType = (direction > 0 ? 0 : 1);
                    mLastExtremes[extType][k] = mLastValues[k];
                    float diff = Math.Abs(mLastExtremes[extType][k] - mLastExtremes[1 - extType][k]);

                    if(diff > mLimit)
                    {
                        Console.WriteLine(TAG + "step");
                        // handle step here
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

    }
}