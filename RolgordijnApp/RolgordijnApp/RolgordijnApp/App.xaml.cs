using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("PatrickHand-Regular.ttf", Alias = "Patrick")]
namespace RolgordijnApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            VersionTracking.Track();

            //MainPage = new OnboardingPage();

            if (VersionTracking.IsFirstLaunchEver)
            {
                MainPage = new OnboardingPage();
            }
            else
            {
                MainPage = new MainPage();
            }

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
