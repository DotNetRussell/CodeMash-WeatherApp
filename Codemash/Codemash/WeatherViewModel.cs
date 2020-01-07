using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Codemash
{
    public class WeatherViewModel : BaseModel
    {
        private string WEATHERMAP_API_KEY = "your api key";

        public ICommand SearchCommand { get; set; }

        public string SearchText { get; set; }

        private WeatherModel _currentLocation;

        public WeatherModel CurrentLocation
        {
            get { return _currentLocation; }
            set { _currentLocation = value; OnPropertyChanged(nameof(CurrentLocation)); }
        }

        public ObservableCollection<WeatherModel> Weathers { get; set; }

        public WeatherViewModel()
        {
            SearchCommand = new Command(SearchCommandExecuted);
            Weathers = new ObservableCollection<WeatherModel>();

            LoadCurrentLocationData();
        }

        private async void LoadCurrentLocationData()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            WebClient client = new WebClient();
            client.DownloadStringCompleted += Client_DownloadStringCompleted1;
            client.DownloadStringAsync(new Uri("https://api.openweathermap.org/data/2.5/weather?units=imperial&lat="+location.Latitude+"&lon="+location.Longitude+"&appid=" + WEATHERMAP_API_KEY));
        }

        private void Client_DownloadStringCompleted1(object sender, DownloadStringCompletedEventArgs e)
        {
            RootObject root = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(e?.Result);
            CurrentLocation = new WeatherModel() { Location = root.name, Temp = root.main.temp, Condition = root.AllWeather };
        }

        private void SearchCommandExecuted(object obj)
        {
            LoadData();
        }

        private async void LoadData()
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += Client_DownloadStringCompleted;
            client.DownloadStringAsync( new Uri("https://api.openweathermap.org/data/2.5/weather?units=imperial&zip=" + SearchText+",us&appid=" + WEATHERMAP_API_KEY));
        }

        private void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            RootObject root = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(e?.Result);
            Weathers.Add(new WeatherModel() { Location = root.name, Temp = root.main.temp, Condition = root.AllWeather });
        }
    }
}
