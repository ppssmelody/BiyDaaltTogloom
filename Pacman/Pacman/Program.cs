using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Pacman
{
    class Program 
    {
        //static int level = 0;
        static int box = 1;// uy dawj suns hurd zergiig nemehed heregtei bolsn tooluur maygiin zuil
        static Random random = new Random();  //sunsnii hiih hodolgoon random baina
        static bool gamePaused = false;  // togloom pause hiigdsen esehg zaah huwisagch
        static bool pausedTextIsShown = false; //togloom pause hiigdwel garj ireh bichig text n haragdaj baigaa eseh 
        static bool loop = true; // main funkts dotorh while gol dawtaltiig urgejluuleh zogsooh gol huwisagch
        static PacMan pacman = new PacMan();  
        static int speed = 200; // togloomnii hurdnii anhnii utga
        static Ghost[] ghostList1 = new Ghost[4] // sunsnii anhnii utga anh 4 suns  ogogdono
        {
            new Ghost(ConsoleColor.Red,15,8),
            new Ghost(ConsoleColor.Cyan,16,12),
            new Ghost(ConsoleColor.Magenta,17,12),
            new Ghost(ConsoleColor.DarkCyan,18,12),
            
        };
        // Map
        static Map map = new Map();
        static string[,] border = map.GetMap;  //border huwisagchid map aa hadgalj map deeree oorchlolt oruulahad ashiglasan 
        // Console Settings
        const int GameWidth = 70;
        const int GameHeight = 29;

        public static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "Pacman";
            Console.WindowWidth = GameWidth; // console-iin orgond ooriinhoo deer zarlasan gamewidth const utgatai huwisagchiin  utgaar hemjeeg n zaaj ogson
            Console.BufferWidth = GameWidth;
            Console.WindowHeight = GameHeight;// ondor-iin hemjeeg olgoson
            Console.BufferHeight = GameHeight;

            ShowWelcomeMenu();// ug funkts n togloom ehlehed garch ireh x darj togloom ehluuleh eswel escape darj garah gsn txt-g haruulna

            RedrawMap(); //map-g achaallaj delgetsend zurah funkts
            LoadGUI();// interface zereg orchniig achaallaj unshih funkts

            LoadPlayer();// pacman buyu toglochiig achaallaj unshih funkts

            Loadghosts();// ghost-iig achaallan unshih funkts

            while (loop)// gol dawtalt
            {

                ReadUserKey();// toglogch keyboardnii ymr towch darj baigaag unshih funkts
                if (gamePaused) // togloom pause hiigdsen eseh
                {
                    BlinkPausedText();//pause hiigdsen bol paused gej garah text-g aniwchuulah funkts
                    continue;
                }
                
                ghostAi();// sunsnii hodolgoon hiih zereg sunstei holbootoi buhii l zuilg hj bui funkts

                PlayerMovement();// toglogchiin hodolgoon

                CheckIfNoLives();// ami baiga esehg shalgaad baixgvi bol dawtalt zogsooj togloom duusgana

                CheckScore();// onoog shalgah funkts

                Thread.Sleep(speed);// hurdiig taaruulj ogson thread
            }
        }

        static void LoadGUI()
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
            Console.SetCursorPosition(40, GameHeight - 8);
            Console.Write("{0}", new string('-', 22)); // 22 gdg ni "-" ene temdegt 22 udaa durslegdene gesen ug
            Console.SetCursorPosition(40, GameHeight - 7);
            Console.Write("|  PRESS P TO PAUSE  |");
            Console.SetCursorPosition(40, GameHeight - 6);
            Console.Write("|  PRESS ESC TO EXIT |");
            Console.SetCursorPosition(40, GameHeight - 5);
            Console.Write("{0}", new string('-', 22));
        }

        static void LoadPlayer()// toglogchiin bairshil ongo durs zergiig unshij dursleh funkts
        {
            Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
            Console.ForegroundColor = pacman.GetColor();
            Console.Write(pacman.GetPac());
        }

        static void Loadghosts()
        {
            foreach (var ghost in ghostList1)// gholstlist1 dotorh ghost huwisagch burt doorh uildluudiig hiine gsn operator
            {
                Console.ForegroundColor = ghost.GetColor();
                Console.SetCursorPosition(ghost.GetPosX(), ghost.GetPosY());
                Console.Write(ghost.GetGhost());
            }

        }
        static void Moveghost()// sunsee hodolgoh funkts
        {
            foreach (var ghost in ghostList1)
            {

                Console.ForegroundColor = ghost.GetColor();
                Console.SetCursorPosition(ghost.GetPosX(), ghost.GetPosY());
                Console.Write(ghost.GetGhost());
                Console.ForegroundColor = ConsoleColor.White;
                if (ghost.GetPosX() != ghost.omnohPosX || ghost.GetPosY() != ghost.omnohPosY)// herwee suns bairnaasaa hodolson bol
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
                        loop = false; // 
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
            switch (pacman.CheckCell(border, pacman.NextDirection, ghostList1))
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
                    switch (pacman.CheckCell(border, pacman.Direction, ghostList1))
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
       /* static void resetGhostPos()
        {
            foreach(var ghost,in ghostList1)
            {
                Console.SetCursorPosition(ghost.);
            }
        }*/
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
                    Console.Write(pacman.GetPac());
                    pacman.MoveUp();
                    break;
                case "right":
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.Write(" ");
                    ChangeMap();
                    Console.SetCursorPosition(pacman.GetPosX() + 1, pacman.GetPosY());
                    Console.ForegroundColor = pacman.GetColor();
                    Console.Write(pacman.GetPac());
                    pacman.MoveRight();
                    break;
                case "down":
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.Write(" ");
                    ChangeMap();
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY() + 1);
                    Console.ForegroundColor = pacman.GetColor();
                    Console.Write(pacman.GetPac());
                    pacman.MoveDown();
                    break;
                case "left":
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.Write(" ");
                    ChangeMap();
                    Console.SetCursorPosition(pacman.GetPosX() - 1, pacman.GetPosY());
                    Console.ForegroundColor = pacman.GetColor();
                    Console.Write(pacman.GetPac());
                    pacman.MoveLeft();
                    break;
                case "reset":
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.Write(" ");
                    ChangeMap();
                    pacman.ResetPacMan();
                    Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
                    Console.ForegroundColor = pacman.GetColor();
                    Console.Write(pacman.GetPac());
                    break;
            }
        }
        static void ghostAi()
        {
            for (int i = 0; i < ghostList1.Length; i++)
            {
                if (random.Next(0, 2) != 0)
                {
                    ghostList1[i].Direction = Ghost.bolomjitZug[random.Next(0, Ghost.bolomjitZug.Length)];
                }
                switch (ghostList1[i].Direction)
                {
                    case "left":
                        if (ghostList1[i].checkLeft(ghostList1, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), border))
                        {
                            ghostList1[i].MoveLeft();
                            //Moveghost();
                            if (ghostList1[i].GetPosX() == pacman.GetPosX() && ghostList1[i].GetPosY() == pacman.GetPosY())
                            {
                                pacman.LoseLife();
                                MovePlayer("reset");
                                LoadGUI();
                            }
                        }
                        break;
                    case "right":
                        if (ghostList1[i].CheckRight(ghostList1, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), border))
                        {
                            ghostList1[i].MoveRight();
                            //Moveghost();
                            if (ghostList1[i].GetPosX() == pacman.GetPosX() && ghostList1[i].GetPosY() == pacman.GetPosY())
                            {
                                pacman.LoseLife();
                                MovePlayer("reset");
                                LoadGUI();
                            }
                        }
                        break;
                    case "up":
                        if (ghostList1[i].CheckUp(ghostList1, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), border))
                        {
                            ghostList1[i].MoveUp();
                            //Moveghost();
                            if (ghostList1[i].GetPosX() == pacman.GetPosX() && ghostList1[i].GetPosY() == pacman.GetPosY())
                            {
                                pacman.LoseLife();
                                MovePlayer("reset");
                                LoadGUI();
                            }
                        }
                        break;
                    case "down":
                        if (ghostList1[i].CheckDown(ghostList1, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), border))
                        {
                            ghostList1[i].MoveDown();
                            if (ghostList1[i].GetPosX() == pacman.GetPosX() && ghostList1[i].GetPosY() == pacman.GetPosY())
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
           // Ghost ghost = new Ghost();
          //  if (ghost.count < 10)
          //  {
             //   ghost.addGhost(); 
            //for (int i = 1; i <= (speed - speed / 3)/50;i++ )

      
           // ghostList11.Concat(Enumerable.Repeat(new Ghost, 1)).ToArray();
                map = new Map();
                
                if (pacman.GetScore() >= 684*box && speed>50)//&& ghost.count == 10)
                {
                    addGhost();
                    box++;
                    border = map.GetMap;
                    RedrawMap();
                    MovePlayer("reset");
                    pacman.addLevel();
                    if (pacman.GetLevel()==5)
                    {
                        loop = false;
                        WinGame();
                    }
                    pacman.EarnPoint();
                    speed -= 20;
                }
            //}
        }

        static void CheckIfNoLives()
        {
            if (pacman.Lives() < 0)
            {
                loop = false;
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
        static void addGhost()
        {
              Array.Resize(ref ghostList1, ghostList1.Length+1);
              ghostList1[ghostList1.Length-1] = new Ghost(ConsoleColor.Blue, 15, 11);        
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
