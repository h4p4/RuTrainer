using RuTrainer.Models;
using RuTrainer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Java.Text.Normalizer;

namespace RuTrainer.ViewModels
{
    public class TripsViewModel : BaseViewModel
    {
        private Station _from;
        private Station _to;
        private ObservableCollection<Segment> _segments;
        private Segment _selectedSegments;
        public Station From { get => _from; set => SetProperty(ref _from, value); }
        public Station To { get => _to; set => SetProperty(ref _to, value); }
        public ObservableCollection<Segment> Segments { get => _segments; set => SetProperty(ref _segments, value); }
        public Segment SelectedSegment { get => _selectedSegments; set => SetProperty(ref _selectedSegments, value); }

        public TripsViewModel(string from, string to)
        {
            InitSchedule(FindSettlement(from).codes.yandex_code, FindSettlement(to).codes.yandex_code);
        }        
        public TripsViewModel(Station from, Station to)
        {
            InitSchedule(from.codes.yandex_code, to.codes.yandex_code);
        }
        public TripsViewModel(Station from, string to) 
        {
            InitSchedule(from.codes.yandex_code, to); // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }
        public TripsViewModel(string from, Station to)
        {
            InitSchedule(FindSettlement(from).codes.yandex_code, to.codes.yandex_code);
        }
        public TripsViewModel()
        {
        }
        private Settlement FindSettlement(string from)
        {
            var settlement = ApiProvider.AllSettlements.Where(x => x.title.ToLower() == from.ToLower()).FirstOrDefault();
            if (settlement != null) return settlement;
            settlement = ApiProvider.AllSettlements.Where(x => x.title.ToLower().StartsWith(from.ToLower())).FirstOrDefault();
            if (settlement != null) return settlement;
            settlement = ApiProvider.AllSettlements.Where(x => x.title.ToLower().Contains(from.ToLower())).FirstOrDefault();
            if (settlement != null) return settlement;

            return null;
        }
        private void InitSchedule(string from, string to) 
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var root = await ApiProvider.GetTripsByStationsAsync(from, to);
                Segments = new ObservableCollection<Segment>(root.Segments);
            }).ContinueWith((t) =>
            {
                IsBusy = false;
            });
        }
    }
}
