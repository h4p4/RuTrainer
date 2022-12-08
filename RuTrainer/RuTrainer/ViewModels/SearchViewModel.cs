using RuTrainer.Models;
using RuTrainer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RuTrainer.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public ICommand SearchCommand { get; }
        public ObservableCollection<Station> AllStations { get; set; }
        private async void LoadNearbyStationsAsync() 
        {
            var res = await ApiProvider.GetNearbyStationsAsync();
            App.Current.Dispatcher.BeginInvokeOnMainThread((Action)delegate ()
            {
                //foreach (var r in res)
                    //NearbyStationsTitles.Add(r.title);
            });
        }
        //private async void LoadAllStationsAsync()
        //{
        //    var res = await ApiProvider.GetNearbyStationsAsync();
        //    App.Current.Dispatcher.BeginInvokeOnMainThread((Action)delegate ()
        //    {
        //        //foreach (var r in res)
        //        //NearbyStationsTitles.Add(r.title);
        //    });
        //}
        private async void LoadAllStationsAsync()
        {

            AllStations = new ObservableCollection<Station>(AllStations.Take(150));
        }
        //private void LoadAllStationsAsync()
        //{
        //    Task.Run(async () => 
        //    {
        //        //var res = await ApiProvider.GetAllStationsAsync();
        //        //Device.BeginInvokeOnMainThread(() => 
        //        //{
        //        //    foreach (var r in res.regions)
        //        //        foreach (var s in r.settlements)
        //        //            foreach (var st in s.stations.Take(100))
        //        //            {
        //        //                AllStations.Add(st);
        //        //            }
        //        //});
        //        var res = await ApiProvider.GetAllStationsAsync();
        //        App.Current.Dispatcher.BeginInvokeOnMainThread((Action)delegate ()
        //        {
        //            foreach (var r in res.regions)
        //                foreach (var s in r.settlements)
        //                    foreach (var st in s.stations.Take(100))
        //                    {
        //                        AllStations.Add(st);
        //                    }
        //        });
        //    });
        //}
        public SearchViewModel()
        {
            //LoadNearbyStationsAsync();
            AllStations = new ObservableCollection<Station>();
            LoadAllStationsAsync();

            //var a = Task.Run(async () => await LoadNearbyStationsAsync());
            //NearbyStationsTitles = new ObservableCollection<string>(a.Result);
            //LoadNearbyStationsAsync();

            //NearbyStations = new ObservableCollection<Station>();
            //Station station = new Station();
            //station.popular_title = "test";
            //NearbyStations.Add(station);
            //NearbyStations.Add(station);
            //NearbyStations.Add(station);
        }
    }
}
