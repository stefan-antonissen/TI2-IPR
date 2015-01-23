using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TI2_IPR
{
    class Obstacle
    {

        private double _currentX;
        private double _currentY;
        private double _MinY;
        private double _MinX;
        private double _MaxX;
        private double _MaxY;

        public Obstacle(double x, double y, double sizeX, double sizeY, double distanceTop, double distanceLeft)
        {
            _currentX = x;
            _currentY = y;
            _MinX = distanceTop;
            _MinY = distanceLeft;
            _MaxX = distanceTop + sizeX;
            _MaxY = distanceLeft + sizeY;
        }

        public double getLocationX()
        {
            return _currentX;
        }

        public double getLocationY()
        {
            return _currentY;
        }

        public double getSizeX()
        {
            return _MaxX;
        }

        public double getSizeY()
        {
            return _MaxY;
        }

    }
}
