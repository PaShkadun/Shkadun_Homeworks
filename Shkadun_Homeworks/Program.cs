namespace Shkadun_Bank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Нужен для того, чтобы в раз в n-ный промежуток начислялись кредиты(при наличии)
            // Чтобы не было нужды делать это вручную
            Bank.CreateTimer();
            Bank.Start();
        }
    }
}