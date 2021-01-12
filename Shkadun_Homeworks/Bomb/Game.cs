using System;

namespace Shkadun_Bomb.Bomb
{
    public static class Game
    {
        private delegate void DeactivateBomb(string code);
        public delegate void SendMessages(string message);
        private static event DeactivateBomb deactivateBomb;
        public static SendMessages SendMessage;

        private static Bomb bomb;

        public static GameStatus Status { get; set; }

        static Game()
        {
            SendMessage = ConsoleProvider.SendMessage;
        }

        public static void StartGame()
        {
            Status = GameStatus.InGame;

            while (true)
            {
                if (Status == GameStatus.InGame && bomb.CountAttempts < bomb.Attempts)
                {
                    SendMessage(ConsoleProvider.WriteInputCode);
                    deactivateBomb(ConsoleProvider.InputCode());
                    SendMessage(ConsoleProvider.PressEnter);

                    Console.ReadKey(true);
                    Console.Clear();
                }
                else
                {
                    break;
                }
            }

            if (Status == GameStatus.Win)
            {
                SendMessage(ConsoleProvider.CorrectCode);
                Records.InputWinner(bomb);
            }
            else
            {
                SendMessage(ConsoleProvider.BombExploded + ConsoleProvider.OutputCode + bomb.Code);
            }
        }

        public static void CreateBomb()
        {
            SendMessage(ConsoleProvider.InputTime);

            int time = ConsoleProvider.InputIntegerValue();

            SendMessage(ConsoleProvider.InputAttemptCount);

            int attempts = ConsoleProvider.InputIntegerValue();
            
            bomb = new Bomb(attempts, time);
            deactivateBomb = bomb.CheckCodePosition;
            deactivateBomb += bomb.CheckCode;
        }
    }
}
