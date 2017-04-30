using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class Interfaces
    {
        PacMan pacman = new PacMan();
        private Boolean gamePaused = false;
        private Boolean pausedTextIsShown = false;
        const int GAMEWIDTH = 70;
        const int GAMEHEIGHT = 29;
        public bool gameP() { return this.gamePaused; }
        public bool textS() { return this.pausedTextIsShown; }
        public void LoadPlayer()// toglogchiin bairshil ongo durs zergiig unshij dursleh funkts
        {
            Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
            Console.ForegroundColor = pacman.GetColor();
            Console.Write(pacman.GetPac());
        }
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

        public void LoadGUI()
        {
            Console.ForegroundColor = ConsoleColor.Green;// level gej talbariin baruun deed buland bichigdsen level geh txt-n ongo
            Console.SetCursorPosition(40, 2);// tuunii bairlal
            Console.Write("Level: {0}", pacman.GetLevel());// pacman-s utga awch level oorchlogdono

            Console.ForegroundColor = ConsoleColor.Yellow;// score txt- ni shar ongoor durslegden
            Console.SetCursorPosition(40, 4);// bairshil n 
            Console.Write("Score: {0}", pacman.GetScore());

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(40, 6);
            Console.Write("Lives: {0}", pacman.Lives());

            Console.ForegroundColor = ConsoleColor.White;// baruun dood buland baih togloom pause hiih eswel garah esehg zaasan towchiig haruulsan txtuud
            Console.SetCursorPosition(40, GAMEHEIGHT - 8);
            Console.Write("{0}", new string('-', 22)); // 22 gdg ni "-" ene temdegt 22 udaa durslegdene gesen ug
            Console.SetCursorPosition(40, GAMEHEIGHT - 7);
            Console.Write("|  PRESS P TO PAUSE  |");
            Console.SetCursorPosition(40, GAMEHEIGHT - 6);
            Console.Write("|  PRESS ESC TO EXIT |");
            Console.SetCursorPosition(40, GAMEHEIGHT - 5);
            Console.Write("{0}", new string('-', 22));
        }

    }
}