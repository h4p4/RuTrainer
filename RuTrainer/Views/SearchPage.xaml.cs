using RuTrainer.ViewModels;
using RuTrainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RuTrainer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
            this.BindingContext = new SearchViewModel();
        }

        private async void SearchBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void CancelBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}