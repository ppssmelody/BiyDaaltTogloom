using System;
using System.Threading.Tasks;
using System.Threading;


namespace AppPaccma
{
    class Game
    {
        static Random random = new Random();
        static bool gamePaused = false;
        static bool pausedTextIsShown = false;
        static bool continueLoop = true;
        static PacMan pacman = new PacMan();
        static Ghost[] ghostList =
        {
            new Ghost(ConsoleColor.Red,15,8),
            new Ghost(ConsoleColor.Cyan,16,12),
            new Ghost(ConsoleColor.Magenta,17,12),
            new Ghost(ConsoleColor.DarkCyan,18,12),

        };

        // Map
        static Map map = new Map();
        static string[,] border = map.GetMap;

        // Console Settings
        const int GameWidth = 70;
        const int GameHeight = 29;

        public static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "Pacman";
            Console.WindowWidth = GameWidth;
            Console.BufferWidth = GameWidth;
            Console.WindowHeight = GameHeight;
            Console.BufferHeight = GameHeight;
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            ShowWelcomeMenu();

            RedrawMap();
            LoadGUI();

            LoadPlayer();

            Loadghosts();

            while (continueLoop)
            {

                ReadUserKey();

                // Check if paused
                if (gamePaused)
                {
                    BlinkPausedText();
                    continue;
                }

                ghostAi();

                PlayerMovement();

                CheckIfNoLives();

                CheckScore();

                Thread.Sleep(200);
            }
        }

        static void LoadGUI()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(40, 2);
            Console.Write("Level: {0}", pacman.GetLevel());
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(40, 4);
            Console.Write("Score: {0}", pacman.GetScore());
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(40, 6);
            Console.Write("Lives: {0}", pacman.Lives());
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(40, GameHeight - 8);
            Console.Write("{0}", new string('-', 22));
            Console.SetCursorPosition(40, GameHeight - 7);
            Console.Write("|  PRESS P TO PAUSE  |");
            Console.SetCursorPosition(40, GameHeight - 6);
            Console.Write("|  PRESS ESC TO EXIT |");
            Console.SetCursorPosition(40, GameHeight - 5);
            Console.Write("{0}", new string('-', 22));
        }

        static void LoadPlayer()
        {
            Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
            Console.ForegroundColor = pacman.GetColor();
            Console.Write(pacman.GetSymbol());
        }

        static void Loadghosts()
        {
            foreach (var ghost in ghostList)
            {
                Console.ForegroundColor = ghost.GetColor();
                Console.SetCursorPosition(ghost.GetPosX(), ghost.GetPosY());
                Console.Write(ghost.GetDur());
            }

        }
        static void Moveghost()
        {
            foreach (var ghost in ghostList)
            {

                Console.ForegroundColor = ghost.GetColor();
                Console.SetCursorPosition(ghost.GetPosX(), ghost.GetPosY());
                Console.Write(ghost.GetDur());
                Console.ForegroundColor = ConsoleColor.White;
                if (ghost.GetPosX() != ghost.omnohPosX || ghost.GetPosY() != ghost.omnohPosY)
                {
                    if (border[ghost.omnohPosY, ghost.omnohPosX] == " ")
                    {
                        Console.SetCursorPosition(ghost.omnohPosX, ghost.omnohPosY);
                        Console.Write(' ');
                    }
                    else if (border[ghost.omnohPosY, ghost.omnohPosX] == ".")
                    {
                        Console.SetCursorPosition(ghost.omnohPosX, ghost.omnohPosY);
                        Console.Write('.');
                    }
                    else if (border[ghost.omnohPosY, ghost.omnohPosX] == "*")
                    {
                        Console.SetCursorPosition(ghost.omnohPosX, ghost.omnohPosY);
                        Console.Write('*');
                    }
                }
            }

        }

        static void ReadUserKey()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Escape:
                        continueLoop = false; // Прекъсва while цикъла
                        GameOver();
                        break;
                    case ConsoleKey.P:
                        SetGamePaused();
                        break;
                    case ConsoleKey.UpArrow:
                        pacman.NextDirection = "up";
                        break;
                    case ConsoleKey.W:
                        pacman.NextDirection = "up";
                        break;
                    case ConsoleKey.DownArrow:
                        pacman.NextDirection = "down";
                        break;

                    case ConsoleKey.S:
                        pacman.NextDirection = "down";
                        break;
                    case ConsoleKey.LeftArrow:
                        pacman.NextDirection = "left";
                        break;
                    case ConsoleKey.A:
                        pacman.NextDirection = "left";
                        break;
                    case ConsoleKey.RightArrow:
                        pacman.NextDirection = "right";
                        break;
                    case ConsoleKey.D:
                        pacman.NextDirection = "right";
                        break;
                }
            }
        }

        static void SetGamePaused()
        {
            switch (gamePaused)
            {
                case false:
                    ShowPausedText(true);
                    break;
                case true:
                    ShowPausedText(false);
                    break;
            }

            gamePaused = gamePaused ? false : true;
        }

        static void BlinkPausedText()
        {
            switch (pausedTextIsShown)
            {
                case true:
                    Thread.Sleep(800);
                    ShowPausedText(false);
                    break;
                case false:
                    Thread.Sleep(800);
                    ShowPausedText(true);
                    break;
            }
        }

        static void ShowPausedText(bool showText)
        {
            switch (showText)
            {
                case true:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(47, GameHeight - 2);
                    Console.Write("PAUSED");
                    pausedTextIsShown = true;
                    break;
                case false:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(47, GameHeight - 2);
                    Console.Write("      ");
                    pausedTextIsShown = false;
                    break;
            }
        }

        static void PlayerMovement()
        {
            switch (pacman.CheckCell(border, pacman.NextDirection, ghostList))
            {
                case MapElements.Dot:
                    MovePlayer(pacman.NextDirection);
                    pacman.EarnPoint();
                    pacman.Direction = pacman.NextDirection;
                    LoadGUI();
                    break;
                case MapElements.Star:
                    MovePlayer(pacman.NextDirection);
                    pacman.EarnStar();
                    pacman.Direction = pacman.NextDirection;
                    LoadGUI();
                    break;
                case MapElements.Empty:
                    MovePlayer(pacman.NextDirection);
                    pacman.Direction = pacman.NextDirection;
                    break;
                case MapElements.Ghost:
                    pacman.LoseLife();
                    MovePlayer("reset");
                    LoadGUI();
                    break;
                case MapElements.Wall:
                    switch (pacman.CheckCell(border, pacman.Direction, ghostList))
                    {
                        case MapElements.Dot:
                            MovePlayer(pacman.Direction);
                            pacman.EarnPoint();
                            LoadGUI();
                            break;
                        case MapElements.Star:
                            MovePlayer(pacman.Direction);
                            pacman.EarnStar();
                            LoadGUI();
                            break;
                        case MapElements.Empty:
                            MovePlayer(pacman.Direction);
                            break;
                        case MapElements.Ghost:
                            pacman.LoseLife();
                            MovePlayer("reset");
                            LoadGUI();
                            break;
                        case MapElements.Wall:
                            break;
                    }
                    break;
            }
        }
        static void MovePlayer(string direction)
        {
            switch (direction)
            {
                case "up":
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.Write(" ");
                    ChangeMap();
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY() - 1);
                    Console.ForegroundColor = pacman.GetColor();
                    Console.Write(pacman.GetSymbol());
                    pacman.MoveUp();
                    break;
                case "right":
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.Write(" ");
                    ChangeMap();
                    Console.SetCursorPosition(pacman.GetPosX() + 1, pacman.GetPosY());
                    Console.ForegroundColor = pacman.GetColor();
                    Console.Write(pacman.GetSymbol());
                    pacman.MoveRight();
                    break;
                case "down":
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.Write(" ");
                    ChangeMap();
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY() + 1);
                    Console.ForegroundColor = pacman.GetColor();
                    Console.Write(pacman.GetSymbol());
                    pacman.MoveDown();
                    break;
                case "left":
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.Write(" ");
                    ChangeMap();
                    Console.SetCursorPosition(pacman.GetPosX() - 1, pacman.GetPosY());
                    Console.ForegroundColor = pacman.GetColor();
                    Console.Write(pacman.GetSymbol());
                    pacman.MoveLeft();
                    break;
                case "reset":
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.Write(" ");
                    ChangeMap();
                    pacman.ResetPacMan();
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.ForegroundColor = pacman.GetColor();
                    Console.Write(pacman.GetSymbol());
                    break;
            }
        }

        static void ghostAi()
        {
            for (int i = 0; i < ghostList.Length; i++)
            {
                if (random.Next(0, 2) != 0)
                {
                    ghostList[i].Direction = Ghost.bolomjitDirections[random.Next(0, Ghost.bolomjitDirections.Length)];
                }
                switch (ghostList[i].Direction)
                {
                    case "left":
                        if (ghostList[i].checkLeft(ghostList, ghostList[i].GetPosX(), ghostList[i].GetPosY(), border))
                        {
                            ghostList[i].MoveLeft();
                            //Moveghost();
                            if (ghostList[i].GetPosX() == pacman.GetPosX() && ghostList[i].GetPosY() == pacman.GetPosY())
                            {
                                pacman.LoseLife();
                                MovePlayer("reset");
                                LoadGUI();
                            }
                        }
                        break;
                    case "right":
                        if (ghostList[i].CheckRight(ghostList, ghostList[i].GetPosX(), ghostList[i].GetPosY(), border))
                        {
                            ghostList[i].MoveRight();
                            //Moveghost();
                            if (ghostList[i].GetPosX() == pacman.GetPosX() && ghostList[i].GetPosY() == pacman.GetPosY())
                            {
                                pacman.LoseLife();
                                MovePlayer("reset");
                                LoadGUI();
                            }
                        }
                        break;
                    case "up":
                        if (ghostList[i].CheckUp(ghostList, ghostList[i].GetPosX(), ghostList[i].GetPosY(), border))
                        {
                            ghostList[i].MoveUp();
                            //Moveghost();
                            if (ghostList[i].GetPosX() == pacman.GetPosX() && ghostList[i].GetPosY() == pacman.GetPosY())
                            {
                                pacman.LoseLife();
                                MovePlayer("reset");
                                LoadGUI();
                            }
                        }
                        break;
                    case "down":
                        if (ghostList[i].CheckDown(ghostList, ghostList[i].GetPosX(), ghostList[i].GetPosY(), border))
                        {
                            ghostList[i].MoveDown();
                            //Moveghost();
                            if (ghostList[i].GetPosX() == pacman.GetPosX() && ghostList[i].GetPosY() == pacman.GetPosY())
                            {
                                pacman.LoseLife();
                                MovePlayer("reset");
                                LoadGUI();
                            }
                        }
                        break;

                }
            }

            Moveghost();
        }

        static void CheckScore()
        {
            if (pacman.GetScore() == 684)
            {
                continueLoop = false;
                WinGame();
            }
        }

        static void CheckIfNoLives()
        {
            if (pacman.Lives() < 0)
            {
                continueLoop = false;
                GameOver();
            }

        }

        static void RedrawMap()
        {
            for (int i = 0; i < map.GetMap.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetMap.GetLength(1); j++)
                {
                    Console.Write("{0}", map.GetMap[i, j]);
                }
                Console.WriteLine();
            }
        }
        static void ChangeMap()
        {
            border[pacman.GetPosY(), pacman.GetPosX()] = " ";
        }

        static void ShowWelcomeMenu()
        {
            RedrawMap();

            int horizontalPos = GameHeight / 2 - 2;
            int verticalPos = GameWidth / 2 - 15;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(verticalPos, horizontalPos);
            Console.Write("|{0}|", new string('-', 28));
            Console.SetCursorPosition(verticalPos, horizontalPos + 1);
            Console.Write("||     PRESS X TO START     ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 2);
            Console.Write("||     PRESS ESC TO EXIT    ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 3);
            Console.Write("|{0}|", new string('-', 28));
            Console.ForegroundColor = ConsoleColor.White;

            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            while (true)
            {
                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                else if (keyPressed.Key == ConsoleKey.X)
                {
                    Console.Clear();
                    break;
                }

                keyPressed = Console.ReadKey(true);
            }
        }

        static void GameOver()
        {
            Console.Clear();
            RedrawMap();

            int horizontalPos = GameHeight / 2 - 2;
            int verticalPos = GameWidth / 2 - 15;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(verticalPos, horizontalPos);
            Console.Write("|{0}|", new string('-', 27));
            Console.SetCursorPosition(verticalPos, horizontalPos + 1);
            Console.Write("||        GAME OVER        ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 2);
            Console.Write("||                         ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 3);
            int score = pacman.GetScore();
            Console.Write("||       SCORE: {0}{1}  ||", score, new string(' ', 9 - score.ToString().Length));
            Console.SetCursorPosition(verticalPos, horizontalPos + 4);
            Console.Write("|{0}|", new string('-', 27));
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, GameHeight - 1);

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

        static void WinGame()
        {
            Console.Clear();
            RedrawMap();

            int horizontalPos = GameHeight / 2 - 2;
            int verticalPos = GameWidth / 2 - 15;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(verticalPos, horizontalPos);
            Console.Write("|{0}|", new string('-', 27));
            Console.SetCursorPosition(verticalPos, horizontalPos + 1);
            Console.Write("||        YOU WON!         ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 2);
            Console.Write("||                         ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 3);
            int score = pacman.GetScore();
            Console.Write("||       SCORE: {0}{1}  ||", score, new string(' ', 9 - score.ToString().Length));
            Console.SetCursorPosition(verticalPos, horizontalPos + 4);
            Console.Write("||                         ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 5);
            Console.Write("||    PRESS ESC TO EXIT    ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 6);
            Console.Write("|{0}|", new string('-', 27));
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, GameHeight - 1);

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
