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
    {

        private Position pacManPos;
        private int score;
        private int lives;
        private int level;

        private string durs = "C";
        private ConsoleColor color = ConsoleColor.Magenta;
        public string Direction = "right";
        public string NextDirection = "right";
        public PacMan()
        {
            this.pacManPos = new Position(17, 20);
            this.score = 0;
            this.lives = 3;
            this.level = 1;
        }

        public int GetScore()
        {
            return this.score;
        }

        public int Lives()
        {
            return this.lives;
        }

        public int GetLevel()
        {
            return this.level;
        }
        public int addLevel()
        { return this.level++; }
        public void ResetPacMan()
        {
            this.pacManPos.X = 17;
            this.pacManPos.Y = 20;
            this.Direction = "right";
            this.NextDirection = "right";
        }

        public void LoseLife()
        {
            this.lives--;
            Thread.Sleep(800);
        }

        public void EarnPoint()
        {
            this.score++;
        }

        public void EarnStar()
        {
            this.score += 100;
        }

        public void LevelUp()
        {
            this.level++;
            this.score = 0;
        }

        public string GetPac()
        {
            return this.durs;
        }

        public int GetPosX()
        {
            return this.pacManPos.X;
        }

        public int GetPosY()
        {
            return this.pacManPos.Y;
        }

        public ConsoleColor GetColor()
        {
            return this.color;
        }

        public MapElements CheckCell(string[,] border, string direction, Ghost[] ghostList)
        {
            switch (direction)
            {
                case "up":
                    switch (border[this.pacManPos.Y - 1, this.pacManPos.X])
                    {
                        case "#":
                            return MapElements.Wall;
                        case ".":
                            return MapElements.Dot;
                        case "*":
                            return MapElements.Star;
                        default:
                            if (checkIfGhostAppears(ghostList, this.pacManPos.Y - 1, this.pacManPos.X))
                            {
                                return MapElements.Ghost;
                            }
                            else
                            {
                                return MapElements.Empty;
                            }
                    }
                case "right":
                    switch (border[this.pacManPos.Y, this.pacManPos.X + 1])
                    {
                        case "#":
                            return MapElements.Wall;
                        case ".":
                            return MapElements.Dot;
                        case "*":
                            return MapElements.Star;
                        default:
                            if (checkIfGhostAppears(ghostList, this.pacManPos.Y, this.pacManPos.X + 1))
                            {
                                return MapElements.Ghost;
                            }
                            else
                            {
                                return MapElements.Empty;
                            }
                    }
                case "down":
                    switch (border[this.pacManPos.Y + 1, this.pacManPos.X])
                    {
                        case "#":
                            return MapElements.Wall;
                        case ".":
                            return MapElements.Dot;
                        case "*":
                            return MapElements.Star;
                        default:
                            if (checkIfGhostAppears(ghostList, this.pacManPos.Y + 1, this.pacManPos.X))
                            {
                                return MapElements.Ghost;
                            }
                            else
                            {
                                return MapElements.Empty;
                            }
                    }
                case "left":
                    switch (border[this.pacManPos.Y, this.pacManPos.X - 1])
                    {
                        case "#":
                            return MapElements.Wall;
                        case ".":
                            return MapElements.Dot;
                        case "*":
                            return MapElements.Star;
                        default:
                            if (checkIfGhostAppears(ghostList, this.pacManPos.Y, this.pacManPos.X - 1))
                            {
                                return MapElements.Ghost;
                            }
                            else
                            {
                                return MapElements.Empty;
                            }
                    }
                default:
                    if (checkIfGhostAppears(ghostList, pacManPos.Y, pacManPos.X))
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
            this.pacManPos.Y -= 1;
        }
        public void MoveDown()
        {
            this.pacManPos.Y += 1;
        }
        public void MoveLeft()
        {
            this.pacManPos.X -= 1;
        }
        public void MoveRight()
        {
            this.pacManPos.X += 1;
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
