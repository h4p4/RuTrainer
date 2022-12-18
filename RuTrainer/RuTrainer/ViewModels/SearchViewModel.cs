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
        private string searchBar;
        private string _searchFromText = String.Empty;
        private string _searchToText = String.Empty;
        private ObservableCollection<Station> _stationList;
        private Station? _stationsListSelectedItem;
        private Command _searchCommand;
        private Station _stationFrom;
        private Station _stationTo;
        public Station? StationsListSelectedItem
        {
            get => _stationsListSelectedItem;
            set
            {
                SetProperty(ref _stationsListSelectedItem, value);
                SetSearchBarText();
                StationList.Clear();
            }
        }
        private void SetSearchBarText()
        {
            if (searchBar == "from")
            {
                _stationFrom = StationsListSelectedItem;
                SearchFromText = _stationFrom.title;
                return;
            }
            _stationTo = StationsListSelectedItem;
            SearchToText = _stationTo.title;
        }
        public string SearchFromText
        {
            get => _searchFromText; 
            set
            {
                SetProperty(ref _searchFromText, value);
                Search("from");
            }
        }
        public string SearchToText
        {
            get => _searchToText;
            set
            {
                SetProperty(ref _searchToText, value);
                Search("to");
            }
        }
        public ObservableCollection<Station> StationList
        {
            get => _stationList;
            set => SetProperty(ref _stationList, value);
        }        
        public ICommand SearchCommand => _searchCommand ??= new Command(Search);

        public SearchViewModel()
        {
            StationList = new ObservableCollection<Station>();
            //LoadAllStationsAsync();
        }
        private void Search(object p)
        {
            string searchType = p.ToString();
            searchBar = searchType;
            IsBusy = true;
            Task.Run(() =>
            {
                StationList.Clear();
                if (searchType == "from")
                {
                    if (String.IsNullOrWhiteSpace(SearchFromText))
                    {
                        _stationFrom = null;
                        return;
                    }
                } else 
                if (String.IsNullOrWhiteSpace(SearchToText))
                {
                    _stationTo = null;
                    return;
                }
                HandleSearch(searchType);
            }).ContinueWith((t) =>
            {
                IsBusy = false;
            });

        }
        private void HandleSearch(string searchType)
        {
            string SearchText;
            if (searchType == "from")
                SearchText = SearchFromText;
            else SearchText = SearchToText;

            var source = ApiProvider.AllStations
                .Where(x => x.Settlement.title.ToLower().StartsWith(SearchText.ToLower()) ||
                            x.Settlement.Region.title.ToLower().StartsWith(SearchText.ToLower()) ||
                            x.title.ToLower().StartsWith(SearchText.ToLower()));
            if (_stationFrom == null ||
                _stationTo == null)
                foreach (var item in source)
                    StationList.Add(item);

        }

        private Command clearCommand;
        public ICommand ClearCommand => clearCommand ??= new Command(Clear);

        private void Clear()
        {
            SearchToText = String.Empty;
            SearchFromText = String.Empty;
        }

        private Command searchTripsCommand;
        public ICommand SearchTripsCommand => searchTripsCommand ??= new Command(SearchTrips);

        private void SearchTrips()
        {

        }

        private Command swapCommand;
        public ICommand SwapCommand => swapCommand ??= new Command(Swap);

        private void Swap()
        {
            var tmp = SearchToText;
            SearchToText = SearchFromText;
            SearchFromText = tmp;
        }
    }
}
