using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class MapElements
    {
        private const string WALL = "#", STAR = "*", DOT = ".", GHOST = ((char)9787).ToString();
        public string getWall
        {
            get {return WALL;}
        }
        public string getStar
        {
            get { return STAR; }
        }
        public string getDot
        {
            get { return DOT; }
        }
        public string getGhost
        {
            get { return GHOST; }
        }
    }
}
