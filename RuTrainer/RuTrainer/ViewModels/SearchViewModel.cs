using RuTrainer.Models;
using RuTrainer.Services;
using System;
using System.Collections;
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
        private string _searchText = String.Empty;
        private ObservableCollection<Station> _stationsList;
        private Command searchCommand;
        public string SearchText
        {
            get => _searchText; 
            set
            {
                SetProperty(ref _searchText, value);
                Search(_searchText);
            }
        }
        public ObservableCollection<Station> StationsList
        {
            get => _stationsList;
            set => SetProperty(ref _stationsList, value);
        }        
        public ICommand SearchCommand => searchCommand ??= new Command(Search);

        public SearchViewModel()
        {
            StationsList = new ObservableCollection<Station>();
            //LoadAllStationsAsync();
        }
        private void Search(object p)
        {
            IsBusy = true;
            Task.Run(() =>
            {
                StationsList.Clear();
                if (String.IsNullOrWhiteSpace(SearchText)) return;
                HandleSearch();
            }).ContinueWith((t) =>
            {
                IsBusy = false;
            });

        }
        private void HandleSearch()
        {
            var source = ApiProvider.AllStations
                .Where(x => x.Settlement.title.ToLower().StartsWith(SearchText.ToLower()) ||
                            x.Settlement.Region.title.ToLower().StartsWith(SearchText.ToLower()) ||
                            x.title.ToLower().StartsWith(SearchText.ToLower()));
            foreach (var item in source)
                StationsList.Add(item);
        }



    }
}
