using RuTrainer.Models;
using RuTrainer.ViewModels;
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
    public partial class TripsPage : ContentPage
    {
        public TripsPage(string from, string to)
        {
            InitializeComponent();
            this.BindingContext = new TripsViewModel(from, to);
        }        
        public TripsPage(Station from, Station to)
        {
            InitializeComponent();
            this.BindingContext = new TripsViewModel(from, to);
        }        
        public TripsPage(Station from, string to)
        {
            InitializeComponent();
            this.BindingContext = new TripsViewModel(from, to);
        }      
        public TripsPage(string from, Station to)
        {
            InitializeComponent();
            this.BindingContext = new TripsViewModel(from, to);
        }
    }
}