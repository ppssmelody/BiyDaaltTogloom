using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Pacman
{
    class Display
    {
        private Boolean gamePaused = false;
        private Boolean pausedTextIsShown = false;
        private const int GAMEWIDTH = 70;
        private const int GAMEHEIGHT = 29;
        private Ghost[] ghostlist;

        public bool gameP() { return this.gamePaused; }
        public bool textS() { return this.pausedTextIsShown; }
        public void setGamePaused()
        {
            switch (gameP())
            {
                case false:
                    showPausedText(true);
                    break;
                case true:
                    showPausedText(false);
                    break;
            }

            gamePaused = gamePaused ? false : true;
        }

        public void blinkPausedText()
        {
            switch (pausedTextIsShown)
            {
                case true:
                    Thread.Sleep(800);
                    showPausedText(false);
                    break;
                case false:
                    Thread.Sleep(800);
                    showPausedText(true);
                    break;
            }
        }

        public void showPausedText(bool showText)
        {
            switch (showText)
            {
                case true:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(47, GAMEHEIGHT - 2);
                    Console.Write("PAUSED");
                    pausedTextIsShown = true;
                    break;
                case false:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(47, GAMEHEIGHT - 2);
                    Console.Write("      ");
                    pausedTextIsShown = false;
                    break;
            }
        }
        public Display()
        {
            Console.CursorVisible = false;
        }
        public void drawMap()
        {
            for (int i = 0; i < Map.map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.map.GetLength(1); j++)
                {
                    Console.Write(Map.map[i, j]);
                }
                Console.WriteLine();
            }
        }
        public void showLife(int lives)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(40, 6);
            Console.Write("Lives: {0}", +lives);
        }
        public void showScore(int score)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;// score txt- ni shar ongoor durslegden
            Console.SetCursorPosition(40, 4);// bairshil n 
            Console.Write("Score: {0}", +score);
        }
        public void showLevel(int level)
        {
            Console.ForegroundColor = ConsoleColor.Green;// level gej talbariin baruun deed buland bichigdsen level geh txt-n ongo
            Console.SetCursorPosition(40, 2);// tuunii bairlal
            Console.Write("Level: {0}", +level);// pacman-s utga awch level oorchlogdono
        }
        public void drawPacman(int x, int y, string pacman,ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(pacman); 
        }
        public void clearPacman(int x, int y)
        {
            Map.map[x, y] = " ";
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }
        public void PManTrace(int x,int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
            Map.map[x, y] = " ";
        }
        public void ghostTrace()
        {
            foreach (var ghost in ghostlist)
            {
                Console.ForegroundColor = ConsoleColor.White;//herwee tsagaan ongoor ogoogui bol sunsnii ywsan gazart ymr neg element baih yum bol ug sunsnii ongoor elementuud oorchlogdono
                if (ghost.GetPosX() != ghost.omnohPosX || ghost.GetPosY() != ghost.omnohPosY)// herwee suns bairnaasaa hodolson bol
                {
                    if (Map.map[ghost.omnohPosY, ghost.omnohPosX] == " ")
                    {
                        Console.SetCursorPosition(ghost.omnohPosX, ghost.omnohPosY);
                        Console.Write(' ');
                    }
                    else if (Map.map[ghost.omnohPosY, ghost.omnohPosX] == ".")
                    {
                        Console.SetCursorPosition(ghost.omnohPosX, ghost.omnohPosY);
                        Console.Write('.');
                    }
                    else if (Map.map[ghost.omnohPosY, ghost.omnohPosX] == "*")
                    {
                        Console.SetCursorPosition(ghost.omnohPosX, ghost.omnohPosY);
                        Console.Write('*');
                    }
                }
            }
        }
        public void drawGhost()
        {
            foreach(var ghost in ghostlist){
                Console.ForegroundColor = ghost.GetColor();
                Console.SetCursorPosition(ghost.GetPosX(), ghost.GetPosY());
                Console.Write(ghost.GetGhost());
            }
        }
        public void gameover()
        {
            Console.Clear();
            int horizontalPos = GAMEHEIGHT / 2 - 2;
            int verticalPos = GAMEWIDTH / 2 - 15;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(verticalPos, horizontalPos);
            Console.Write("|{0}|", new string('-', 27));
            Console.SetCursorPosition(verticalPos, horizontalPos + 1);
            Console.Write("||        GAME OVER        ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 2);
            Console.Write("|{0}|", new string('-', 27));
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, GAMEHEIGHT - 1);
            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            while (true)
            {
                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }

                keyPressed = Console.ReadKey(true);
            }
        }
        public void win()
        {
            Console.Clear();
            int horizontalPos = GAMEHEIGHT / 2 - 2;
            int verticalPos = GAMEWIDTH / 2 - 15;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(verticalPos, horizontalPos);
            Console.Write("|{0}|", new string('-', 27));
            Console.SetCursorPosition(verticalPos, horizontalPos + 1);
            Console.Write("||        YOU WON!         ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 2);
            Console.Write("||                         ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 3);
            Console.Write("||                         ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 4);
            Console.Write("||    PRESS ESC TO EXIT    ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 5);
            Console.Write("|{0}|", new string('-', 27));
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, GAMEHEIGHT - 1);

            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            while (true)
            {
                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }

                keyPressed = Console.ReadKey(true);
            }
        }
    }
}
