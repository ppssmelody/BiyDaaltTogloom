using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Pacman
{
    class Pause
    {
        private Boolean gamePaused = false;
        private Boolean pausedTextIsShown = false;
        private const int GAMEWIDTH = 70;
        private const int GAMEHEIGHT = 29;

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
    }
}