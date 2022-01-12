using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("PatrickHand-Regular.ttf", Alias = "Patrick")]
namespace RolgordijnApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OnboardingPage : CarouselPage
    {
        public OnboardingPage()
        {
            InitializeComponent();
        }

        private void NextBtnClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
    }
}