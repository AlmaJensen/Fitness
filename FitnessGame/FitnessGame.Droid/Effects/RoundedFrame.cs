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
using Xamarin.Forms.Platform.Android;
using FitnessGame.Droid.Effects;
using Xamarin.Forms;

[assembly: ExportEffect(typeof(RoundedFrame), "FrameRoundedCorners")]
namespace FitnessGame.Droid.Effects
{
    public class RoundedFrame : PlatformEffect
    {
        protected override void OnAttached()
        {
            //var viewGroup = Control as Android.Widget.ViewGroup;
            //if (viewGroup == null)
                return;

            //listView.StackFromBottom = true;
        }

        protected override void OnDetached()
        {
        }
    }
}