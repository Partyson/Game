using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;

namespace Game.Model
{
    public class Map
    {
        private HashSet<Point> _landscapePoints;
        public Dictionary<Point, LandType> Landscape;

        public void GenerateMap()
        {
            var fillPercent = 0.7;
            var generator = new Random();
            _landscapePoints = new HashSet<Point>();
            Landscape = new Dictionary<Point, LandType>();
           // while (_landscapePoints.Count < GameSettings.MapHeight * GameSettings.MapWidth * fillPercent)
           // while (_landscapePoints.Count < GameSettings.MapWidth * fillPercent)
            {
              //  var x = generator.Next(GameSettings.MapWidth);
               // var y = generator.Next(GameSettings.MapHeight);
               var x = 2;
               var y = 2;
               var values = Enum.GetValues(typeof(LandType));
                var landType = (LandType)values.GetValue(generator.Next(values.Length));
                _landscapePoints.Add(new Point(x, y));
                Landscape[new Point(x, y)] = landType;
            }
        }
    }
}