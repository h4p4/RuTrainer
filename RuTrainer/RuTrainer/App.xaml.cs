using RuTrainer.Services;
using RuTrainer.Views;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel.Design;

namespace RuTrainer
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            Exrin.Common.ThreadHelper.Init(SynchronizationContext.Current);
            Exrin.Common.ThreadHelper.RunOnUIThread(async () => { await ApiProvider.LoadStationsAsync(); });
        }
        //private async void LoadStationsAsync()
        //{
        //    await ApiProvider.LoadStationsAsync();
        //}
        protected override void OnStart()
        {
            //await ApiProvider.LoadStationsAsync();

            //base.OnStart();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
