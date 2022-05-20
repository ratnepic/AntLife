using System;

namespace AntLife
{
    public class random
    {
        private static Random rand = new Random();

        public static int randint(int a, int b)
        {
            return rand.Next(a, b+1);
        }
    }
}