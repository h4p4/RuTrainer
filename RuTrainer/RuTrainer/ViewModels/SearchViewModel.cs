using RuTrainer.Models;
using RuTrainer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RuTrainer.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public ICommand SearchCommand { get; }
        public ObservableCollection<Station> NearbyStations { get; }
        public SearchViewModel()
        {
            var ns = ApiProvider.GetNearbyStationsAsync();

            NearbyStations = new ObservableCollection<Station>((IEnumerable<Station>)ns);
            //SearchCommand = new Command(async () => await ApiProvider.);
        }
    }
}
