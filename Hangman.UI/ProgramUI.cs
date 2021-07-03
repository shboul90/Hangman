using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.UI
{
    //Menu: 1. Choose Category 2. Exit
    //SubMenu 1. Movies 2. Sports 3. Food 4. Exit
    //Display dashes, Number of lives
    //Incorrect Display Hang Man
    // 
    class ProgramUI
    {
        private readonly List<string> _wordBank = new List<string>();
        internal void Run()
        {
            
            while (this.RunMenu()) ;
        }

        private bool RunMenu()
        {
            var task = CycleImagesEveryTen();

            task.Wait();

            Console.Clear();

            this.DisplayMenu();
            
            switch (Console.ReadLine())
            {
                case "1":
                    this.WordBank("1");
                    return true;
                case "2":
                    this.WordBank("2");
                    return true;
                case "3":
                    this.WordBank("3");
                    return true;
                case "4":
                    return false;
                default:
                    this.PrintError("Invalid menu choice! Press any key to retry...");
                    Console.ReadLine();
                    return true;
            }

            
        }
        private void WordBank(string userinput)
        {
            if (userinput=="1")
            {
                string selection = "Movie";
                string wordToGuessUppercase = PullFromMovies();
                Console.Clear();
                StringBuilder displayToPlayer = DisplayWordLength(wordToGuessUppercase);
                StartGame(wordToGuessUppercase, displayToPlayer, selection);

                Console.ReadKey();
            }
            else if (userinput == "2")
            {
                string selection = "Sports Team";
                string wordToGuessUppercase = PullFromSportsTeams();
                Console.Clear();
                StringBuilder displayToPlayer = DisplayWordLength(wordToGuessUppercase);
                StartGame(wordToGuessUppercase, displayToPlayer, selection);

                Console.ReadKey();
            }
            else if (userinput == "3")
            {
                string selection = "Food";
                string wordToGuessUppercase = PullFromFoods();
                Console.Clear();
                StringBuilder displayToPlayer = DisplayWordLength(wordToGuessUppercase);
                StartGame(wordToGuessUppercase, displayToPlayer, selection);

                Console.ReadKey();
            }
        }
        private string PullFromFoods()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            string[] arrayMovies = System.IO.File.ReadAllLines(@"Foods.txt");
            string wordToGuess = arrayMovies[random.Next(0, arrayMovies.Length)];
            string wordToGuessUppercase = wordToGuess.ToUpper();

            return wordToGuessUppercase;
        }
        private string PullFromSportsTeams()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            string[] arrayMovies = System.IO.File.ReadAllLines(@"Sports.txt");
            string wordToGuess = arrayMovies[random.Next(0, arrayMovies.Length)];
            string wordToGuessUppercase = wordToGuess.ToUpper();

            return wordToGuessUppercase;
        }
        private void StartGame(string wordToGuess, StringBuilder displayToPlayer, string category)
        {
            
            List<char> correctGuesses = new List<char>();
            List<char> incorrectGuesses = new List<char>();

            int lives = 6;
            bool won = false;
            int lettersRevealed = 0;

            foreach(int a in wordToGuess)
            {
                if(a == ' ')
                {
                    lettersRevealed++;
                }
                else if (a == '\'')
                {
                    lettersRevealed++;
                }

            }

            string input;
            char guess;

            while (!won && lives > 0)
            {
                HangManModel(lives);

                PrintInColor($"This is the word you need to guess (Remember the Category is {category}):", ConsoleColor.Blue);
                PrintInColor(displayToPlayer.ToString(), ConsoleColor.White);

                PrintInColor($"\nYou have {lives} lives remaining!", ConsoleColor.DarkYellow);

                Console.WriteLine("Here are your incorrect guess:");
                PrintCharInList(incorrectGuesses, ConsoleColor.Red);
                
                PrintInColor($"Guess a Letter or Number for the {category}: ", ConsoleColor.Blue);

                input = Console.ReadLine().ToUpper();

                while (input == "")
                {
                    PrintError("Please enter a valid value");
                    input = Console.ReadLine().ToUpper();
                }

                guess = input[0];

                Console.Clear();

                if (correctGuesses.Contains(guess))
                {
                    Console.WriteLine("You've already tried '{0}', and it was correct!", guess);
                    continue;
                }
                else if (incorrectGuesses.Contains(guess))
                {
                    Console.WriteLine("You've already tried '{0}', and it was wrong!", guess);
                    continue;
                }

                if (wordToGuess.Contains(guess))
                {
                    correctGuesses.Add(guess);

                    for (int i = 0; i < wordToGuess.Length; i++)
                    {
                        if (wordToGuess[i] == guess)
                        {
                            displayToPlayer[i] = wordToGuess[i];
                            lettersRevealed++;
                        }
                    }

                    if (lettersRevealed == wordToGuess.Length)
                        won = true;
                }
                else
                {
                    incorrectGuesses.Add(guess);
                    Console.WriteLine("Nope, there's no '{0}' in it!", guess);
                    lives--;
                }
            }

            HangManModel(lives);
            if (won)
            {
                PrintInColor($"You won! Good job on guess {wordToGuess}", ConsoleColor.Green);
            }
            else
            {
                var task = CycleSwingEveryTen();
                task.Wait();
                PrintInColor($"You lost! It was  {wordToGuess}", ConsoleColor.Red);
            }


            Console.Write("Press ENTER to exit....");
            Console.ReadLine();
        }
        private void HangManModel(int livesLeft)
        {
            switch (livesLeft)
            {
                case 6:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /      ||\n" +
                        "| |/ /       ||\n" +
                        "| | /        ||\n" +
                        "| |/         ||\n" +
                        "| |          ||\n" +
                        "| |          (\\\n" +
                        "| |         \n" +
                        "| |        \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |             \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |         \n" +
                        "----------|_        \n" +
                        "|-|-------| |       \n" +
                        "| |        | |        \n" +
                        ": :         | |       \n",ConsoleColor.Green);
                    break;
                case 5:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /      ||\n" +
                        "| |/ /       ||\n" +
                        "| | /        ||.-''.\n" +
                        "| |/         |/  _  \\\n" +
                        "| |          ||  `/,|\n" +
                        "| |          (\\\\`_.'\n" +
                        "| |         \n" +
                        "| |        \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |             \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |         \n" +
                        "----------|_        \n" +
                        "|-|-------| |       \n" +
                        "| |        | |        \n" +
                        ": :         | |       \n", ConsoleColor.DarkGreen);
                    break;
                case 4:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /      ||\n" +
                        "| |/ /       ||\n" +
                        "| | /        ||.-''.\n" +
                        "| |/         |/  _  \\\n" +
                        "| |          ||  `/,|\n" +
                        "| |          (\\\\`_.'\n" +
                        "| |         .-`--'.\n" +
                        "| |         Y . . Y\n" +
                        "| |          |   | \n" +
                        "| |          | . |  \n" +
                        "| |          |   |   \n" +
                        "| |           --- \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |         \n" +
                        "----------|_\n" +
                        "|-|-------| |\n" +
                        "| |        | |\n" +
                        ": :         | |\n", ConsoleColor.Gray);
                    break;
                case 3:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /      ||\n" +
                        "| |/ /       ||\n" +
                        "| | /        ||.-''.\n" +
                        "| |/         |/  _  \\\n" +
                        "| |          ||  `/,|\n" +
                        "| |          (\\\\`_.'\n" +
                        "| |         .-`--'.\n" +
                        "| |        /Y . . Y\n" +
                        "| |       // |   | \n" +
                        "| |      //  | . |  \n" +
                        "| |     (')  |   |   \n" +
                        "| |           --- \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |         \n" +
                        "----------|_\n" +
                        "|-|-------| |\n" +
                        "| |        | |\n" +
                        ": :         | |\n", ConsoleColor.DarkGray);
                    break;
                case 2:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /      ||\n" +
                        "| |/ /       ||\n" +
                        "| | /        ||.-''.\n" +
                        "| |/         |/  _  \\\n" +
                        "| |          ||  `/,|\n" +
                        "| |          (\\\\`_.'\n" +
                        "| |         .-`--'.\n" +
                        "| |        /Y . . Y\\\n" +
                        "| |       // |   | \\\\\n" +
                        "| |      //  | . |  \\\\\n" +
                        "| |     (')  |   |   (`)\n" +
                        "| |           --- \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |         \n" +
                        "----------|_\n" +
                        "|-|-------| |\n" +
                        "| |        | |\n" +
                        ": :         | |\n", ConsoleColor.Magenta);
                    break;
                case 1:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /      ||\n" +
                        "| |/ /       ||\n" +
                        "| | /        ||.-''.\n" +
                        "| |/         |/  _  \\\n" +
                        "| |          ||  `/,|\n" +
                        "| |          (\\\\`_.'\n" +
                        "| |         .-`--'.\n" +
                        "| |        /Y . . Y\\\n" +
                        "| |       // |   | \\\\\n" +
                        "| |      //  | . |  \\\\\n" +
                        "| |     (')  |   |   (`)\n" +
                        "| |          |--- \n" +
                        "| |          ||\n" +
                        "| |          ||\n" +
                        "| |          ||\n" +
                        "| |         / |\n" +
                        "----------|_`-'\n" +
                        "|-|-------| |\n" +
                        "| |        | |\n" +
                        ": :         | |\n", ConsoleColor.DarkMagenta);
                    break;
                default:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /      ||\n" +
                        "| |/ /       ||\n" +
                        "| | /        ||.-''.\n" +
                        "| |/         |/  _  \\\n" +
                        "| |          ||  `/,|\n" +
                        "| |          (\\\\`_.'\n" +
                        "| |         .-`--'.\n" +
                        "| |        /Y . . Y\\\n" +
                        "| |       // |   | \\\\\n" +
                        "| |      //  | . |  \\\\\n" +
                        "| |     (')  |   |   (`)\n" +
                        "| |          |---|\n" +
                        "| |          || ||\n" +
                        "| |          || ||\n" +
                        "| |          || ||\n" +
                        "| |         / | | \\\n" +
                        "----------|_`-' `-'\n" +
                        "|-|-------| |\n" +
                        "| |        | |\n" +
                        ": :         | |\n",ConsoleColor.DarkRed);
                    break;
                    
            }

        }

        public async Task CycleSwingEveryTen()
        {
            int t = 12;
            int e = 2;
            while (t > 0)
            {
                Console.Clear();

                var delayTask = Task.Delay(200);

                if (e < 0)
                {
                    e = 2;
                }

                CycleSwing(e);

                await delayTask;

                e--;
                t--;
            }

        }

        private void CycleSwing(int livesLeft)
        {
            switch (livesLeft)
            {
                case 0:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /     //\n" +
                        "| |/ /     //\n" +
                        "| | /   .-''.\n" +
                        "| |/   /  _  \\\n" +
                        "| |    |  `/,|\n" +
                        "| |   /\\\\`_.'\n" +
                        "| | .-`--'.\n" +
                        "| |/Y . . Y\\\n" +
                        "| // |   | \\\\\n" +
                        " //  | . |  \\\\\n" +
                        "(')  |   |  (')  \n" +
                        "| |  ||'||\n" +
                        "| |  || ||\n" +
                        "| |  || ||\n" +
                        "| |  || ||\n" +
                        "| | / | | \\\n" +
                        "****`-' `-'_\n" +
                        "|-|-------| |\n" +
                        "| |        | |\n" +
                        ": :         | |\n", ConsoleColor.DarkRed);
                    break;
                case 1:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /      ||\n" +
                        "| |/ /       ||\n" +
                        "| | /        ||.-''.\n" +
                        "| |/         |/  _  \\\n" +
                        "| |          ||  `/,|\n" +
                        "| |          (\\\\`_.'\n" +
                        "| |         .-`--'.\n" +
                        "| |        /Y . . Y\\\n" +
                        "| |       // |   | \\\\\n" +
                        "| |      //  | . |  \\\\\n" +
                        "| |     (')  |   |   (`)\n" +
                        "| |          |---|\n" +
                        "| |          || ||\n" +
                        "| |          || ||\n" +
                        "| |          || ||\n" +
                        "| |         / | | \\\n" +
                        "----------|_`-' `-'\n" +
                        "|-|-------| |\n" +
                        "| |        | |\n" +
                        ": :         | |\n", ConsoleColor.DarkRed);
                    break;
                case 2:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /      \\\\\n" +
                        "| |/ /        \\\\\n" +
                        "| | /          \\\\    .-''.\n" +
                        "| |/            \\\\  /_    \\\n" +
                        "| |              \\\\( `/,  )\n" +
                        "| |               \\\\` _. /\n" +
                        "| |               .-`--'.\n" +
                        "| |             /Y . . Y\\\\\n" +
                        "| |            // \\    \\ \\\\\n" +
                        "| |           //   \\ .  \\ \\\\\n" +
                        "| |          (')    \\    \\ (`)\n" +
                        "| |                  -----\n" +
                        "| |                  \\\\   \\\\\n" +
                        "| |                   \\\\   \\\\\n" +
                        "| |                    \\\\   \\\\\n" +
                        "| |                    / |   \\ \\\n" +
                        "----------|_           `-'    `-'\n" +
                        "|-|-------| |\n" +
                        "| |        | |\n" +
                        ": :         | |\n", ConsoleColor.DarkRed);
                    break;

            }

        }
        public async Task CycleImagesEveryTen()
        {
            int t = 7;
            int e = 6;
            while (t>0)
            {
                Console.Clear();

                var delayTask = Task.Delay(200);

                if (e<0)
                {
                    e = 6;
                }
                
                CycleImages(e);

                await delayTask; 

                e--;
                t--;
            }
            
        }

        private void CycleImages(int livesLeft)
        {
            switch (livesLeft)
            {
                case 6:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /      ||\n" +
                        "| |/ /       ||\n" +
                        "| | /        ||\n" +
                        "| |/         ||\n" +
                        "| |          ||\n" +
                        "| |          (\\\n" +
                        "| |         \n" +
                        "| |        \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |             \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |         \n" +
                        "----------|_        \n" +
                        "|-|-------| |       \n" +
                        "| |        | |        \n" +
                        ": :         | |       \n", ConsoleColor.Green);
                    break;
                case 5:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /      ||\n" +
                        "| |/ /       ||\n" +
                        "| | /        ||.-''.\n" +
                        "| |/         |/  _  \\\n" +
                        "| |          ||  `/,|\n" +
                        "| |          (\\\\`_.'\n" +
                        "| |         \n" +
                        "| |        \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |             \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |         \n" +
                        "----------|_        \n" +
                        "|-|-------| |       \n" +
                        "| |        | |        \n" +
                        ": :         | |       \n", ConsoleColor.DarkGreen);
                    break;
                case 4:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /      ||\n" +
                        "| |/ /       ||\n" +
                        "| | /        ||.-''.\n" +
                        "| |/         |/  _  \\\n" +
                        "| |          ||  `/,|\n" +
                        "| |          (\\\\`_.'\n" +
                        "| |         .-`--'.\n" +
                        "| |         Y . . Y\n" +
                        "| |          |   | \n" +
                        "| |          | . |  \n" +
                        "| |          |   |   \n" +
                        "| |           --- \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |         \n" +
                        "----------|_\n" +
                        "|-|-------| |\n" +
                        "| |        | |\n" +
                        ": :         | |\n", ConsoleColor.Gray);
                    break;
                case 3:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /      ||\n" +
                        "| |/ /       ||\n" +
                        "| | /        ||.-''.\n" +
                        "| |/         |/  _  \\\n" +
                        "| |          ||  `/,|\n" +
                        "| |          (\\\\`_.'\n" +
                        "| |         .-`--'.\n" +
                        "| |        /Y . . Y\n" +
                        "| |       // |   | \n" +
                        "| |      //  | . |  \n" +
                        "| |     (')  |   |   \n" +
                        "| |           --- \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |         \n" +
                        "----------|_\n" +
                        "|-|-------| |\n" +
                        "| |        | |\n" +
                        ": :         | |\n", ConsoleColor.DarkGray);
                    break;
                case 2:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /      ||\n" +
                        "| |/ /       ||\n" +
                        "| | /        ||.-''.\n" +
                        "| |/         |/  _  \\\n" +
                        "| |          ||  `/,|\n" +
                        "| |          (\\\\`_.'\n" +
                        "| |         .-`--'.\n" +
                        "| |        /Y . . Y\\\n" +
                        "| |       // |   | \\\\\n" +
                        "| |      //  | . |  \\\\\n" +
                        "| |     (')  |   |   (`)\n" +
                        "| |           --- \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |          \n" +
                        "| |         \n" +
                        "----------|_\n" +
                        "|-|-------| |\n" +
                        "| |        | |\n" +
                        ": :         | |\n", ConsoleColor.Magenta);
                    break;
                case 1:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /      ||\n" +
                        "| |/ /       ||\n" +
                        "| | /        ||.-''.\n" +
                        "| |/         |/  _  \\\n" +
                        "| |          ||  `/,|\n" +
                        "| |          (\\\\`_.'\n" +
                        "| |         .-`--'.\n" +
                        "| |        /Y . . Y\\\n" +
                        "| |       // |   | \\\\\n" +
                        "| |      //  | . |  \\\\\n" +
                        "| |     (')  |   |   (`)\n" +
                        "| |          |--- \n" +
                        "| |          ||\n" +
                        "| |          ||\n" +
                        "| |          ||\n" +
                        "| |         / |\n" +
                        "----------|_`-'\n" +
                        "|-|-------| |\n" +
                        "| |        | |\n" +
                        ": :         | |\n", ConsoleColor.DarkMagenta);
                    break;
                case 0:
                    PrintInColor(" ___________.._______\n" +
                        "| .__________))______|\n" +
                        "| | / /      ||\n" +
                        "| |/ /       ||\n" +
                        "| | /        ||.-''.\n" +
                        "| |/         |/  _  \\\n" +
                        "| |          ||  `/,|\n" +
                        "| |          (\\\\`_.'\n" +
                        "| |         .-`--'.\n" +
                        "| |        /Y . . Y\\\n" +
                        "| |       // |   | \\\\\n" +
                        "| |      //  | . |  \\\\\n" +
                        "| |     (')  |   |   (`)\n" +
                        "| |          |---|\n" +
                        "| |          || ||\n" +
                        "| |          || ||\n" +
                        "| |          || ||\n" +
                        "| |         / | | \\\n" +
                        "----------|_`-' `-'\n" +
                        "|-|-------| |\n" +
                        "| |        | |\n" +
                        ": :         | |\n", ConsoleColor.DarkRed);
                    break;

            }

        }
        private string PullFromMovies()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            string[] arrayMovies = System.IO.File.ReadAllLines(@"Movies.txt");
            string wordToGuess = arrayMovies[random.Next(0, arrayMovies.Length)];
            string wordToGuessUppercase = wordToGuess.ToUpper();

            return wordToGuessUppercase;
        }
        private StringBuilder DisplayWordLength(string wordToGuess)
        {
            StringBuilder displayToPlayer = new StringBuilder(wordToGuess.Length);

            foreach(char a in wordToGuess)
            {
                if(a == ' ')
                {
                    displayToPlayer.Append(' ');
                }
                else if (a == '\'')
                {
                    displayToPlayer.Append('\'');
                }
                else
                {
                    displayToPlayer.Append('-');
                }
            }
            
            return displayToPlayer;
        }
        private void DisplayMenu()
        {
            Console.Clear();
            PrintInColorDelay(
                $"----------------------------------------------------------------------\n" +
                $"888  888\n" +
                $"888  888\n" +
                $"888  888\n" +
                $"88888888  8888b.  88888b.   .d88b.  88888b.d88b.   8888b.  88888b.\n" +
                $"888 *88b    *88b 888 *88bd 88P*88b 888 *888 *88b     *88b8 88 *88b\n" +
                $"888  888 .d88888 8888  888 888  88 8888  888  888 .d888888 888  888\n" +
                $"888  888 888  88 8888  888 Y88b 88 8888  888  888 888  888 888  888\n" +
                $"888  888 *Y88888 8888  888  *Y8888 8888  888  888 *Y888888 888  888\n" +
                $"                               888\n" +
                $"                          Y8b d88P\n" +
                $"                           *Y88P*\n" +
                $"----------------------------------------------------------------------\n\n", ConsoleColor.DarkYellow, 1);
            PrintInColor(
                $"                  ----------Main Menu---------\n" +
                $"                  **Choose A Category or Exit**\n" +
                $"                           1. Movies\n" +
                $"                           2. Sports\n" +
                $"                           3. Food\n" +
                $"                           4. Exit", ConsoleColor.Blue);
        }
        private void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private void PrintCharInList(List<char> obj, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            foreach (char c in obj)
            {
                Console.Write(c + ", ");
            }
            Console.WriteLine();
            Console.ResetColor();
        }
        private void PrintInColorDelay(string obj, ConsoleColor color, int speed)
        {
            Console.ForegroundColor = color;
            foreach (char c in obj)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(speed);
            }
            Console.WriteLine();
            Console.ResetColor();
        }
        private void PrintInColor(string obj, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(obj);
            Console.ResetColor();
        }
        private void PrintInColorBuilder(StringBuilder obj, ConsoleColor color, ConsoleColor back)
        {
            Console.BackgroundColor = back;
            Console.ForegroundColor = color;
            Console.WriteLine(obj);
            Console.ResetColor();
        }
    }
}
