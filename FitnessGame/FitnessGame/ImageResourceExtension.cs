using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessGame
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(Source))
                return null;
            var imageSource = ImageSource.FromResource(Source);
            return imageSource;
        }
    }
}
