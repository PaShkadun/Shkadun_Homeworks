using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Shkadun_Bomb.Bomb
{
    public static class Records
    {
        public async static void InputWinner(Bomb bomb)
        {
            List<Bomb> winners = new List<Bomb>();

            using (FileStream file = new FileStream("../../../Bomb/winners.json", FileMode.OpenOrCreate))
            {
                    winners = await JsonSerializer.DeserializeAsync<List<Bomb>>(file);
                    winners.Add(bomb);
            }

            using (FileStream file = new FileStream("../../../Bomb/winners.json", FileMode.Create))
            {
                await JsonSerializer.SerializeAsync<List<Bomb>>(file, winners);
            }
        }

        public async static void OutputWinners()
        {
            using (FileStream file = new FileStream("../../../Bomb/winners.json", FileMode.OpenOrCreate))
            {
                List<Bomb> winners = new List<Bomb>();

                try
                {
                    winners = await JsonSerializer.DeserializeAsync<List<Bomb>>(file);

                    var sortWinner = from win in winners
                                     orderby win.Timer ascending
                                     select win;

                    ConsoleProvider.SendMessage(ConsoleProvider.WinnersInfo);

                    foreach (Bomb winner in sortWinner)
                    {
                        ConsoleProvider.SendMessage($"{winner.Timer} {winner.CountAttempts}");
                    }
                }
                catch
                {
                    ConsoleProvider.SendMessage(ConsoleProvider.NoneWinners);
                }
            }
        }
    }
}
