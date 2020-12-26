using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shkadun_Bomb.Bomb
{
    public class Bomb
    {
        public const int CodeLength = 4;
        private const char MatchesNumber = 'X';

        public readonly int Attempts;
        public readonly int Time;

        public int Timer { get; set; }

        public int CountAttempts { get; set; }

        public string Code { get; }
        
        public Bomb(int attempts, int time)
        {
            Timer = 0;
            Time = time;
            Attempts = attempts;
            CountAttempts = 0;
            Code = CustomRandom.GenerationCode(CodeLength);

            Console.WriteLine(Code);
            CreateTimer();
        }

        public Bomb()
        {

        }

        public void CheckCode(string inputCode)
        {
            if (Game.Status != GameStatus.InGame)
            {
                return;
            }
            else
            {
                if (inputCode.Length != Code.Length)
                {
                    Game.SendMessage(ConsoleProvider.IncorrectInput);
                }
                else if (inputCode.Equals(Code))
                {
                    Game.Status = GameStatus.Win;
                }
                else
                {
                    Game.SendMessage(ConsoleProvider.IncorrectCode);
                }
            }
        }

        public void CheckCodePosition(string inputCode)
        {
            if (inputCode.Length != CodeLength || Game.Status != GameStatus.InGame)
            {
                return;
            }
            else 
            {
                CountAttempts++;
            }

            int countPositionMatches = 0;
            char[] charsCodeNumber = Code.ToCharArray();
            char[] charsInputCode = inputCode.ToCharArray();

            for (int i = 0; i < CodeLength; i++)
            {
                if (charsCodeNumber[i] == charsInputCode[i])
                {
                    countPositionMatches++;
                }
            }

            Game.SendMessage(ConsoleProvider.PositionMatches + countPositionMatches);

            int countNumberMatches = 0;

            for (int i = 0; i < CodeLength; i++)
            {
                for (int j = 0; j < CodeLength; j++)
                {
                    if (charsCodeNumber[i] == charsInputCode[j])
                    {
                        charsInputCode[j] = MatchesNumber;
                        countNumberMatches++;
                        break;
                    }
                }
            }

            Game.SendMessage(ConsoleProvider.NumbersMatches + countNumberMatches);
        }

        private void CreateTimer()
        {
            Task task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    Timer++;

                    if (Timer == Time && Game.Status == GameStatus.InGame)
                    {
                        Game.Status = GameStatus.Los;

                        return;
                    }
                }
            });
        }
    }
}
