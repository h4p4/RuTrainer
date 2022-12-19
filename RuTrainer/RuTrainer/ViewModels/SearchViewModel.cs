using RuTrainer.Models;
using RuTrainer.Services;
using RuTrainer.Views;
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
        #region Fields
        private string searchBar;
        private string _searchFromText = String.Empty;
        private string _searchToText = String.Empty;
        
        private ObservableCollection<Station> _stationList;

        private Station? _stationsListSelectedItem;
        private Station _stationFrom;
        private Station _stationTo;

        private Command _searchCommand;
        private Command clearCommand;
        private Command searchTripsCommand;
        private Command swapCommand;
        #endregion

        #region Constructors
        public SearchViewModel()
        {
            StationList = new ObservableCollection<Station>();
        }
        #endregion Constructors

        #region Properties


        #region FullProperties
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
        public string SearchFromText { get => _searchFromText; set { SetProperty(ref _searchFromText, value); Search("from"); } }
        public string SearchToText { get => _searchToText; set { SetProperty(ref _searchToText, value); Search("to"); } }
        public ObservableCollection<Station> StationList { get => _stationList; set => SetProperty(ref _stationList, value); }
        #endregion FullProperties

        #region Commands
        public ICommand SearchCommand => _searchCommand ??= new Command(Search);
        public ICommand ClearCommand => clearCommand ??= new Command(Clear);
        public ICommand SwapCommand => swapCommand ??= new Command(Swap);
        public ICommand SearchTripsCommand => searchTripsCommand ??= new Command(async () => await SearchTrips());
        #endregion Commands


        #endregion Properties

        #region Methods


        #region CommandMethods
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
                    }
                }
                else
                if (String.IsNullOrWhiteSpace(SearchToText))
                {
                    _stationTo = null;
                }
                HandleSearch(searchType);
            }).ContinueWith((t) =>
            {
                IsBusy = false;
            });

        }
        private void Clear()
        {
            SearchToText = String.Empty;
            SearchFromText = String.Empty;
        }
        private void Swap()
        {
            var tmp = SearchToText;
            SearchToText = SearchFromText;
            SearchFromText = tmp;
        }
        private async Task SearchTrips()
        {
            if (_stationFrom != null && _stationTo != null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new TripsPage(_stationFrom, _stationTo));
                return;
            }
            if (_stationFrom != null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new TripsPage(_stationFrom, SearchToText));
                return;
            }
            await App.Current.MainPage.Navigation.PushAsync(new TripsPage(SearchFromText, SearchToText));


        }
        #endregion CommandMethods

        #region SubMethods
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
        #endregion SubMethods


        #endregion Methods

    }
}
