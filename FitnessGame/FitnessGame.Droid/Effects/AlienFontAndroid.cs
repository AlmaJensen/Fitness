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
[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(AlienFontAndroid), "AlienFont")]
namespace FitnessGame.Droid.Effects
{
    public class AlienFontAndroid : PlatformEffect
    {
        protected override void OnAttached()
        {
            Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, "AlienEncountersRegular.ttf");
            var type = Control.GetType();
            if (Control.GetType().ToString().Contains("TextView"))
            {
                var label = (Control as Android.Widget.TextView).Typeface = font;
                return;
            }
            else if (Control.GetType() == typeof(Android.Widget.Button))
            {
                var label = (Control as Android.Widget.Button).Typeface = font;
                return;
            }
            //if (label == null)
            //    return;


            //label.Typeface = font;
        }

        protected override void OnDetached()
        {
        }
    }
}