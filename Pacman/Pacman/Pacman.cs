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
        public KeyBoard key = new KeyBoard();
        private Display dis = new Display();
        private MapElements MElements = new MapElements();
        private Ghost[] ghostlist;
        private int score;//Onoo 
        private int lives;// Ami
        private int level;// Uy 
        private int respawnTime = 600;
        private int firstX = 17, firstY = 20;
        private const string UP = "up", DOWN = "down",RIGHT="right",LEFT="left";
        private const string WALL="#", DOT=".", STAR="*",EMPTY=" ";
        private string durs = null; //Pacmanii console deer durslegdeh helber
        private ConsoleColor color = ConsoleColor.Yellow;//Pacmanii console deer haragdah ongo
        private const string ANHNIIZUG = "right";
        public string Direction = null;//Pacmanii odoo hodolj bui zug
        public string NextDirection = null;//Pacmanii daraagiin hodloh zug
        public PacMan()
        {
            this.durs=MElements.getPacman;
            this.pacmanPos = new Position(firstX, firstY);
            this.score = 0;// Anhnii onoo
            this.lives = 3;// Anhnii ami
            this.level = 1;// Anhnii level (uy)
            this.Direction = ANHNIIZUG;
            this.NextDirection = ANHNIIZUG;
        }
        public PacMan(string pac,int onoo,int uy,int ami,string firstdirection,int x,int y)
        {
            this.durs = pac;
            this.score = onoo; this.lives = ami; this.level = uy;
            this.Direction = firstdirection; this.NextDirection = firstdirection;
            this.pacmanPos = new Position(x, y);
        }
        public void move()
        {
            key.ReadUserKey();
            switch (key.keyInfo)
            {
                case UP:
                    if (CheckCell(Map.map, NextDirection, ghostlist) == WALL)
                    {
                        dis.drawPacman(pacmanPos.X, pacmanPos.Y, durs, color);
                    }
                    else if (CheckCell(Map.map, NextDirection, ghostlist) == MElements.getStar 
                        || CheckCell(Map.map, NextDirection, ghostlist) == MElements.getDot || CheckCell(Map.map, NextDirection, ghostlist) == MElements.getEmpty)
                    {
                        if (CheckCell(Map.map, NextDirection, ghostlist) == MElements.getStar)
                        {
                            EarnStar(); 
                        }
                        if (CheckCell(Map.map, NextDirection, ghostlist) == MElements.getDot)
                        {
                            EarnPoint();
                        }
                        dis.PManTrace(pacmanPos.X, pacmanPos.Y);
                        dis.drawPacman(pacmanPos.X, pacmanPos.Y - 1, durs, color);
                    }
                    else if (CheckCell(Map.map, NextDirection, ghostlist) == MElements.getGhost)
                    {
                        LoseLife();
                        dis.drawPacman(firstX, firstY, durs, color);
                    }
                    break;

                case DOWN:
                    if (CheckCell(Map.map, NextDirection, ghostlist) == WALL)
                    {
                        dis.drawPacman(pacmanPos.X, pacmanPos.Y, durs, color);
                    }
                    else if (CheckCell(Map.map, NextDirection, ghostlist) == MElements.getStar
                        || CheckCell(Map.map, NextDirection, ghostlist) == MElements.getDot || CheckCell(Map.map, NextDirection, ghostlist) == MElements.getEmpty)
                    {
                        if (CheckCell(Map.map, NextDirection, ghostlist) == MElements.getStar)
                        {
                            EarnStar();
                        }
                        if (CheckCell(Map.map, NextDirection, ghostlist) == MElements.getDot)
                        {
                            EarnPoint();
                        }
                        dis.PManTrace(pacmanPos.X, pacmanPos.Y);
                        dis.drawPacman(pacmanPos.X, pacmanPos.Y + 1, durs, color);
                    }
                    else if (CheckCell(Map.map, NextDirection, ghostlist) == MElements.getGhost)
                    {
                        LoseLife();
                        dis.drawPacman(firstX, firstY, durs, color);
                    }
                  
                    break;

                case RIGHT:
                    if (CheckCell(Map.map, NextDirection, ghostlist) == WALL)
                    {
                        dis.drawPacman(pacmanPos.X, pacmanPos.Y, durs, color);
                    }
                    else if (CheckCell(Map.map, NextDirection, ghostlist) == MElements.getStar
                        || CheckCell(Map.map, NextDirection, ghostlist) == MElements.getDot || CheckCell(Map.map, NextDirection, ghostlist) == MElements.getEmpty)
                    {
                        if (CheckCell(Map.map, NextDirection, ghostlist) == MElements.getStar)
                        {
                            EarnStar();
                        }
                        if (CheckCell(Map.map, NextDirection, ghostlist) == MElements.getDot)
                        {
                            EarnPoint();
                        }
                        dis.PManTrace(pacmanPos.X, pacmanPos.Y);
                        dis.drawPacman(pacmanPos.X + 1, pacmanPos.Y, durs, color);
                    }
                    else if (CheckCell(Map.map, NextDirection, ghostlist) == MElements.getGhost)
                    {
                        LoseLife();
                        dis.drawPacman(firstX, firstY, durs, color);
                    }
                  
                    break;

                case LEFT:
                    if (CheckCell(Map.map, NextDirection, ghostlist) == WALL)
                    {
                        dis.drawPacman(pacmanPos.X, pacmanPos.Y, durs, color);
                    }
                    else if (CheckCell(Map.map, NextDirection, ghostlist) == MElements.getStar
                        || CheckCell(Map.map, NextDirection, ghostlist) == MElements.getDot || CheckCell(Map.map, NextDirection, ghostlist) == MElements.getEmpty)
                    {
                        if (CheckCell(Map.map, NextDirection, ghostlist) == MElements.getStar)
                        {
                            EarnStar();
                        }
                        if (CheckCell(Map.map, NextDirection, ghostlist) == MElements.getDot)
                        {
                            EarnPoint();
                        }
                        dis.PManTrace(pacmanPos.X, pacmanPos.Y);
                        dis.drawPacman(pacmanPos.X - 1, pacmanPos.Y, durs, color);
                    }
                    else if (CheckCell(Map.map, NextDirection, ghostlist) == MElements.getGhost)
                    {
                        LoseLife();
                        dis.drawPacman(firstX, firstY, durs, color);
                    }
                  
                    
                    break;
            }
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
            this.Direction = ANHNIIZUG;
            this.NextDirection = ANHNIIZUG;
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
  
        public string CheckCell(string[,] border, string direction, Ghost[] ghostList)//xodolj bui zug deh daraagiin 1 nudend hana od tseg suns zereg baigaa esehiig shalgah funkts
        {
            switch (direction)// Pacmanii tuhain ywj bui chiglel
            {
                case UP:// deesh ywj baih uyd
                    switch (border[this.pacmanPos.Y - 1, this.pacmanPos.X])// ug chigleld yu baigaag
                    {
                        case WALL:
                            return MElements.getWall;
                        case DOT:
                            return MElements.getDot;
                        case STAR:
                            return MElements.getStar;
                        default:
                            if (checkIfGhostAppears(ghostList, this.pacmanPos.X, this.pacmanPos.Y-1))
                            {
                                return MElements.getGhost;
                            }
                            else
                            {
                                return MElements.getEmpty;
                            }
                    }
                case RIGHT:
                    switch (border[this.pacmanPos.Y, this.pacmanPos.X + 1])
                    {
                        case WALL:
                            return MElements.getWall;
                        case DOT:
                            return MElements.getDot;
                        case STAR:
                            return MElements.getStar;
                        default:
                            if (checkIfGhostAppears(ghostList, pacmanPos.X+1, pacmanPos.Y))                           
                            {
                                return MElements.getGhost;
                            }
                            else
                            {
                                return MElements.getEmpty;
                            }
                    }
                case DOWN:
                    switch (border[this.pacmanPos.Y + 1, this.pacmanPos.X])
                    {
                        case WALL:
                            return MElements.getWall;
                        case DOT:
                            return MElements.getDot;
                        case STAR:
                            return MElements.getStar;
                        default:
                            if (checkIfGhostAppears(ghostList, pacmanPos.X, pacmanPos.Y+1))
                            {
                                return MElements.getGhost;
                            }
                            else
                            {
                                return MElements.getEmpty;
                            }
                    }
                case LEFT:
                    switch (border[this.pacmanPos.Y, this.pacmanPos.X - 1])
                    {
                        case WALL:
                            return MElements.getWall;
                        case DOT:
                            return MElements.getDot;
                        case STAR:
                            return MElements.getStar;
                        default:
                            if (checkIfGhostAppears(ghostList, pacmanPos.X-1, pacmanPos.Y))
                            {
                                return MElements.getGhost;
                            }
                            else
                            {
                                return MElements.getEmpty;
                            }
                    }
                default:
                    if (checkIfGhostAppears(ghostList,pacmanPos.Y, pacmanPos.X))
                    {
                        return MElements.getGhost;
                    }
                    else
                    {
                        return MElements.getEmpty;
                    }
            }
        }
        /*
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
        }*/
    }
}
