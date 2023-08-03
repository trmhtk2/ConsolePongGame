using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

class GameObject
{
    public int x, y, rotation;

    public GameObject(int x, int y)
    {
        this.x = x; this.y = y;
    }

    public GameObject(int x, int y, int rotation)
    {
        this.x = x; this.y = y; this.rotation = rotation;
    }

    // This method rotates the current GameObject to face another GameObject
    public void RotateTowards(GameObject target)
    {
        // Get the direction (dx, dy) towards the target GameObject
        int dx = target.x - this.x;
        int dy = target.y - this.y;

        // Calculate the rotation angle in radians
        double angleRadians = Math.Atan2(dy, dx);

        // Convert the rotation to degrees
        this.rotation = (int)(angleRadians * 180.0 / Math.PI);

        // normalize the rotation to between 0 and 360
        this.rotation = this.rotation % 360;
        if (this.rotation < 0)
        {
            this.rotation += 360;
        }
    }
}


class Program
{
    static readonly string[] MEDIUM_NUMBERS = new string[10] {"\r\n█▀▀█ \r\n█▄▀█ \r\n█▄▄█" /*0*/, "\r\n▄█  \r\n █  \r\n▄█▄" /*1*/ , "\r\n█▀█ \r\n ▄▀ \r\n█▄▄" /*2*/,
        "█▀▀█ \r\n  ▀▄ \r\n█▄▄█" /*3*/, "\r\n █▀█  \r\n█▄▄█▄ \r\n   █ " /*4*/, "\r\n█▀▀ \r\n▀▀▄ \r\n▄▄▀" /*5*/,
        "\r\n▄▀▀▄ \r\n█▄▄  \r\n▀▄▄▀" /*6*/,  "\r\n▀▀▀█ \r\n  █  \r\n ▐▌ " /*7*/,"\r\n▄▀▀▄ \r\n▄▀▀▄ \r\n▀▄▄▀" /*8*/,
        "\r\n▄▀▀▄ \r\n▀▄▄█ \r\n░▄▄▀" /*9*/};

    static readonly string YOU_WON = "\r\n██╗░░░██╗░█████╗░██╗░░░██╗  ░██╗░░░░░░░██╗░█████╗░███╗░░██╗██╗\r\n╚██╗░██╔╝██╔══██╗██║░░░██║  ░██║░░██╗░░██║██╔══██╗████╗░██║██║\r\n░╚████╔╝░██║░░██║██║░░░██║  ░╚██╗████╗██╔╝██║░░██║██╔██╗██║██║\r\n░░╚██╔╝░░██║░░██║██║░░░██║  ░░████╔═████║░██║░░██║██║╚████║╚═╝\r\n░░░██║░░░╚█████╔╝╚██████╔╝  ░░╚██╔╝░╚██╔╝░╚█████╔╝██║░╚███║██╗\r\n░░░╚═╝░░░░╚════╝░░╚═════╝░  ░░░╚═╝░░░╚═╝░░░╚════╝░╚═╝░░╚══╝╚═╝";
    static readonly string YOU_LOST = "\r\n██╗░░░██╗░█████╗░██╗░░░██╗  ██╗░░░░░░█████╗░░██████╗████████╗██╗\r\n╚██╗░██╔╝██╔══██╗██║░░░██║  ██║░░░░░██╔══██╗██╔════╝╚══██╔══╝██║\r\n░╚████╔╝░██║░░██║██║░░░██║  ██║░░░░░██║░░██║╚█████╗░░░░██║░░░██║\r\n░░╚██╔╝░░██║░░██║██║░░░██║  ██║░░░░░██║░░██║░╚═══██╗░░░██║░░░╚═╝\r\n░░░██║░░░╚█████╔╝╚██████╔╝  ███████╗╚█████╔╝██████╔╝░░░██║░░░██╗\r\n░░░╚═╝░░░░╚════╝░░╚═════╝░  ╚══════╝░╚════╝░╚═════╝░░░░╚═╝░░░╚═╝";

    static readonly string TETRIS_LOGO = "████████╗███████╗████████╗██████╗ ██╗██████╗\r\n╚══██╔══╝██╔════╝╚══██╔══╝██╔══██╗██║██╔════╝\r\n   ██║   █████╗      ██║  ██████╔╝██║╚█████╗ \r\n   ██║   ██╔══╝      ██║  ██╔══██╗██║ ╚═══██╗\r\n   ██║   ███████╗    ██   ██║  ██║██║██████╔╝\r\n   ╚═╝   ╚══════╝    ╚═╝  ╚═╝  ╚═╝╚═╝╚═════╝ ";
    static readonly string PONG_LOGO = "██████╗  █████╗ ███╗  ██╗  █████╗ \r\n██╔══██╗██╔══██╗████╗ ██║██╔════╝ \r\n██████╔╝██║  ██║██╔██╗██║██║  ██╗ \r\n██╔═══╝ ██║  ██║██║╚████║██║  ╚██╗\r\n██║     ╚█████╔╝██║ ╚███║╚██████╔╝\r\n╚═╝      ╚════╝ ╚═╝  ╚══╝ ╚═════╝ ";
    static readonly string BIG_DIFFICULTY = "\r\n█▀▄ █ █▀▀ █▀▀ █ █▀▀ █ █ █   ▀█▀ █▄█\r\n█▄▀ █ █▀  █▀  █ █▄▄ █▄█ █▄▄  █   █ ";
    static readonly string BIG_START_TEXT = "▒█▀▀█ ▒█▀▀█ ▒█▀▀▀ ▒█▀▀▀█ ▒█▀▀▀█ 　 ▒█▀▀▀█ ▒█▀▀█ ░█▀▀█ ▒█▀▀█ ▒█▀▀▀ 　 ▀▀█▀▀ ▒█▀▀▀█ 　 ▒█▀▀▀█ ▀▀█▀▀ ▒█▀▀█ ▒█▀▀█ ▀▀█▀▀ \r\n▒█▄▄█ ▒█▄▄▀ ▒█▀▀▀  ▀▀▀▄▄  ▀▀▀▄▄ 　  ▀▀▀▄▄ ▒█▄▄█ ▒█▄▄█ ▒██   ▒█▀▀▀ 　   █   ▒█   █ 　  ▀▀▀▄▄  ▒█   ▒█▄▄█ ▒█▄▄▀   █  \r\n▒█    ▒█ ▒█ ▒█▄▄▄ ▒█▄▄▄█ ▒█▄▄▄█ 　 ▒█▄▄▄█ ▒█    ▒█ ▒█ ▒█▄▄█ ▒█▄▄▄ 　   █   ▒█▄▄▄█ 　 ▒█▄▄▄█  ▒█   ▒█ ▒█ ▒█ ▒█   █ ";

    static readonly string BIG_GOAL = "\r\n░██████╗░░█████╗░░█████╗░██╗░░░░░██╗\r\n██╔════╝░██╔══██╗██╔══██╗██║░░░░░██║\r\n██║░░██╗░██║░░██║███████║██║░░░░░██║\r\n██║░░╚██╗██║░░██║██╔══██║██║░░░░░╚═╝\r\n╚██████╔╝╚█████╔╝██║░░██║███████╗██╗\r\n░╚═════╝░░╚════╝░╚═╝░░╚═╝╚══════╝╚═╝";
    static readonly string BIG_1 = "\r\n─████████──────\r\n─██░░░░██──────\r\n─████░░██──────\r\n───██░░██──────\r\n───██░░██──────\r\n───██░░██──────\r\n───██░░██──────\r\n───██░░██──────\r\n─████░░████────\r\n─██░░░░░░██────\r\n─██████████";
    static readonly string BIG_2 = "\r\n─██████████████─\r\n─██░░░░░░░░░░██─\r\n─██████████░░██─\r\n─────────██░░██─\r\n─██████████░░██─\r\n─██░░░░░░░░░░██─\r\n─██░░██████████─\r\n─██░░██─────────\r\n─██░░██████████─\r\n─██░░░░░░░░░░██─\r\n─██████████████";
    static readonly string BIG_3 = "\r\n─██████████████─\r\n─██░░░░░░░░░░██─\r\n─██████████░░██─\r\n─────────██░░██─\r\n─██████████░░██─\r\n─██░░░░░░░░░░██─\r\n─██████████░░██─\r\n─────────██░░██─\r\n─██████████░░██─\r\n─██░░░░░░░░░░██─\r\n─██████████████";
    static readonly string BIG_GO = "\r\n─██████████████─██████████████─██████─\r\n─██░░░░░░░░░░██─██░░░░░░░░░░██─██░░██─\r\n─██░░██████████─██░░██████░░██─██░░██─\r\n─██░░██─────────██░░██──██░░██─██░░██─\r\n─██░░██─────────██░░██──██░░██─██░░██─\r\n─██░░██──██████─██░░██──██░░██─██░░██─\r\n─██░░██──██░░██─██░░██──██░░██─██████─\r\n─██░░██──██░░██─██░░██──██░░██────────\r\n─██░░██████░░██─██░░██████░░██─██████─\r\n─██░░░░░░░░░░██─██░░░░░░░░░░██─██░░██─\r\n─██████████████─██████████████─██████─";
    static readonly string PLAYER_SHAPE = new string('█', 40);
    static readonly string BALL_SHAPE = "O";

    static int difficultyLevel = 3;
    static int ballUpdateSpeed = 300;
    static int enemyUpdateSpeed = 700;
    static int targetScore = 2;
    static void Main()
    {
        Console.SetWindowSize(80, 60);
        Console.SetBufferSize(85, 65);
        Console.ForegroundColor = ConsoleColor.White;

        //  LoadingSection(200, 5);

        StartTitleScreen();
    }

    public static void StartTitleScreen()
    {
        Console.CursorVisible = false;

        //Show tetris logo
        WriteCentered(PONG_LOGO, 18, 0, ConsoleColor.DarkGray);
        Thread.Sleep(100);
        WriteCentered(PONG_LOGO, 18, 0, ConsoleColor.Gray);
        Thread.Sleep(100);
        WriteCentered(PONG_LOGO, 18, 0, ConsoleColor.White);

        //Show Settings text
        WriteCentered(BIG_DIFFICULTY, -6, 0, ConsoleColor.DarkRed);
        SetDifficultyLevelText(difficultyLevel);

        WriteCentered("Ｃ ｏ ｎ ｔ ｒ ｏ ｌ ｓ ：", -18, 0, ConsoleColor.DarkBlue);
        WriteCentered("← → for moving and P for pausing", -19, 0, ConsoleColor.Blue);
        WriteCentered($"You must reach {targetScore} points before your opponent", -20, 0, ConsoleColor.Blue);
        WriteCentered("Ｐ Ｒ Ｅ Ｓ Ｓ  Ｓ Ｐ Ａ Ｃ Ｅ  Ｔ Ｏ  Ｓ Ｔ Ａ Ｒ Ｔ", -25, 0, ConsoleColor.Green);
        WriteCentered("© All rights resolved to Golden Dragon (AAA) 2023", -30, 0, ConsoleColor.DarkRed);

        while (true)
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.Spacebar:
                    Console.Clear();
                    StartGameScreen();
                    break;
                case ConsoleKey.LeftArrow:
                    if (difficultyLevel > 1)
                    {
                        difficultyLevel--;
                        SetDifficultyLevelText(difficultyLevel);
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (difficultyLevel < 5)
                    {
                        difficultyLevel++;
                        SetDifficultyLevelText(difficultyLevel);
                    }
                    break;
                default:
                    break;
            }

        }
    }

    public static void SetDifficultyLevelText(int level)
    {

        int leftPosition = (Console.WindowWidth) / 2;
        int topPosition = (Console.WindowHeight) / 2;


        ConsoleColor color = ConsoleColor.Red;

        switch (level)
        {
            case 1:
                ballUpdateSpeed = 500;
                enemyUpdateSpeed = 1200;
                WriteCentered("   Super-Easy   ", -12, 0, ConsoleColor.Green);
                color = ConsoleColor.Green;
                break;
            case 2:
                ballUpdateSpeed = 400;
                enemyUpdateSpeed = 850;
                WriteCentered("   Easy   ", -12, 0, ConsoleColor.DarkGreen);
                color = ConsoleColor.DarkGreen;
                break;
            case 3:
                ballUpdateSpeed = 300;
                enemyUpdateSpeed = 800;
                WriteCentered("   Normal   ", -12, 0, ConsoleColor.Yellow);
                color = ConsoleColor.Yellow;
                break;
            case 4:
                ballUpdateSpeed = 200;
                enemyUpdateSpeed = 300;
                WriteCentered("   Hard   ", -12, 0, ConsoleColor.DarkRed);
                color = ConsoleColor.DarkRed;
                break;
            case 5:
                ballUpdateSpeed = 200;
                enemyUpdateSpeed = 190;
                WriteCentered("   Impossible   ", -12, 0, ConsoleColor.Red);
                color = ConsoleColor.Red;
                break;
            default:
                break;
        }

        ClearArea(17, leftPosition, topPosition - 8);

        WriteCentered("Control using ← →", -6, 0, color);
        WriteCentered(DrawRectangle(40, 5), -8, 0, color);
        WriteCentered("→", -8, -10, color);
        WriteCentered("←", -8, 10, color);
        if(level >= 5)  {
            WriteCentered(" ", -8, -10, ConsoleColor.DarkRed);
        } else if(level <= 1) {
            WriteCentered(" ", -8, 10, ConsoleColor.DarkRed);

        }

        WriteCentered(ToMeduimFont(level.ToString(), MEDIUM_NUMBERS), -8, 0, color);
    }

    public static void StartGameScreen()
    {
        Random random = new Random();
        GameObject player1 = CreateNewPlayer(random.Next(20, 60), 2);
        GameObject player2 = CreateNewPlayer(random.Next(20, 60), 58);
        GameObject ball = new GameObject(Console.WindowWidth / 2, Console.WindowHeight / 2, random.Next(0, 2));
        if (ball.rotation < .5)
        {
            ball.rotation = 45;
        }
        else
        {
            ball.rotation = 90 + 45;
        }

        Console.CursorVisible = false;

        int frameCount = 0;

        Countdown();


        Console.SetCursorPosition(ball.x, ball.y);
        Console.Write(BALL_SHAPE);

        int player1Score = 0;
        int player2Score = 0;

        WriteCentered(ToMeduimFont(player1Score.ToString(), MEDIUM_NUMBERS), 20, -30);
        WriteCentered(ToMeduimFont(player2Score.ToString(), MEDIUM_NUMBERS), -20, -30);

        int horizonalInputPlayer1 = 0;
        int horizonalInputPlayer2 = random.Next(0, 2);
        if (horizonalInputPlayer2 > .5f)
        {
            horizonalInputPlayer2 = 1;
        }
        else
        {
            horizonalInputPlayer2 = -1;
        }


        //GAME LOOP
        while (true)
        {
            int previousBallX = ball.x;
            int previousBallY = ball.y;
            if (Console.CursorVisible == true)
            {
                Console.CursorVisible = false;
            }


            while (Console.KeyAvailable)
            {

                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Note: The true argument suppresses the key from being displayed


                switch (keyInfo.Key)
                {
                    case ConsoleKey.RightArrow:
                        horizonalInputPlayer1 = 1;
                        break;
                    case ConsoleKey.D:
                        horizonalInputPlayer1 = 1;
                        break;
                    case ConsoleKey.A:
                        horizonalInputPlayer1 = -1;
                        break;
                    case ConsoleKey.LeftArrow:
                        horizonalInputPlayer1 = -1;
                        break;
                    case ConsoleKey.R:
                        ball.x = Console.WindowWidth / 2;
                        ball.y = Console.WindowHeight / 2;
                        ball.rotation = random.Next(35, 90 + 35);
                        RedrawBall(ball.x, previousBallX, ball.y, previousBallY);
                        break;
                }


            }
            if (frameCount % ballUpdateSpeed == 0)
            {

                double rotationInRadians = Math.PI * ball.rotation / 180.0;
                double dx = (int)Math.Round(Math.Cos(rotationInRadians));
                double dy = (int)Math.Round(Math.Sin(rotationInRadians));


                ball.x += (int)Math.Round(dx);
                ball.y += (int)Math.Round(dy);



                if (ball.x <= 1)
                {
                    dx = Math.Abs(dx);  // Ball should move right
                }
                else if (ball.x >= Console.WindowWidth - 1)
                {
                    dx = -Math.Abs(dx); // Ball should move left
                }

                if (ball.y < 1)
                {
                    dy = -dy;
                    player2Score++;
                    NewScore(ref ball, player1Score, player2Score, targetScore, ref player1, ref player2);
                }
                if (ball.y >= Console.WindowHeight - 1)
                {
                    dy = -dy;
                    player1Score++;
                    NewScore(ref ball, player1Score, player2Score, targetScore, ref player1, ref player2);
                }

                if (CheckCollision(ball, player1))
                {
                    // if the player is moving to the right
                    if (horizonalInputPlayer1 > 0)
                    {
                        // make the ball move to the right
                        dx = Math.Abs(-dx);

                    }
                    // if the player is moving to the left
                    else if (horizonalInputPlayer1 < 0)
                    {
                        // make the ball move to the left
                        dx = -Math.Abs(+dx);

                    }
                    dy = -dy; // invert y direction of the ball
                }

                if (CheckCollision(ball, player2))
                {
                    // if the player is moving to the right
                    if (horizonalInputPlayer2 > 0)
                    {
                        // make the ball move to the right
                        dx = Math.Abs(dx);
                    }
                    // if the player is moving to the left
                    else if (horizonalInputPlayer2 < 0)
                    {
                        // make the ball move to the left
                        dx = -Math.Abs(dx);
                    }
                    dy = -dy; // invert y direction of the ball

                }

                ball.rotation = (int)Math.Round(Math.Atan2(dy, dx) * 180.0 / Math.PI);

                /* if (ball.rotation < 0)
                  {
                      ball.rotation += 360;
                  } */
                RedrawBall(ball.x, previousBallX, ball.y, previousBallY);

            }



            int buffer = -22;

            if (horizonalInputPlayer1 > 0)
            {
                if (player1.x < Console.WindowWidth - (buffer) - PLAYER_SHAPE.Length)
                {
                    ClearLine(player1.y);
                    player1.x += horizonalInputPlayer1;
                }
            }
            if (horizonalInputPlayer1 < 0)
            {
                if (player1.x > buffer + PLAYER_SHAPE.Length)
                {
                    ClearLine(player1.y);
                    player1.x += horizonalInputPlayer1;
                }
            }

            int player2PreX = player2.x;
            if (frameCount % enemyUpdateSpeed == 0)
            {
                // handle player2 movement
                //    ClearLine(player2.y);
                if (ball.x > player2.x)
                {
                    player2.x++;
                    horizonalInputPlayer2 = 1;
                    if (!(player2.x < Console.WindowWidth - (buffer) - PLAYER_SHAPE.Length))
                    {
                    }
                }
                if (ball.x < player2.x)
                {
                    player2.x--;
                    horizonalInputPlayer2 = -1;
                    if (!(player2.x > buffer + PLAYER_SHAPE.Length))
                    {
                    }
                }

            }
            if (player2PreX != player2.x)
            {
                ClearLine(player2.y);
            }
            DrawPlayerInPosition(player1.x, player1.y);
            //DrawBoundingBox(player1, PLAYER_SHAPE);

            DrawPlayerInPosition(player2.x, player2.y);
            //DrawBoundingBox(player2, PLAYER_SHAPE);


            horizonalInputPlayer1 = 0;
            // horizonalInputPlayer2 = 0;

            frameCount++;
        }

    }

    public static void ClearArea(int size, int x, int y)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Console.SetCursorPosition(x + j, y + i);
                Console.Write(" ");
            }
        }
    }

    static string DrawRectangle(int width, int height)
    {
        //גם פונקציה זו הועתקה מגוגל על ידי טובי המוחות
        if (width < 2 || height < 2)
        {
            return "Both the width and the height should be at least 2 for it to form a rectangle.";
        }

        StringBuilder sb = new StringBuilder();

        // Draw top border
        sb.AppendLine(new string('-', width));

        // Draw sides
        for (int i = 0; i < height - 2; i++)
        {
            sb.AppendLine("|" + new string(' ', width - 2) + "|");
        }

        // Draw bottom border
        sb.AppendLine(new string('-', width));

        return sb.ToString();
    }

    public static void DrawPlayerInPosition(int x, int y, bool includeClearing = false, int clearingLine = 0)
    {
        int xPos = Math.Max(0, x - PLAYER_SHAPE.Length / 2);
        xPos = Math.Min(Console.WindowWidth - PLAYER_SHAPE.Length, xPos);

        if (includeClearing)
        {
            ClearLine(clearingLine);
        }
        Console.SetCursorPosition(xPos, y);
        Console.WriteLine(PLAYER_SHAPE);
    }
    public static void RedrawBall(int x, int preX, int y, int preY)
    {
        Console.SetCursorPosition(preX, preY);
        Console.Write("  ");
        Console.SetCursorPosition(x, y);
        Console.Write(BALL_SHAPE);
    }

    public static string ToMeduimFont(string number, string[] mediumNumbers)
    {
        string bigNumber = "";

        foreach (char digit in number)
        {
            if (Char.IsDigit(digit))
            {
                string addedSpace = "";
                int index = int.Parse(digit.ToString());
                bigNumber += mediumNumbers[index] + addedSpace;
            }
            else
            {
                return "Error: Invalid input. Please enter only numbers.";
            }
        }

        return bigNumber.Trim();  // Trim the trailing space
    }
    public static void Countdown()
    {
        // Thread.Sleep(1000);
        WriteCentered(BIG_3, 0, 0, ConsoleColor.Green);
        Console.Beep(1000, 800);
        //3
        Thread.Sleep(200);
        WriteCentered(BIG_2, 0, 0, ConsoleColor.Yellow);
        Console.Beep(1000, 800);
        //2
        Thread.Sleep(200);
        WriteCentered(BIG_1, 0, 0, ConsoleColor.Red);
        Console.Beep(1000, 800);
        //1
        Thread.Sleep(200);
        WriteCentered(BIG_GO, 0, 0, ConsoleColor.Red);
        Console.Beep(100, 800);
        Thread.Sleep(200);
        ClearCenter(40);
        //GO!
        Console.ForegroundColor = ConsoleColor.White;

    }

    public static void NewScore(ref GameObject ball, int p1s, int p2s, int targetPoints, ref GameObject player1, ref GameObject player2)
    {
        Console.SetCursorPosition(ball.x, ball.y);
        Console.Write(" ");

        ClearScoreArea(); // Clear the area for the scores
        if (p1s >= targetPoints)
        {
            //You won!
            WriteCentered(YOU_WON, 0, 0, ConsoleColor.Green);

            System.Threading.Thread.Sleep(5000);
            Console.Clear();
            StartTitleScreen();
        }
        else if (p2s >= targetPoints)
        {
            //You lost!
            WriteCentered(YOU_LOST, 0, 0, ConsoleColor.Red);

            System.Threading.Thread.Sleep(5000);
            Console.Clear();
            StartTitleScreen();
        }
        
        Random random = new Random();

        WriteCentered(ToMeduimFont(p1s.ToString(), MEDIUM_NUMBERS), 20, -30);
        WriteCentered(ToMeduimFont(p2s.ToString(), MEDIUM_NUMBERS), -20, -30);
        player1.x = random.Next(20, 60);
        player1.y = 2;
        player2.x = random.Next(20, 60);
        player2.y = 58;


        ball.x = Console.WindowWidth / 2;
        ball.y = Console.WindowHeight / 2;
        ball.rotation = random.Next(0, 2);


        if (ball.rotation < .5)
        {
            ball.RotateTowards(player1);
        }
        else
        {
            ball.RotateTowards(player2);
        }

        WriteCentered(BIG_GOAL, 0, 0, ConsoleColor.Yellow);
        Console.Beep(600, 800);
        Thread.Sleep(100);
        ClearLine(player1.y);
        DrawPlayerInPosition(player1.x, player1.y);
        ClearLine(player2.y);
        DrawPlayerInPosition(player2.x, player2.y);
        ClearCenter(50);
        Countdown();
    }
    public static GameObject CreateNewPlayer(int x, int y, bool instantDraw = true)
    {
        GameObject player = new GameObject(x, y);
        if (instantDraw)
        {
            DrawPlayerInPosition(x, y);
        }
        return player;
    }
    public static List<string> WriteCentered(string text, int downPad = 0, int leftPad = 0, ConsoleColor color = ConsoleColor.White)
    {
        List<string> list = new List<string>();
        var lines = text.Split('\n');
        int textHeight = lines.Length;
        int textWidth = lines.Max(line => line.Length);
        int leftPosition = (Console.WindowWidth - textWidth) / 2;
        int topPosition = (Console.WindowHeight - textHeight) / 2;

        Console.SetCursorPosition(leftPosition - leftPad, topPosition - downPad);
        Console.ForegroundColor = color;
        foreach (string line in lines)
        {
            Console.SetCursorPosition(leftPosition - leftPad, Console.CursorTop);
            Console.WriteLine(line);
            list.Add(line);
        }
        Console.ResetColor();
        return list;
    }

    public static void ClearLine(int line)
    {
        int oldCursorPosition = Console.CursorTop;
        int oldCursorPositionX = Console.CursorLeft;
        Console.SetCursorPosition(0, line);
        Console.WriteLine(GetEmptyString());
        Console.CursorTop = oldCursorPosition;
    }

    public static void ClearScoreArea()
    {
        for (int i = 5; i < 55; i++) // you can change '5' to the height of the score area
        {
            ClearLine(i);

        }
    }



    public static void ClearCenter(int offsetSize, int offsetX = 0, int offsetY = 0)
    {
        int leftPosition = ((Console.WindowWidth - offsetSize) / 2) + offsetX;
        int topPosition = ((Console.WindowHeight - offsetSize) / 2) + offsetY;
        for (int i = topPosition; i < topPosition + offsetSize; i++)
        {
            for (int j = leftPosition; j < leftPosition + offsetSize; j++)
            {
                /*if (j > 1 && j < Console.BufferWidth - 1 && i > 1 && i < Console.BufferHeight - 1) 
                { */

                Console.SetCursorPosition(j, i);
                Console.Write(" ");
                //}
            }
        }
    }

    public static bool CheckCollision(GameObject ball, GameObject player)
    {
        // Calculate the bounding box for the ball
        int ballLeft = ball.x;
        int ballRight = ball.x + BALL_SHAPE.Length;
        int ballTop = ball.y;
        int ballBottom = ball.y + 1;

        // Calculate the bounding box for the player
        int playerLeft = player.x - PLAYER_SHAPE.Length / 2 - 1;
        int playerRight = player.x + PLAYER_SHAPE.Length / 2 + 1;
        int playerTop = player.y;
        int playerBottom = player.y + 1;

        // Check for collision
        return ballLeft < playerRight && ballRight > playerLeft && ballTop < playerBottom && ballBottom > playerTop;
    }

    public static void DrawBoundingBox(GameObject obj, string shape)
    {
        // Calculate the bounding box for the object
        int objLeft = obj.x - shape.Length / 2 - 1;
        int objRight = objLeft + shape.Length + 1;
        int objTop = obj.y;
        int objBottom = obj.y + 1;

        // Draw the bounding box
        for (int x = objLeft; x < objRight; x++)
        {
            Console.SetCursorPosition(x, objTop - 1);
            Console.Write('+');
            Console.SetCursorPosition(x, objBottom);
            Console.Write('+');
        }
        for (int y = objTop; y < objBottom + 1; y++)
        {
            Console.SetCursorPosition(objLeft - 1, y);
            Console.Write('+');
            Console.SetCursorPosition(objRight, y);
            Console.Write('+');
        }
    }


    public static string GetEmptyString()
    {
        return new string(' ', Console.WindowWidth);
    }
    public static void LoadingSection(int wait, int repeatTimes)
    {
        Console.CursorVisible = false;
        for (int i = 0; i < repeatTimes; i++)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("Loading    ");
            System.Threading.Thread.Sleep(wait);

            Console.SetCursorPosition(0, 0);
            Console.Write("Loading.   ");
            System.Threading.Thread.Sleep(wait);

            Console.SetCursorPosition(0, 0);
            Console.Write("Loading..  ");
            System.Threading.Thread.Sleep(wait);

            Console.SetCursorPosition(0, 0);
            Console.Write("Loading... ");
            System.Threading.Thread.Sleep(wait);

            Console.SetCursorPosition(0, 0);
        }
        Console.Clear();
    }

}
