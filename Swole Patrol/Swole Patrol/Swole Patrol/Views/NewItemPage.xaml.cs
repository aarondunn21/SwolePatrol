using Swole_Patrol.Models;
using Swole_Patrol.ViewModels;
using Xamarin.Forms;

namespace Swole_Patrol.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}