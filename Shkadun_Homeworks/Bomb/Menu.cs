using System;
using System.Collections.Generic;
using System.Text;

namespace Shkadun_Bomb.Bomb
{
    public class Menu
    {
        private delegate void PlayGame();

        public static void CreateMenu()
        {
            while (true)
            {
                PlayGame playGame = Game.CreateBomb;
                playGame += Game.StartGame;

                ConsoleProvider.SendMessage(ConsoleProvider.ChooseAction);

                switch (ConsoleProvider.InputIntegerValue())
                {
                    case (int)GameActions.StartGame:
                        playGame();
                        break;

                    case (int)GameActions.LookRecords:
                        Records.OutputWinners();
                        break;

                    case (int)GameActions.Exit:
                        return;

                    default:
                        ConsoleProvider.SendMessage(ConsoleProvider.IncorrectInput);
                        break;
                }

                ConsoleProvider.SendMessage(ConsoleProvider.PressEnter);
                Console.ReadKey(true);
                Console.Clear();
            }
        }
    }
}
