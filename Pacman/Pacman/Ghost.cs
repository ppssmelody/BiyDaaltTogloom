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
        const int[] ghostSpawnX = new int[10]{
        15,16,17,18,15,16,17,18,15,16};
        const int[] ghostSpawnY = new int[10]{
        8,9,10,11,12,11,13,8,9,10};
        private Position ghostPos;
        public int omnohPosX;
        public int omnohPosY;
        private const int EMOJI = 9787;
        private string ghost = null;
        private ConsoleColor color;
        private const string WALL = "#";
        public string Direction = null;
        public static string[] zug =
        {
            "up",
            "down",
            "left",
            "right"
        };
        
        public Ghost(ConsoleColor color, int x, int y)
        {
            this.ghost = "M";//((char)EMOJI).ToString();
            this.color = color;
            this.ghostPos = new Position(x, y);
            this.omnohPosX = x;
            this.omnohPosY = y;
        }
        public bool checkDir(Ghost[] ghostList, string[,] border, int x, int y,string direction)
        {
            bool isEmpty = true;
            switch (direction)
            {
                case "left": isEmpty = CheckLeft(ghostList, x, y, border); break;
                case "right": isEmpty = CheckRight(ghostList, x, y, border); break;
                case "down": isEmpty = CheckDown(ghostList, x, y, border); break;
                case "up": isEmpty = CheckUp(ghostList, x, y, border); break;
                
            }
            return isEmpty;
        }
        private bool CheckLeft(Ghost[] ghostList, int x, int y, string[,] border)
        {
            bool isEmpty = true ;
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
        public void setPosX(int x)
        { this.ghostPos.X = x; }

        public void setPosY(int y)
        { this.ghostPos.Y = y; }
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
