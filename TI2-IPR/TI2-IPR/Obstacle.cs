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
        private double _SizeX;
        private double _SizeY;

        public Obstacle(double x, double y, double sizeX, double sizeY, double distanceTop, double distanceLeft)
        {
            _currentX = x;
            _currentY = y;
            _SizeX = 20;
            _SizeY = 100;
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
            return _SizeX;
        }

        public double getSizeY()
        {
            return _SizeY;
        }

    }
}
