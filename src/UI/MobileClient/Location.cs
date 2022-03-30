using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.DesktopClient
{
    public struct Location
    {
        public int XLocation { get; set; }
        public int YLocation { get; set; }

        public Location(int xLocation, int yLocation)
        {
            XLocation = xLocation;
            YLocation = yLocation;
        }
    }
}
