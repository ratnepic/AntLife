using System;
using System.Collections.Generic;

namespace AntLife
{
    class Program
    {
        public static void Main(string[] args)
        {
            LifeCycle.init();
            Console.WriteLine("Эх, нелегка жизнь муравьев. Они стараются собрать как можно больше\n" +
                              "ресурсов до наступления засухи. После засухи выживет только колония\n" +
                              "с самым большим количеством ресурсов. Ваша задача - проследить за\n" +
                              "процессом жизни муравьёв и узнать победителя этой игры на выживание.");
            while (LifeCycle.InProcess)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Информация о текущем дне");
                Console.WriteLine("2. Информация о колонии");
                Console.WriteLine("3. Информация о муравье");
                Console.WriteLine("4. Начать поход за ресурасами");
                Console.Write("Введите номер варианта: ");
                string action = Console.ReadLine();
                Console.Clear();
                switch (action)
                {
                    case "1":
                        LifeCycle.day_info();
                        Console.ReadLine();
                        break;
                    case "2":
                        Console.Write("Введите номер колонии от 1 до {0}: ", LifeCycle.ColonyCount());
                        int col1 = 0;
                        if (int.TryParse(Console.ReadLine(), out col1) && col1 <= LifeCycle.ColonyCount())
                        {
                            LifeCycle.ColonyInfo(col1-1);
                        }
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.Write("Введите номер колонии от 1 до {0}: ", LifeCycle.ColonyCount());
                        int col2 = 0;
                        if (int.TryParse(Console.ReadLine(), out col2) && col2 <= LifeCycle.ColonyCount())
                        {
                            Console.Write("Введите номер муравья от 1 до {0}: ", LifeCycle.GetColony(col2-1).AntCount());
                            int antvar = 0;
                            if (int.TryParse(Console.ReadLine(), out antvar) &&
                                antvar <= LifeCycle.GetColony(col2 - 1).AntCount())
                            {
                                LifeCycle.GetColony(col2-1).GetAnt(antvar-1).info();
                                Console.WriteLine("Муравей хочет рассказать о своей королеве. Хотите послушать?\n" +
                                                  "(введите \"да\", чтобы послушать, или что-то другое, чтобы продолжить)");
                                if (Console.ReadLine() == "да")
                                {
                                    LifeCycle.GetColony(col2-1).GetAnt(antvar-1).queen_info();
                                }
                            }
                        }
                        Console.ReadLine();
                        break;
                    case "4":
                        break;
                    
                }
            }
        }
    }
}