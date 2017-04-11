﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPaccma
{
    class Ghost
    {
        //69 //28
        private Position ghostPos;
        public int omnohPosX;
        public int omnohPosY;

        private string ghost = ((char)9787).ToString();
        private ConsoleColor color;
        public string Direction = "up";

        public static string[] bolomjitDirections =
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

        public bool checkLeft(Ghost[] ghostList, int x, int y, string[,] border)
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

            if (border[y, x - 1] == "#")
            {
                isEmpty = false;
            }

            return isEmpty;
        }
        public bool CheckRight(Ghost[] ghostList, int x, int y, string[,] border)
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

            if (border[y, x + 1] == "#")
            {
                isEmpty = false;
            }


            return isEmpty;
        }
        public bool CheckUp(Ghost[] ghostList, int x, int y, string[,] border)
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

            if (border[y - 1, x] == "#")
            {
                isEmpty = false;
            }

            return isEmpty;
        }
        public bool CheckDown(Ghost[] ghostList, int x, int y, string[,] border)
        {
            bool isEmpty = true;
            foreach (var ghost in ghostList)
            {
                if (x == ghost.GetPosX() && y + 1 == ghost.GetPosY())
                {
                    isEmpty = false;
                }
            }

            if (border[y + 1, x] == "#")
            {
                isEmpty = false;
            }

            return isEmpty;
        }
        public string GetDur()
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
        public void EraseGhost()
        {
            Console.SetCursorPosition(omnohPosX, omnohPosY);
            Console.Write(' ');
        }
        public void MoveRight()
        {
            if (ghostPos.X + 1 < 34)
            {
                omnohPosX = ghostPos.X;
                omnohPosY = ghostPos.Y;
                ghostPos.X++;
            }
        }
        public void MoveLeft()
        {
            if (ghostPos.X - 1 > 0)
            {
                omnohPosX = ghostPos.X;
                omnohPosY = ghostPos.Y;
                ghostPos.X--;
            }
        }
        public void MoveDown()
        {
            if (ghostPos.Y + 1 < 28)
            {
                omnohPosX = ghostPos.X;
                omnohPosY = ghostPos.Y;
                ghostPos.Y++;
            }
        }
        public void MoveUp()
        {
            if (ghostPos.Y - 1 > 0)
            {
                omnohPosX = ghostPos.X;
                omnohPosY = ghostPos.Y;
                ghostPos.Y--;
            }
        }

    }
}
