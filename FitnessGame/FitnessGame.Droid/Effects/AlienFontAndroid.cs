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
using FitnessGame.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;

[assembly: ExportEffect(typeof(AlienFontAndroid), "AlienFont")]
namespace FitnessGame.Droid.Effects
{
    public class AlienFontAndroid : PlatformEffect
    {
        protected override void OnAttached()
        {
            var label = Control as Android.Widget.TextView;
            if (label == null)
                return;

            Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, "Alien-Encounters-Regular.ttf");
            label.Typeface = font;
        }

        protected override void OnDetached()
        {
        }
    }
}