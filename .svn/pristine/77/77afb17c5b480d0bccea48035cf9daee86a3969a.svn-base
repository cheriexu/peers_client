using Android.Graphics;
using Android.Widget;
using Peers.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Peers.Controls;
using Peers;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace Peers.Droid
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null) return;

            // this.Control.BorderStyle = UITextBorderStyle.None;
            this.Control.Background = this.Resources.GetDrawable(Resource.Drawable.noBorderEditText);



        }
    }
}