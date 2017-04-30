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
        private int respawnTime = 600;
        private int firstX = 17, firstY = 20;
        private const int EMOJINUMBER = 9786;
        private const string WALL="#", DOT=".", STAR="*";
        private string durs = null; //Pacmanii console deer durslegdeh helber
        private ConsoleColor color = ConsoleColor.Yellow;//Pacmanii console deer haragdah ongo
        private string anhniizug = "right";
        public string Direction = null;//Pacmanii odoo hodolj bui zug
        public string NextDirection = null;//Pacmanii daraagiin hodloh zug
        public PacMan()
        {
            this.durs=((char)EMOJINUMBER).ToString();
            this.pacmanPos = new Position(firstX, firstY);
            this.score = 0;// Anhnii onoo
            this.lives = 3;// Anhnii ami
            this.level = 1;// Anhnii level (uy)
            this.Direction = anhniizug;
            this.NextDirection = anhniizug;
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
            this.pacmanPos.X = firstX;
            this.pacmanPos.Y = firstY;
            this.Direction = anhniizug;
            this.NextDirection = anhniizug;
        }

        public void LoseLife()//Sunsend iduulehed ami hasagdah funkts
        {
            this.lives--;
            Thread.Sleep(respawnTime); // Iduulsnii daraa anhnii bairlaldaa dahin garah agshinii hugatsaa
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
        private bool upGhostmet(Ghost[] ghostList)
        {
            if (checkIfGhostAppears(ghostList, this.pacmanPos.Y -1, this.pacmanPos.X))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool downGhostmet(Ghost[] ghostList)
        {
            if (checkIfGhostAppears(ghostList, this.pacmanPos.Y + 1, this.pacmanPos.X))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool rightGhostmet(Ghost[] ghostList)
        {
            if (checkIfGhostAppears(ghostList, this.pacmanPos.Y , this.pacmanPos.X + 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool leftGhostmet(Ghost[] ghostList)
        {
            if (checkIfGhostAppears(ghostList, this.pacmanPos.Y, this.pacmanPos.X - 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public MapElements CheckCell(string[,] border, string direction, Ghost[] ghostList)//xodolj bui zug deh daraagiin 1 nudend hana od tseg suns zereg baigaa esehiig shalgah funkts
        {
            switch (direction)// Pacmanii tuhain ywj bui chiglel
            {
                case "up":// deesh ywj baih uyd
                    switch (border[this.pacmanPos.Y - 1, this.pacmanPos.X])// ug chigleld yu baigaag
                    {
                        case WALL:
                            return MapElements.Wall;
                        case DOT:
                            return MapElements.Dot;
                        case STAR:
                            return MapElements.Star;
                        default:
                            if (upGhostmet(ghostList))
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
                        case WALL:
                            return MapElements.Wall;
                        case DOT:
                            return MapElements.Dot;
                        case STAR:
                            return MapElements.Star;
                        default:
                            if (rightGhostmet(ghostList))                           
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
                        case WALL:
                            return MapElements.Wall;
                        case DOT:
                            return MapElements.Dot;
                        case STAR:
                            return MapElements.Star;
                        default:
                            if (downGhostmet(ghostList))
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
                        case WALL:
                            return MapElements.Wall;
                        case DOT:
                            return MapElements.Dot;
                        case STAR:
                            return MapElements.Star;
                        default:
                            if (leftGhostmet(ghostList))
                            {
                                return MapElements.Ghost;
                            }
                            else
                            {
                                return MapElements.Empty;
                            }
                    }
                default:
                    if (checkIfGhostAppears(ghostList,pacmanPos.Y, pacmanPos.X))
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

        private bool checkIfGhostAppears(Ghost[] ghostList, int pacManPosY, int pacManPosX)
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
