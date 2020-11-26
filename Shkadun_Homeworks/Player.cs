
namespace Shkadun_Princess
{
    class Player
    {
        public int HP { get; private set; }
        public int PlayerPositionHorizontal { get; private set; }
        public int PlayerPositionVertical { get; private set; }

        //Для проверки победы. Если hp > 10, то в program выводит победу.
        public void SetHpWin()
        {
            HP = 11;
        }

        public void StartPlayerPosition()
        {
            PlayerPositionHorizontal = 0;
            PlayerPositionVertical = 0;
            HP = 10;
        }

        public void PlayerRunUp(Map map)
        {
            //Если юзер находится на верхней ячейке
            if (PlayerPositionHorizontal == 0) { return; }

            PlayerPositionHorizontal -= 1;
            int damage = map.CheckMine(this); //Проверяет, стал ли юзер на мину. Если нет
            HP -= damage;                     //Снимает 0 HP
            map.DrowMap(this);
        }
        public void PlayerRunDown(Map map)
        {
            //Если юзер находится на нижней клетке
            if (PlayerPositionHorizontal == 9) { return; }

            PlayerPositionHorizontal += 1;
            int damage = map.CheckMine(this); //Проверяет, стал ли юзер на мину. Если нет
            HP -= damage;                     //Снимает 0 HP
            map.DrowMap(this);
        }
        public void PlayerRunRight(Map map)
        {
            //Если юзер на правой ячейке
            if (PlayerPositionVertical == 9) { return; }

            PlayerPositionVertical += 1;
            int damage = map.CheckMine(this); //Проверяет, стал ли юзер на мину. Если нет
            HP -= damage;                     //Снимает 0 HP
            map.DrowMap(this);
        }
        public void PlayerRunLeft(Map map)
        {
            //Если юзер на левой ячейке
            if (PlayerPositionVertical == 0) { return; }

            PlayerPositionVertical -= 1;
            int damage = map.CheckMine(this); //Проверяет, стал ли юзер на мину. Если нет
            HP -= damage;                     //Снимает 0 HP
            map.DrowMap(this);
        }

        public Player()
        {
            HP = 10;
            PlayerPositionHorizontal = 0;
            PlayerPositionVertical = 0;
        }
    }
}
