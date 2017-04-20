using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Media;

namespace Pacman
{
    class PacMan
    {   private Position pacmanPos;
        private int score;//Onoo 
        private int lives;// Ami
        private int level;// Uy 

        private string durs = ((char)9786).ToString(); //Pacmanii console deer durslegdeh helber
        private ConsoleColor color = ConsoleColor.Yellow;//Pacmanii console deer haragdah ongo
        public string Direction = "right";//Pacmanii odoo hodolj bui zug
        public string NextDirection = "right";//Pacmanii daraagiin hodloh zug
        public PacMan()
        {
            this.pacmanPos = new Position(17, 20);
            this.score = 0;// Anhnii onoo
            this.lives = 3;// Anhnii ami
            this.level = 1;// Anhnii level (uy)
        }

        public int GetScore()//Onoo butsaah get funkts
        {
            return this.score;
        }

        public int Lives()//Ami butsaah get funkts
        {
            return this.lives;
        }

        public int GetLevel()//Uy butsaah get funkts
        {
            return this.level;
        }
        public int addLevel()//Omnoh uyee duusgaad uy nemegdeh funkts
        { return this.level++; }
        public void ResetPacMan()
        {
            this.pacmanPos.X = 17;
            this.pacmanPos.Y = 20;
            this.Direction = "right";
            this.NextDirection = "right";
        }

        public void LoseLife()//Sunsend iduulehed ami hasagdah funkts
        {
            this.lives--;
            Thread.Sleep(800); // Iduulsnii daraa anhnii bairlaldaa dahin garah agshinii hugatsaa
        }

        public void EarnPoint() // Tseg ideh funkts onoog 1-r nemegduulne
        {
            this.score++;
        }

        public void EarnStar()// Od ideh funkts onoog 100-r nemegduulne
        {
            this.score += 100;
        }
        public string GetPac()//Pacmanii durslegdeh dursiig butsaah get funkts
        {
            return this.durs;
        }

        public int GetPosX()// X deerh coordinatiig butsaah get funkts
        {
            return this.pacmanPos.X;
        }

        public int GetPosY()// Y deerh coordinatiig butsaah get funkts
        {
            return this.pacmanPos.Y;
        }

        public ConsoleColor GetColor()// Pacmanii ongiig butsaah get funkts
        {
            return this.color;
        }

        public MapElements CheckCell(string[,] border, string direction, Ghost[] ghostList)// 1 nudend hana od tseg suns zereg baigaa esehiig shalgah funkts
        {
            switch (direction)// Pacmanii tuhain ywj bui chiglel
            {
                case "up":// deesh ywj baih uyd
                    switch (border[this.pacmanPos.Y - 1, this.pacmanPos.X])// ug chigleld yu baigaag
                    {
                        case "#":
                            return MapElements.Wall;
                        case ".":
                            return MapElements.Dot;
                        case "*":
                            return MapElements.Star;
                        default:
                            if (checkIfGhostAppears(ghostList, this.pacmanPos.Y - 1, this.pacmanPos.X))
                            {
                                return MapElements.Ghost;
                            }
                            else
                            {
                                return MapElements.Empty;
                            }
                    }
                case "right":
                    switch (border[this.pacmanPos.Y, this.pacmanPos.X + 1])
                    {
                        case "#":
                            return MapElements.Wall;
                        case ".":
                            return MapElements.Dot;
                        case "*":
                            return MapElements.Star;
                        default:
                            if (checkIfGhostAppears(ghostList, this.pacmanPos.Y, this.pacmanPos.X + 1))
                            {
                                return MapElements.Ghost;
                            }
                            else
                            {
                                return MapElements.Empty;
                            }
                    }
                case "down":
                    switch (border[this.pacmanPos.Y + 1, this.pacmanPos.X])
                    {
                        case "#":
                            return MapElements.Wall;
                        case ".":
                            return MapElements.Dot;
                        case "*":
                            return MapElements.Star;
                        default:
                            if (checkIfGhostAppears(ghostList, this.pacmanPos.Y + 1, this.pacmanPos.X))
                            {
                                return MapElements.Ghost;
                            }
                            else
                            {
                                return MapElements.Empty;
                            }
                    }
                case "left":
                    switch (border[this.pacmanPos.Y, this.pacmanPos.X - 1])
                    {
                        case "#":
                            return MapElements.Wall;
                        case ".":
                            return MapElements.Dot;
                        case "*":
                            return MapElements.Star;
                        default:
                            if (checkIfGhostAppears(ghostList, this.pacmanPos.Y, this.pacmanPos.X - 1))
                            {
                                return MapElements.Ghost;
                            }
                            else
                            {
                                return MapElements.Empty;
                            }
                    }
                default:
                    if (checkIfGhostAppears(ghostList, pacmanPos.Y, pacmanPos.X))
                    {
                        return MapElements.Ghost;
                    }
                    else
                    {
                        return MapElements.Empty;
                    }
            }
        }
        public void MoveUp()
        {
            this.pacmanPos.Y -= 1;
        }
        public void MoveDown()
        {
            this.pacmanPos.Y += 1;
        }
        public void MoveLeft()
        {
            this.pacmanPos.X -= 1;
        }
        public void MoveRight()
        {
            this.pacmanPos.X += 1;
        }

        public bool checkIfGhostAppears(Ghost[] ghostList, int pacManPosY, int pacManPosX)
        {
            foreach (var ghost in ghostList)
            {
                if (ghost.GetPosX() == pacManPosX && ghost.GetPosY() == pacManPosY)
                {
                    return true;
                }

            }

            return false;

        }
    }
}
