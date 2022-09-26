using Swole_Patrol.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Swole_Patrol.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}