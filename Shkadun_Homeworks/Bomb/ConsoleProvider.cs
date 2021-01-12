using System;

namespace Shkadun_Bomb.Bomb
{
    public static class ConsoleProvider
    {
        public const string InputAttemptCount = "Input count attempts";
        public const string InputTime = "Input time for code section(in seconds)";
        public const string WriteInputCode = "Input code";
        public const string IncorrectInput = "Incorrect input";
        public const string CorrectCode = "Corerct code. You Win!";
        public const string IncorrectCode = "Incorerct code.";
        public const string BombExploded = "Bomb exploded. You loss. ";
        public const string PositionMatches = "Number in their places ";
        public const string NumbersMatches = "Numbers matches ";
        public const string ChooseAction = "1. Start game\n2. Look records\n3. Exit";
        public const string PressEnter = "Press enter to continue";
        public const string OutputCode = "Code was ";
        public const string FileNotExists = "File not exists";
        public const string WinnersInfo = "winners:\ntime/attempts";
        public const string NoneWinners = "Not winners in list";


        public static void SendMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static int InputIntegerValue()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine(IncorrectInput);
                }
            }
        }

        public static string InputCode()
        {
            return Console.ReadLine();
        }
    }
}
