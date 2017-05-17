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
        Random random = new Random();
        private Position ghostPos;
        public int omnohPosX;
        public int omnohPosY;
        private const int EMOJI = 9787;
        private string ghost = null;
        private ConsoleColor color;
        private const string WALL = "#";
        public string Direction = null;
        private const string UP = "up", DOWN = "down", RIGHT = "right", LEFT = "left";
        public static int[] ghostSpanX = new int[10] {15,16,17,13,15,16,17,18,15,16,};
        public static int[] ghostSpanY = new int[10] { 14,13,12,11,12,11,13,10,9,10,};
        public static string[] zug =
        {
            "up",
            "down",
            "left",
            "right"
        };
        public Ghost(ConsoleColor color, int x, int y)
        {
            this.ghost = ((char)EMOJI).ToString();
            this.color = color;
            this.ghostPos = new Position(x, y);
            this.omnohPosX = x;
            this.omnohPosY = y;
        }

        public void move()
        {
            if (random.Next(0, 2) != 0)
            {
               Direction = zug[random.Next(0, Ghost.zug.Length)];
            }
         
                switch (Direction)
                {
                    case LEFT:
                        if (checkDir(ghostList1, border, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), ghostList1[i].Direction))//ghostList1[i].CheckLeft(ghostList1, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), border))
                        {
                            ghostList1[i].MoveLeft();
                            if (ghostList1[i].GetPosX() == pacman.GetPosX() && ghostList1[i].GetPosY() == pacman.GetPosY())
                            {
                                pacman.LoseLife();
                                MovePlayer(RESET);
                            }
                        }
                        break;
                    case RIGHT:
                        if (ghostList1[i].checkDir(ghostList1, border, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), ghostList1[i].Direction))//CheckRight(ghostList1, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), border))
                        {
                            ghostList1[i].MoveRight();
                            if (ghostList1[i].GetPosX() == pacman.GetPosX() && ghostList1[i].GetPosY() == pacman.GetPosY())
                            {
                                pacman.LoseLife();
                                MovePlayer(RESET);
                            }
                        }
                        break;
                    case UP:
                        if (ghostList1[i].checkDir(ghostList1, border, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), ghostList1[i].Direction))//.CheckUp(ghostList1, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), border))
                        {
                            ghostList1[i].MoveUp();
                            if (ghostList1[i].GetPosX() == pacman.GetPosX() && ghostList1[i].GetPosY() == pacman.GetPosY())
                            {
                                pacman.LoseLife();
                                MovePlayer(RESET);
                            }
                        }
                        break;
                    case DOWN:
                        if (ghostList1[i].checkDir(ghostList1, border, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), ghostList1[i].Direction))//ghostList1[i].CheckDown(ghostList1, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), border))
                        {
                            ghostList1[i].MoveDown();
                            if (ghostList1[i].GetPosX() == pacman.GetPosX() && ghostList1[i].GetPosY() == pacman.GetPosY())
                            {
                                pacman.LoseLife();
                                MovePlayer(RESET);
                            }
                        }
                        break;

                }
            
            Moveghost();
        }
        public bool checkDir(Ghost[] ghostList, string[,] border, int x, int y,string direction)
        {
            bool isEmpty = true;
            switch (direction)
            {
                case LEFT: isEmpty = CheckLeft(ghostList, x, y, border); break;
                case RIGHT: isEmpty = CheckRight(ghostList, x, y, border); break;
                case DOWN: isEmpty = CheckDown(ghostList, x, y, border); break;
                case UP: isEmpty = CheckUp(ghostList, x, y, border); break;
                
            }
            return isEmpty;
        }
        public void moveGhost(Ghost[] ghostList)
        {
            foreach (var ghost in ghostList)
            {
 
            }
 
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
