using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.Devices.Sensors;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Core;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TI2_IPR.Screens
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameScreen : Page
    {
        private Accelerometer _accelerometer;
        private Ball ball;
        private List<Object> obstacles;
        private double oldX;
        private double oldY;

        public GameScreen()
        {
            this.InitializeComponent();
            Canvas.GetLeft(ell1);
            UpdateLayout();

            //obstacles.Add("obstakel");

            ball = new Ball(Canvas.GetLeft(ell1), Canvas.GetTop(ell1),350 , 475, this.Margin.Top, this.Margin.Left);
            _accelerometer = Accelerometer.GetDefault();
            _accelerometer.ReportInterval = _accelerometer.MinimumReportInterval;
            _accelerometer.ReadingChanged += OnAccelerometerReading;
            this.DataContext = this;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LevelScreen));
        }

        private async void OnAccelerometerReading(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            Boolean setX = false;
            Boolean setY = false;

            double currentX = ball.getLocationX();
            double currentY = ball.getLocationY();
            
            //for (int i = 0; i < obstacles.Count; i++ )
            //{
            //}
            
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                Canvas.SetLeft(ell1, currentX);
                Canvas.SetTop(ell1, currentY);
            });
                
            oldX = currentX;
            oldY = currentY;
        }

    }
}
