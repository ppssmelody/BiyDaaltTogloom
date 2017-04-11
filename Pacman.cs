using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Media;

namespace AppPaccma
{
    class PacMan
    {

        private Position pacManPos;
        private int score;
        private int lives;
        private int level;

        private string symbol = ((char)9786).ToString();
        private ConsoleColor color = ConsoleColor.Yellow;
        public string Direction = "right";
        public string NextDirection = "right";


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

        public PacMan()
        {

            this.pacManPos = new Position(17, 20);
            this.score = 0;
            this.lives = 3;
            this.level = 1;
        }

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
           // Thread.Sleep(100);
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

        public string GetSymbol()
        {
            return this.symbol;
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

        public MapElements CheckCell(string[,] border, string direction, Ghost[] monsterList)
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
                        //case "☻":
                        //  return BoardElements.Monster;
                        default:
                            if (checkIfMonsterAppears(monsterList, this.pacManPos.Y - 1, this.pacManPos.X))
                            {
                                return MapElements.Ghost;
                            }
                            else
                            {
                                return MapElements.Empty;
                            }
                    }

                //return BoardElements.Empty;
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
                            if (checkIfMonsterAppears(monsterList, this.pacManPos.Y, this.pacManPos.X + 1))
                            {
                                return MapElements.Ghost;
                            }
                            else
                            {
                                return MapElements.Empty;
                            }
                    }

                //return BoardElements.Empty;
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
                            if (checkIfMonsterAppears(monsterList, this.pacManPos.Y + 1, this.pacManPos.X))
                            {
                                return MapElements.Ghost;
                            }
                            else
                            {
                                return MapElements.Empty;
                            }
                    }

                //return BoardElements.Empty;
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
                            if (checkIfMonsterAppears(monsterList, this.pacManPos.Y, this.pacManPos.X - 1))
                            {
                                return MapElements.Ghost;
                            }
                            else
                            {
                                return MapElements.Empty;
                            }
                    }

                //return BoardElements.Empty;
                default:
                    if (checkIfMonsterAppears(monsterList, pacManPos.Y, pacManPos.X))
                    {
                        return MapElements.Ghost;
                    }
                    else
                    {
                        return MapElements.Empty;
                    }
            }
            //return BoardElements.Empty;
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

        public bool checkIfMonsterAppears(Ghost[] monsterList, int pacManPosY, int pacManPosX)
        {
            foreach (var monster in monsterList)
            {
                if (monster.GetPosX() == pacManPosX && monster.GetPosY() == pacManPosY)
                {
                    return true;
                }

            }

            return false;

        }
    }
}
