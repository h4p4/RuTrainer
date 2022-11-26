
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RuTrainer.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About application";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/h4p4"));
        }

        public ICommand OpenWebCommand { get; }
    }
}