using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Pacman
{
    class Program
    {
        //static int level = 0;
        static Pause pause = new Pause();
        static int box = 1;// uy dawj suns hurd zergiig nemehed heregtei bolsn tooluur maygiin zuil
        static Random random = new Random();  //sunsnii hiih hodolgoon random baina
        static bool loop = true; // main funkts dotorh while gol dawtaltiig urgejluuleh zogsooh gol huwisagch
        static PacMan pacman = new PacMan();
        static int ghostPosIndex = 4;
        static int speed = 200; // togloomnii hurdnii anhnii utga
        static Ghost[] ghostList1 = new Ghost[4] // sunsnii anhnii utga anh 4 suns  ogogdono
        {
            new Ghost(getRandomConsoleColor(),Ghost.ghostSpanX[0],Ghost.ghostSpanY[0]),
            new Ghost(getRandomConsoleColor(),Ghost.ghostSpanX[1],Ghost.ghostSpanY[1]),
            new Ghost(getRandomConsoleColor(),Ghost.ghostSpanX[2],Ghost.ghostSpanY[2]),
            new Ghost(getRandomConsoleColor(),Ghost.ghostSpanX[3],Ghost.ghostSpanY[3]),
            
        };
        static ConsoleColor getRandomConsoleColor()
        {
            var ConsoleColors = Enum.GetValues(typeof(ConsoleColor));
            return (ConsoleColor)ConsoleColors.GetValue(random.Next(ConsoleColors.Length));
        }
        // Map
        static Map map = new Map();
        static string[,] border = map.GetMap;  //border huwisagchid map aa hadgalj map deeree oorchlolt oruulahad ashiglasan 
        // Console Settings
        const int GAMEWIDTH = 70;
        const int GAMEHEIGHT = 29;

        public static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "Pacman";
            Console.WindowWidth = GAMEWIDTH; // console-iin orgond ooriinhoo deer zarlasan gamewidth const utgatai huwisagchiin  utgaar hemjeeg n zaaj ogson
            Console.BufferWidth = GAMEWIDTH;
            Console.WindowHeight = GAMEHEIGHT;// ondor-iin hemjeeg olgoson
            Console.BufferHeight = GAMEHEIGHT;
            ShowWelcomeMenu();// ug funkts n togloom ehlehed garch ireh x darj togloom ehluuleh eswel escape darj garah gsn txt-g haruulna

            RedrawMap(); //map-g achaallaj delgetsend zurah funkts

            LoadPlayer();// pacman buyu toglochiig achaallaj unshih funkts

            Loadghosts();// ghost-iig achaallan unshih funkts

            while (loop)// gol dawtalt
            {

                ReadUserKey();// toglogch keyboardnii ymr towch darj baigaag unshih funkts
                if (pause.gameP()) // togloom pause hiigdsen eseh
                {
                    pause.blinkPausedText();//pause hiigdsen bol paused gej garah text-g aniwchuulah funkts
                    continue;
                }

                ghostAi();// sunsnii hodolgoon hiih zereg sunstei holbootoi buhii l zuilg hj bui funkts

                PlayerMovement();// toglogchiin hodolgoon
                LoadGUI();
                CheckIfNoLives();// ami baiga esehg shalgaad baixgvi bol dawtalt zogsooj togloom duusgana

                CheckScore();// onoog shalgah funkts

                Thread.Sleep(speed);// hurdiig taaruulj ogson thread
            }
        }
        static void LoadPlayer()// toglogchiin bairshil ongo durs zergiig unshij dursleh funkts
        {
            Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
            Console.ForegroundColor = pacman.GetColor();
            Console.Write(pacman.GetPac());
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
            Console.SetCursorPosition(40, GAMEHEIGHT - 8);
            Console.Write("{0}", new string('-', 22)); // 22 gdg ni "-" ene temdegt 22 udaa durslegdene gesen ug
            Console.SetCursorPosition(40, GAMEHEIGHT - 7);
            Console.Write("|  PRESS P TO PAUSE  |");
            Console.SetCursorPosition(40, GAMEHEIGHT - 6);
            Console.Write("|  PRESS ESC TO EXIT |");
            Console.SetCursorPosition(40, GAMEHEIGHT - 5);
            Console.Write("{0}", new string('-', 22));
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
        static void ghostOmnohPosition(Ghost ghost)
        {
            Console.SetCursorPosition(ghost.omnohPosX, ghost.omnohPosY);
        }
        static void Moveghost()// sunsee hodolgoh funkts
        {
            foreach (var ghost in ghostList1)
            {
                Console.ForegroundColor = ghost.GetColor();
                Console.SetCursorPosition(ghost.GetPosX(), ghost.GetPosY());
                Console.Write(ghost.GetGhost());
                Console.ForegroundColor = ConsoleColor.White;//herwee tsagaan ongoor ogoogui bol sunsnii ywsan gazart ymr neg element baih yum bol ug sunsnii ongoor elementuud oorchlogdono
                if (ghost.GetPosX() != ghost.omnohPosX || ghost.GetPosY() != ghost.omnohPosY)// herwee suns bairnaasaa hodolson bol
                {
                    if (border[ghost.omnohPosY, ghost.omnohPosX] == " ")
                    {
                        ghostOmnohPosition(ghost);
                        Console.Write(' ');
                    }
                    else if (border[ghost.omnohPosY, ghost.omnohPosX] == ".")
                    {
                        ghostOmnohPosition(ghost);
                        Console.Write('.');
                    }
                    else if (border[ghost.omnohPosY, ghost.omnohPosX] == "*")
                    {
                        ghostOmnohPosition(ghost);
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
                        pause.setGamePaused();
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
        static void PlayerMovement()
        {
            switch (pacman.CheckCell(border, pacman.NextDirection, ghostList1))
            {
                case MapElements.Dot:
                    MovePlayer(pacman.NextDirection);
                    pacman.EarnPoint();
                    pacman.Direction = pacman.NextDirection;
                    break;
                case MapElements.Star:
                    MovePlayer(pacman.NextDirection);
                    pacman.EarnStar();
                    pacman.Direction = pacman.NextDirection;
                    break;
                case MapElements.Empty:
                    MovePlayer(pacman.NextDirection);
                    pacman.Direction = pacman.NextDirection;
                    break;
                case MapElements.Ghost:
                    pacman.LoseLife();
                    MovePlayer("reset");
                    break;
                case MapElements.Wall:
                    switch (pacman.CheckCell(border, pacman.Direction, ghostList1))
                    {
                        case MapElements.Dot:
                            MovePlayer(pacman.Direction);
                            pacman.EarnPoint();
                            break;
                        case MapElements.Star:
                            MovePlayer(pacman.Direction);
                            pacman.EarnStar();
                            break;
                        case MapElements.Empty:
                            MovePlayer(pacman.Direction);
                            break;
                        case MapElements.Ghost:
                            pacman.LoseLife();
                            MovePlayer("reset");
                            break;
                        case MapElements.Wall:
                            break;
                    }
                    break;
            }
        }
        static void PManTrace()
        {
            Console.SetCursorPosition(pacman.GetPosX(), pacman.GetPosY());
            Console.Write(" ");
        }
        static void PManNewPos(int x, int y)
        {
            Console.SetCursorPosition(pacman.GetPosX() + x, pacman.GetPosY() + y);
            Console.ForegroundColor = pacman.GetColor();
            Console.Write(pacman.GetPac());
        }
        static void MovePlayer(string direction)
        {
            switch (direction)
            {
                case "up":
                    PManTrace();
                    ChangeMap();
                    PManNewPos(0, -1);//x  y
                    pacman.MoveUp();
                    break;
                case "right":
                    PManTrace();
                    ChangeMap();
                    PManNewPos(1, 0);//x  y
                    pacman.MoveRight();
                    break;
                case "down":
                    PManTrace();
                    ChangeMap();
                    PManNewPos(0, 1);//x  y
                    pacman.MoveDown();
                    break;
                case "left":
                    PManTrace();
                    ChangeMap();
                    PManNewPos(-1, 0);//x  y
                    pacman.MoveLeft();
                    break;
                case "reset":
                    PManTrace();
                    ChangeMap();
                    pacman.ResetPacMan();
                    PManNewPos(0, 0);
                    break;
            }
        }
        static void ghostAi()
        {
            for (int i = 0; i < ghostList1.Length; i++)
            {
                if (random.Next(0, 2) != 0)
                {
                    ghostList1[i].Direction = Ghost.zug[random.Next(0, Ghost.zug.Length)];
                }
                switch (ghostList1[i].Direction)
                {
                    case "left":
                        if (ghostList1[i].checkDir(ghostList1, border, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), ghostList1[i].Direction))//ghostList1[i].CheckLeft(ghostList1, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), border))
                        {
                            ghostList1[i].MoveLeft();
                            if (ghostList1[i].GetPosX() == pacman.GetPosX() && ghostList1[i].GetPosY() == pacman.GetPosY())
                            {
                                pacman.LoseLife();
                                MovePlayer("reset");
                            }
                        }
                        break;
                    case "right":
                        if (ghostList1[i].checkDir(ghostList1, border, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), ghostList1[i].Direction))//CheckRight(ghostList1, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), border))
                        {
                            ghostList1[i].MoveRight();
                            if (ghostList1[i].GetPosX() == pacman.GetPosX() && ghostList1[i].GetPosY() == pacman.GetPosY())
                            {
                                pacman.LoseLife();
                                MovePlayer("reset");
                            }
                        }
                        break;
                    case "up":
                        if (ghostList1[i].checkDir(ghostList1, border, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), ghostList1[i].Direction))//.CheckUp(ghostList1, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), border))
                        {
                            ghostList1[i].MoveUp();
                            if (ghostList1[i].GetPosX() == pacman.GetPosX() && ghostList1[i].GetPosY() == pacman.GetPosY())
                            {
                                pacman.LoseLife();
                                MovePlayer("reset");
                            }
                        }
                        break;
                    case "down":
                        if (ghostList1[i].checkDir(ghostList1, border, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), ghostList1[i].Direction))//ghostList1[i].CheckDown(ghostList1, ghostList1[i].GetPosX(), ghostList1[i].GetPosY(), border))
                        {
                            ghostList1[i].MoveDown();
                            if (ghostList1[i].GetPosX() == pacman.GetPosX() && ghostList1[i].GetPosY() == pacman.GetPosY())
                            {
                                pacman.LoseLife();
                                MovePlayer("reset");
                            }
                        }
                        break;

                }
            }
            Moveghost();
        }
        static void CheckScore()
        {
            map = new Map();

            if (pacman.GetScore() >= 684 * box && speed > 50)
            {
                addGhost();
                box++;
                border = map.GetMap;
                RedrawMap();
                MovePlayer("reset");
                pacman.addLevel();
                if (pacman.GetLevel() == 5)
                {
                    loop = false;
                    WinGame();
                }
                pacman.EarnPoint();
                speed -= 20;
            }
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

            int horizontalPos = GAMEHEIGHT / 2 - 2;
            int verticalPos = GAMEWIDTH / 2 - 15;

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
            Array.Resize(ref ghostList1, ghostList1.Length + 1);

            ghostList1[ghostList1.Length - 1] = new Ghost(getRandomConsoleColor(), Ghost.ghostSpanX[ghostPosIndex], Ghost.ghostSpanY[ghostPosIndex]);
            ghostPosIndex++;
        }
        static void GameOver()
        {
            Console.Clear();
            RedrawMap();

            int horizontalPos = GAMEHEIGHT / 2 - 2;
            int verticalPos = GAMEWIDTH / 2 - 15;

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

        static void WinGame()
        {
            Console.Clear();
            RedrawMap();

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
            int score = pacman.GetScore();
            Console.Write("||       SCORE: {0}{1}  ||", score, new string(' ', 9 - score.ToString().Length));
            Console.SetCursorPosition(verticalPos, horizontalPos + 4);
            Console.Write("||                         ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 5);
            Console.Write("||    PRESS ESC TO EXIT    ||");
            Console.SetCursorPosition(verticalPos, horizontalPos + 6);
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