using Android.Content;
using showTracker.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WebView), typeof(TransparentWebViewRenderer))]

namespace showTracker.Droid
{
    public class TransparentWebViewRenderer : WebViewRenderer
    {
        public TransparentWebViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}