
namespace Shkadun_Princess
{
    class Player
    {
        public int Hp { get; private set; }
        public int PlayerX { get; private set; }
        public int PlayerY { get; private set; }

        //Для проверки победы. Если hp > 10, то в program выводит победу.
        public void setHpWin()
        {
            Hp = 11;
        }

        public void StartPlayerPosition()
        {
            PlayerX = 0;
            PlayerY = 0;
            Hp = 10;
        }

        public void PlayerRunUp(Map map)
        {
            //Если юзер находится на верхней ячейке
            if (PlayerX == 0) { return; }

            PlayerX -= 1;
            int damage = map.CheckMine(this); //Проверяет, стал ли юзер на мину. Если нет
            Hp -= damage;                     //Снимает 0 HP
            map.DrowMap(this);
        }
        public void PlayerRunDown(Map map)
        {
            //Если юзер находится на нижней клетке
            if (PlayerX == 9) { return; }

            PlayerX += 1;
            int damage = map.CheckMine(this); //Проверяет, стал ли юзер на мину. Если нет
            Hp -= damage;                     //Снимает 0 HP
            map.DrowMap(this);
        }
        public void PlayerRunRight(Map map)
        {
            //Если юзер на правой ячейке
            if (PlayerY == 9) { return; }

            PlayerY += 1;
            int damage = map.CheckMine(this); //Проверяет, стал ли юзер на мину. Если нет
            Hp -= damage;                     //Снимает 0 HP
            map.DrowMap(this);
        }
        public void PlayerRunLeft(Map map)
        {
            //Если юзер на левой ячейке
            if (PlayerY == 0) { return; }

            PlayerY -= 1;
            int damage = map.CheckMine(this); //Проверяет, стал ли юзер на мину. Если нет
            Hp -= damage;                     //Снимает 0 HP
            map.DrowMap(this);
        }

        public Player()
        {
            Hp = 10;
            PlayerX = 0;
            PlayerY = 0;
        }
    }
}
