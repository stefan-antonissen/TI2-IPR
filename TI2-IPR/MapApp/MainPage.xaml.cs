using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Phone.UI.Input;
using System.Diagnostics;
using Windows.Storage.Streams;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml.Documents;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.Devices.Sensors;
using Windows.UI.Core;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace MapApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Geolocator _geo = new Geolocator() { DesiredAccuracy = PositionAccuracy.High, ReportInterval = 1000 };
        private CoreDispatcher _cd;
        public MapControl _mapControl { get; set; }
        public static MainPage _mapView = null;
        public static readonly object _padlock = new object();
        private SimpleOrientationSensor _simpleorientation = SimpleOrientationSensor.GetDefault();
        private Boolean avans;

        const double PIx = 3.141592653589793;
        const double RADIUS = 6378.16;
        public Dictionary<string, MapIcon> _sightings { get; set; }
        private Ellipse _ellipse;
        private bool _started { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            Window.Current.SizeChanged += Current_SizeChanged;
            _sightings = new Dictionary<string, MapIcon>();
            _started = false;
            /* Layout goed zetten op landscape als de device al op landscape stond */
            if (_simpleorientation.GetCurrentOrientation() == SimpleOrientation.Rotated90DegreesCounterclockwise)
                this.setToLandscape();

            _cd = Window.Current.CoreWindow.Dispatcher;

            this._mapControl = map;
            _mapView = this;

            this.NavigationCacheMode = NavigationCacheMode.Required;
            addToMap();
            avans = false;
            setToCurrentLocation();
            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void addToMap()
        {
            MapIcon tempMapIcon = new MapIcon();
            tempMapIcon.Location = new Geopoint(new BasicGeoposition()
            {
                Latitude = 51.5856,
                Longitude = 4.7935
            });
            tempMapIcon.Title = "Avans";
            _sightings.Add(String.Format("Avans" + "{0}", "lalala"), tempMapIcon);
            map.MapElements.Add(tempMapIcon);
        }

        public static double Radians(double x)
        {
            return x * PIx / 180;
        }

        private async void setToCurrentLocation()
        {
            var location = await getLocationAsync();
            await map.TrySetViewAsync(location.Coordinate.Point, 18, 0, 0, MapAnimationKind.Linear);

            _geo.PositionChanged += new TypedEventHandler<Geolocator, PositionChangedEventArgs>(geo_PositionChanged);
        }

        private async void checkForSightings()
        {
            double latitude;
            double longitude;
            String description;
            double distance = 0.02;
            
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                //for (int i = 0; i < DatabaseConnection.instance.getSightings().Count; i++)
                //{
                    latitude = 51.5856;
                    longitude = 4.7935;

                    var location = await getLocationAsync();

                    if (DistanceBetweenPlaces(longitude, latitude, location.Coordinate.Point.Position.Longitude, location.Coordinate.Point.Position.Latitude) <= distance)
                    {
                        description = "Je bent bij Avans!";
                        
                        if (avans == false)
                        {
                            avans = true;
                            MessageDialog _msgbox = new MessageDialog(description);
                            await _msgbox.ShowAsync();
                            
                        }
                        
                    }
                //}
            });
        }

        public static MainPage instance
        {
            get
            {
                return _mapView;
            }
        }

        public static double DistanceBetweenPlaces(
            double lon1,
            double lat1,
            double lon2,
            double lat2)
        {
            double dlon = Radians(lon2 - lon1);
            double dlat = Radians(lat2 - lat1);

            double a = (Math.Sin(dlat / 2) * Math.Sin(dlat / 2)) + Math.Cos(Radians(lat1)) * Math.Cos(Radians(lat2)) * (Math.Sin(dlon / 2) * Math.Sin(dlon / 2));
            double angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return angle * RADIUS;
        }

        private async void geo_PositionChanged(Geolocator sender, PositionChangedEventArgs e)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                var location = await getLocationAsync();
                map.Children.Remove(_ellipse);
                _ellipse = new Ellipse();

                _ellipse.Fill = new SolidColorBrush(Colors.Red);
                _ellipse.Width = 10;
                _ellipse.Height = 10;
                map.Children.Add(_ellipse);
                MapControl.SetLocation(_ellipse, location.Coordinate.Point);

                if (UpdateLocation_Checkbox.IsChecked == true)
                    await map.TrySetViewAsync(location.Coordinate.Point, map.ZoomLevel, 0, 0, MapAnimationKind.Linear);
            });
            checkForSightings();
        }

        private async Task<Geoposition> getLocationAsync()
        {
            return await _geo.GetGeopositionAsync();
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            string CurrentViewState = ApplicationView.GetForCurrentView().Orientation.ToString();

            if (CurrentViewState == "Portrait")
            {

                Grid.SetRow(map, 0);
                Grid.SetColumn(map, 0);
                Grid.SetRowSpan(map, 2);
                Grid.SetColumnSpan(map, 3);

                Grid.SetRow(SettingsScrollViewer, 2);
                Grid.SetColumn(SettingsScrollViewer, 0);
                Grid.SetRowSpan(SettingsScrollViewer, 1);
                Grid.SetColumnSpan(SettingsScrollViewer, 1);
                Aerial_Checkbox.Margin = new Thickness(4, 0, 4, 0);
                AerialWithRoads_Checkbox.Margin = new Thickness(4, 0, 4, 0);
                Traffic_Checkbox.Margin = new Thickness(4, 0, 4, 0);
                Dark_Checkbox.Margin = new Thickness(4, 0, 4, 0);
                Pedestrian_Checkbox.Margin = new Thickness(4, 0, 4, 0);
            }

            if (CurrentViewState == "Landscape")
            {
                setToLandscape();
            }
        }

        private void setToLandscape()
        {
            Grid.SetRow(map, 0);
            Grid.SetColumn(map, 0);
            Grid.SetRowSpan(map, 3);
            Grid.SetColumnSpan(map, 2);

            Grid.SetRow(SettingsScrollViewer, 0);
            Grid.SetColumn(SettingsScrollViewer, 2);
            Grid.SetRowSpan(SettingsScrollViewer, 1);
            Grid.SetColumnSpan(SettingsScrollViewer, 1);
            Aerial_Checkbox.Margin = new Thickness(4, 0, 4, -25);
            AerialWithRoads_Checkbox.Margin = new Thickness(4, 0, 4, -25);
            Traffic_Checkbox.Margin = new Thickness(4, 0, 4, -25);
            Dark_Checkbox.Margin = new Thickness(4, 0, 4, -25);
            Pedestrian_Checkbox.Margin = new Thickness(4, 0, 4, -25);
        }

        private void Dark_Checked(object sender, RoutedEventArgs e)
        {
            map.ColorScheme = MapColorScheme.Dark;
        }

        private void Dark_Unchecked(object sender, RoutedEventArgs e)
        {
            map.ColorScheme = MapColorScheme.Light;
        }
        private void Traffic_Checked(object sender, RoutedEventArgs e)
        {
            map.TrafficFlowVisible = true;
        }

        private void Traffic_Unchecked(object sender, RoutedEventArgs e)
        {
            map.TrafficFlowVisible = false;
        }

        private void Pedestrian_Checked(object sender, RoutedEventArgs e)
        {
            map.PedestrianFeaturesVisible = true;
        }

        private void Pedestrian_Unchecked(object sender, RoutedEventArgs e)
        {
            map.PedestrianFeaturesVisible = false;
        }

        private void Aerial_Checked(object sender, RoutedEventArgs e)
        {
            map.Style = MapStyle.Aerial;
        }

        private void Map_NoStyle(object sender, RoutedEventArgs e)
        {
            map.Style = MapStyle.Road;
        }

        private void AerialWithRoads_Checked(object sender, RoutedEventArgs e)
        {
            map.Style = MapStyle.AerialWithRoads;
        }
    }
}
