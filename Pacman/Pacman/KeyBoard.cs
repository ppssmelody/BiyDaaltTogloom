using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class KeyBoard
    {
        private ConsoleKeyInfo key;
        public string keyInfo;
        private const string RIGHT = "right", LEFT = "left", UP="up",DOWN="down";
        private Display display;
        public KeyBoard()
        {
            key = new ConsoleKeyInfo();
            display = new Display();
        }
        public void keys()
        {
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        keyInfo = "left";
                        break;
                    case ConsoleKey.RightArrow:
                        keyInfo = "right";
                        break;

                    case ConsoleKey.Escape:
                        display.pauseWrite();
                        bool isEscape = false;
                        while (!isEscape)
                        {
                            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                            {
                                display.pauseClear();
                                isEscape = true;
                            }
                        }
                        break;
                }
            }
            else
            {
                keyInfo = "null";
            }
        }
        public void ReadUserKey()
        {
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        keyInfo = "up";
                        break;
                    case ConsoleKey.DownArrow:
                        keyInfo = "down";
                        break;
                    case ConsoleKey.LeftArrow:
                        keyInfo = "left";
                        break;
                    case ConsoleKey.RightArrow:
                        keyInfo = "right";
                        break;
                    case ConsoleKey.Escape:
                        
                        break;
                    case ConsoleKey.X:
                        Console.Clear();
                        break;
                    case ConsoleKey.P:
                        display.setGamePaused();
                        break;
                }
            }
            else keyInfo = null;
        }
    }
}
