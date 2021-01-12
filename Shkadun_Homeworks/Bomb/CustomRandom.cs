using System;

namespace Shkadun_Bomb.Bomb
{
    public static class CustomRandom
    {
        private static Random random;

        static CustomRandom()
        {
            random = new Random();
        }

        public static string GenerationCode(int codeLength)
        {
            string code = random.Next(0, 9).ToString();

            for (int i = 1; i < codeLength; i++)
            {
                code += random.Next(0, 9).ToString();
            }

            return code;
        }
    }
}
