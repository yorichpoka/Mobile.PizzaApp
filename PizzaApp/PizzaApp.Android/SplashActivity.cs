using Android.App;

namespace PizzaApp.Droid
{
    [Activity(
        Label = "Pizza",
        Icon = "@mipmap/icon",
        Theme = "@style/splashscreen",
        MainLauncher = true,
        NoHistory = true
    )]
    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnResume()
        {
            // Call base method
            base.OnResume();

            StartActivity(
                typeof(MainActivity)
            );
        }
    }
}