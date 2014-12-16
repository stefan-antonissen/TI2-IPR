﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Core;

namespace TI2_IPR
{
    class Ball
    {
        private Gyrometer _Gyrometer;
        private double _currentX;
        private double _currentY;
        private double _MinY;
        private double _MinX;
        private double _MaxX;
        private double _MaxY;

        public Ball(double x, double y, double sizeX, double sizeY,double distanceTop, double distanceLeft)
        {
            _currentX = x;
            _currentY = y;
            _MinX = distanceTop;
            _MinY = distanceLeft;
            _MaxX = distanceTop + sizeX;
            _MaxY = distanceLeft + sizeY;
            newLocation();
        }

        public void newLocation()
        {
            _Gyrometer = Gyrometer.GetDefault();
         
            _Gyrometer.ReportInterval = _Gyrometer.MinimumReportInterval;
            _Gyrometer.ReadingChanged += ReadingChanged;
        }



        private async void ReadingChanged(Gyrometer sender, GyrometerReadingChangedEventArgs args)
        {

            _currentX += args.Reading.AngularVelocityX * 5;
            if (_currentX < _MinX) _currentX = _MinX;
            if (_currentX > _MaxX) _currentX = _MaxX;

            _currentY += -args.Reading.AngularVelocityY * 5;
            if (_currentY < _MinY) _currentY = _MinY;
            if (_currentY > _MaxY) _currentY = _MaxY;
        }

        public double getLocationX()
        {
            return _currentX;
        }

        public double getLocationY()
        {
            return _currentY;
        }
    }
}