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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace TI2_IPR.Screens
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameScreen : Page
    {
        private Gyrometer _Gyrometer;
        private Ball ball;
        public GameScreen()
        {
            this.InitializeComponent();
            Canvas.GetLeft(ell1);
            UpdateLayout();
            ball = new Ball(Canvas.GetLeft(ell1), Canvas.GetTop(ell1), this.ActualWidth, this.ActualHeight, this.Margin.Top, this.Margin.Left);
            _Gyrometer = Gyrometer.GetDefault();
            _Gyrometer.ReportInterval = _Gyrometer.MinimumReportInterval;
            _Gyrometer.ReadingChanged += OnGyrometerReading;
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

        private async void OnGyrometerReading(Gyrometer sender, GyrometerReadingChangedEventArgs args)
        {
                double currentX = ball.getLocationX();
                double currentY = ball.getLocationY();

            await this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                Canvas.SetLeft(ell1, currentX);
                Canvas.SetTop(ell1, currentY);
            });
        }

    }
}
