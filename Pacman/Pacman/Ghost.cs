using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class Ghost
    {
        //69 //28
        private Position ghostPos;
        public int omnohPosX;
        public int omnohPosY;
        private string ghost = ((char)9787).ToString();
        private ConsoleColor color;
        private const string WALL = "#"; 
        public static string[] zug =
        {
            "up",
            "down",
            "left",
            "right"
        };
        public static Random random = new Random();
        public Ghost(ConsoleColor color, int x, int y)
        {
            this.color = color;
            this.ghostPos = new Position(x, y);
            this.omnohPosX = x;
            this.omnohPosY = y;
        }
        private bool checkLeft(Ghost[] ghostList, int x, int y, string[,] border)
        {
            bool isEmpty = true;
            foreach (var ghost in ghostList)
            {
                if (x - 1 == ghost.GetPosX() && y == ghost.GetPosY())
                {
                    isEmpty = false;
                    break;
                }
            }

            if (border[y, x - 1] == WALL)
            {
                isEmpty = false;
            }

            return isEmpty;
        }
        private bool CheckRight(Ghost[] ghostList, int x, int y, string[,] border)
        {
            bool isEmpty = true;
            foreach (var ghost in ghostList)
            {
                if (x + 1 == ghost.GetPosX() && y == ghost.GetPosY())
                {
                    isEmpty = false;
                    break;
                }
            }

            if (border[y, x + 1] == WALL)
            {
                isEmpty = false;
            }


            return isEmpty;
        }
        private bool CheckUp(Ghost[] ghostList, int x, int y, string[,] border)
        {
            bool isEmpty = true;
            foreach (var ghost in ghostList)
            {
                if (x == ghost.GetPosX() && y - 1 == ghost.GetPosY())
                {
                    isEmpty = false;
                    break;
                }
            }

            if (border[y - 1, x] == WALL)
            {
                isEmpty = false;
            }

            return isEmpty;
        }
        private bool CheckDown(Ghost[] ghostList, int x, int y, string[,] border)
        {
            bool isEmpty = true;
            foreach (var ghost in ghostList)
            {
                if (x == ghost.GetPosX() && y + 1 == ghost.GetPosY())
                {
                    isEmpty = false;
                }
            }

            if (border[y + 1, x] == WALL)
            {
                isEmpty = false;
            }

            return isEmpty;
        }
        public string GetGhost()
        {
            return this.ghost;
        }
        public int GetPosX()
        {
            return this.ghostPos.X;

        }
        public int GetPosY()
        {
            return this.ghostPos.Y;
        }
        public ConsoleColor GetColor()
        {
            return this.color;
        }
        public void MoveRight()
        {
                omnohPosX = ghostPos.X;
                omnohPosY = ghostPos.Y;
                ghostPos.X++;
        }
        public void MoveLeft()
        {
                omnohPosX = ghostPos.X;
                omnohPosY = ghostPos.Y;
                ghostPos.X--;
        }
        public void MoveDown()
        {
                omnohPosX = ghostPos.X;
                omnohPosY = ghostPos.Y;
                ghostPos.Y++;
        }
        public void MoveUp()
        {
                omnohPosX = ghostPos.X;
                omnohPosY = ghostPos.Y;
                ghostPos.Y--;
        }

    }
}
