using Swole_Patrol.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Swole_Patrol.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HealthPage : ContentPage
    {
        public HealthPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();

        }
    }
}