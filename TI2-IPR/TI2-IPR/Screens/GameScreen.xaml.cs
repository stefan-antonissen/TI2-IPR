﻿using System;
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
using Windows.UI.Xaml.Shapes;

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
        private List<Rectangle> obstacles;
        private List<Object> obstacles;
        private Obstacle obstakel;
        private double oldX;
        private double oldY;

        public GameScreen()
        {
            this.InitializeComponent();
            Canvas.GetLeft(ell1);
            UpdateLayout();

            //obstacles.Add("obstakel");

            ball = new Ball(Canvas.GetLeft(ell1), Canvas.GetTop(ell1),475 , 350, this.Margin.Top, this.Margin.Left);
            _accelerometer = Accelerometer.GetDefault();
            _accelerometer.ReportInterval = _accelerometer.MinimumReportInterval;
            _accelerometer.ReadingChanged += OnAccelerometerReading;

            obstakel = new Obstacle(Canvas.GetLeft(obstacle1), Canvas.GetTop(obstacle1), 475, 350, this.Margin.Top, this.Margin.Left);
            this.DataContext = this;
        }

        private void addObstacles()
        {
            foreach (Rectangle r in obstacles)
            {
                this.canvas.Children.Add(r);
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            obstacles = e.Parameter as List<Rectangle>;
            if (obstacles != null)
                addObstacles();
        }

        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LevelScreen));
        }

        private async void OnAccelerometerReading(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            //Boolean setX = false;
            //Boolean setY = false;

            double currentX = ball.getLocationX();
            double currentY = ball.getLocationY();
            double obstakelX = obstakel.getLocationX();
            double obstakelY = obstakel.getLocationY();
            //for (int i = 0; i < obstacles.Count; i++ )
            //{
            //}



            if (currentX + 50 <= obstakelX || currentX >= obstakel.getSizeX() + obstakelX || currentY + 50 <= obstakelY || currentY >= obstakel.getSizeY() + obstakelY)/*als het op 1 plek binnnen obstakel is*/
            {
                if (currentX + 50 >= obstakelX && currentX <= obstakel.getSizeX() && currentY + 50 >= obstakelY && currentY <= obstakel.getSizeY() + obstakelY)/*als binnen obstakel valt*/
                {
                    ball.setLocationX(oldX);
                    
                }
                else if (currentY + 50 <= obstakelY || currentY >= obstakel.getSizeY())
                {
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    {
                        Canvas.SetLeft(ell1, currentX);
                        oldX = currentX;
                    });
                }
                else
                {
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    {
                        Canvas.SetLeft(ell1, currentX);
                        oldX = currentX;
                    });
                }

                if (currentY + 50 >= obstakelY && currentY <= obstakel.getSizeY() + obstakelY && (currentX + 50 >= obstakelX && currentX <= obstakel.getSizeX() + obstakelX))
                {
                    ball.setLocationX(oldY);
                }
                else if (currentX + 50 <= obstakelX || currentX >= obstakel.getSizeX()+ obstakelX)
                {
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    {
                        Canvas.SetTop(ell1, currentY);
                        oldY = currentY;
                    });
                }
                else
                {
                    await this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    {
                        Canvas.SetTop(ell1, currentY);
                        oldY = currentY;
                    });
                }
            }
        }

    }
}
