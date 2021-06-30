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
                Console.WriteLine($"Here is the {selection} you need to guess:\n");
                StringBuilder displayToPlayer = DisplayWordLength(wordToGuessUppercase);
                Console.WriteLine("You have six (6) guesses to make, choose wisely");
                StartGame(wordToGuessUppercase, displayToPlayer, selection);

                Console.ReadKey();
            }
            else if (userinput == "2")
            {
                string selection = "Sports Team";
                string wordToGuessUppercase = PullFromSportsTeams();
                Console.Clear();
                Console.WriteLine($"Here is the {selection} you need to guess:\n");
                StringBuilder displayToPlayer = DisplayWordLength(wordToGuessUppercase);
                Console.WriteLine("You have six (6) guesses to make, choose wisely");
                StartGame(wordToGuessUppercase, displayToPlayer, selection);

                Console.ReadKey();
            }
            else if (userinput == "3")
            {
                string selection = "Food";
                string wordToGuessUppercase = PullFromFoods();
                Console.Clear();
                Console.WriteLine($"Here is the {selection} you need to guess:\n");
                StringBuilder displayToPlayer = DisplayWordLength(wordToGuessUppercase);
                Console.WriteLine("You have six (6) guesses to make, choose wisely");
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
                PrintInColor($"You have {lives} lives remaining!", ConsoleColor.DarkYellow);
                PrintInColor($"Guess a Letter or Number for the {category}: ", ConsoleColor.Blue);
                input = Console.ReadLine().ToUpper();
                guess = input[0];

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

                PrintInColor(displayToPlayer.ToString(), ConsoleColor.Green);
                
            }

            HangManModel(lives);
            if (won)
                PrintInColor($"You won! Good job on guess {wordToGuess}", ConsoleColor.Green );
            else
                PrintInColor($"You lost! It was  {wordToGuess}", ConsoleColor.Red);

            Console.Write("Press ENTER to exit....");
            Console.ReadLine();

        }
        private void HangManModel(int livesLeft)
        {
            switch (livesLeft)
            {
                case 6:
                    Console.WriteLine(" ___________.._______\n" +
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
                        ": :         | |       \n");
                    break;
                case 5:
                    Console.WriteLine(" ___________.._______\n" +
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
                        ": :         | |       \n");
                    break;
                case 4:
                    Console.WriteLine(" ___________.._______\n" +
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
                        ": :         | |\n");
                    break;
                case 3:
                    Console.WriteLine(" ___________.._______\n" +
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
                        ": :         | |\n");
                    break;
                case 2:
                    Console.WriteLine(" ___________.._______\n" +
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
                        ": :         | |\n");
                    break;
                case 1:
                    Console.WriteLine(" ___________.._______\n" +
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
                        ": :         | |\n");
                    break;
                default:
                    Console.WriteLine(" ___________.._______\n" +
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
                        ": :         | |\n");
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
            
            PrintInColorBuilder(displayToPlayer, ConsoleColor.Green, ConsoleColor.Black);
            return displayToPlayer;
        }
        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine($"-----------HangMan----------\n\n" +
                $"----------Main Menu---------\n" +
                $"**Choose A Category or Exit**\n" +
                $"1. Movies\n" +
                $"2. Sports\n" +
                $"3. Food\n" +
                $"4. Exit");
        }
        private void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
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
