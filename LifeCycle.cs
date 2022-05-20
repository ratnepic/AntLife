using System;
using System.Collections.Generic;

namespace AntLife
{
    public static class LifeCycle
    {
        private static readonly int days_to_end = 15;
        private static int day = 1;
        private static bool global_effect_begun = false;
        private static int global_effect_turns = 2;
        private static bool inProcess = true;
        public static bool InProcess => inProcess;
        private static readonly Dictionary<ColonyTeam, ColonyAntInfo> TeamAntInfo =
            new Dictionary<ColonyTeam, ColonyAntInfo>()
            {
                {
                    ColonyTeam.Ginger, new ColonyAntInfo(
                        new AntVariant[]
                        {
                            new AntVariant("рабочий", BasicModifier.Elite, BehaviorModifier.Default),
                            new AntVariant("рабочий", BasicModifier.Elite, BehaviorModifier.Chunky),
                            new AntVariant("воин", BasicModifier.Regular, BehaviorModifier.Default),
                            new AntVariant("воин", BasicModifier.Regular, BehaviorModifier.Sloppy),
                            new AntVariant("- Сверчок", BasicModifier.Special, BehaviorModifier.Villain)
                        },
                        new ResourceType[][]
                        {
                            new ResourceType[] {ResourceType.Dew, ResourceType.Stick},
                            new ResourceType[] {ResourceType.Leaf, ResourceType.Leaf}
                        },
                        new int[] {27, 9, 5},
                        3, 1, true)
                },
                {
                    ColonyTeam.Red, new ColonyAntInfo(
                        new AntVariant[]
                        {
                            new AntVariant("рабочий", BasicModifier.Elite, BehaviorModifier.Default),
                            new AntVariant("рабочий", BasicModifier.Regular, BehaviorModifier.Naughty),
                            new AntVariant("воин", BasicModifier.Elder, BehaviorModifier.Default),
                            new AntVariant("воин", BasicModifier.Legend, BehaviorModifier.Sergeant),
                            new AntVariant("- Термит", BasicModifier.Special, BehaviorModifier.Unlucky)
                        },
                        new ResourceType[][]
                        {
                            new ResourceType[] {ResourceType.Leaf, ResourceType.Leaf},
                            new ResourceType[] {ResourceType.Dew}
                        },
                        new int[] {18, 9, 7},
                        2, 2, true)
                },
                {
                    ColonyTeam.Green, new ColonyAntInfo(
                        new AntVariant[]
                        {
                            new AntVariant("рабочий", BasicModifier.Elder, BehaviorModifier.Default),
                            new AntVariant("рабочий", BasicModifier.Elite, BehaviorModifier.Runner),
                            new AntVariant("воин", BasicModifier.Legend, BehaviorModifier.Default),
                            new AntVariant("воин", BasicModifier.Advanced, BehaviorModifier.Vengeful),
                            new AntVariant("- Пчела", BasicModifier.Special, BehaviorModifier.Fragile)
                        },
                        new ResourceType[][]
                        {
                            new ResourceType[] {ResourceType.Dew, ResourceType.Dew},
                            new ResourceType[] {ResourceType.Stick, ResourceType.Stick}
                        },
                        new int[] {19, 7, 7},
                        2, 3, false)
                }
            };
        private static List<Colony> colonies =
            new List<Colony>()
            {
                new Colony(
                    new Queen(ColonyTeam.Ginger,"Клеопатра", 25, 5, 18, new int[]{3, 4}, new int[]{2, 4}),
                    TeamAntInfo[ColonyTeam.Ginger]),
                new Colony(
                    new Queen(ColonyTeam.Red, "Бланка", 28, 8, 17, new int[]{3, 3}, new int[]{3, 3}),
                    TeamAntInfo[ColonyTeam.Red]),
                new Colony(
                    new Queen(ColonyTeam.Green, "Бланка", 26, 8, 18, new int[]{1, 5}, new int[]{2, 4}),
                    TeamAntInfo[ColonyTeam.Green])
            };

        private static Dictionary<int, Dictionary<ResourceType, int>> resource_heaps =
            new Dictionary<int, Dictionary<ResourceType, int>>()
            {
                {
                    1, new Dictionary<ResourceType, int>()
                    {
                        {ResourceType.Stone, 0},
                        {ResourceType.Stick, 12},
                        {ResourceType.Leaf, 35},
                        {ResourceType.Dew, 0}
                    }
                },
                {
                    2, new Dictionary<ResourceType, int>()
                    {
                        {ResourceType.Stone, 0},
                        {ResourceType.Stick, 31},
                        {ResourceType.Dew, 12},
                        {ResourceType.Leaf, 0}
                    }
                },
                {
                    3, new Dictionary<ResourceType, int>()
                    {
                        {ResourceType.Stone, 0},
                        {ResourceType.Stick, 18},
                        {ResourceType.Dew, 22},
                        {ResourceType.Leaf, 0}
                    }
                },
                {
                    4, new Dictionary<ResourceType, int>()
                    {
                        {ResourceType.Stone, 0},
                        {ResourceType.Stick, 29},
                        {ResourceType.Leaf, 13},
                        {ResourceType.Dew, 0}
                    }
                },
                {
                    5, new Dictionary<ResourceType, int>()
                    {
                        {ResourceType.Stick, 48},
                        {ResourceType.Stone, 38},
                        {ResourceType.Leaf, 0},
                        {ResourceType.Dew, 0}
                    }
                }
            };

        public static void init()
        {
            colonies[0].init(19, 6, 1);
            colonies[1].init(17, 7, 1);
            colonies[2].init(13, 9, 1);
        }

        public static void create_colony(Queen qn)
        {
            colonies.Add(new Colony(qn, TeamAntInfo[qn.team]));
        }

        public static void ColonyInfo(int index)
        {
            colonies[index].info();
        }

        public static int ColonyCount()
        {
            return colonies.Count;
        }

        public static Colony GetColony(int index)
        {
            return colonies[index];
        }

        private static bool check_heaps()
        {
            int total_resources = 0;
            foreach (var resources in resource_heaps.Values)
            {
                foreach (var res in resources.Values)
                {
                    total_resources += res;
                }
            }

            return (total_resources > 0);
        }
        
        public static void day_info()
        {
            Console.WriteLine("День {0} (До засухи осталось {1} дней)", day, days_to_end-day);
            foreach (var col in colonies)
            {
                col.short_info();
                Console.WriteLine();
            }

            foreach (var resource_heap in resource_heaps)
            {
                Console.WriteLine("Куча {0}: к={1}, л={2}, в={3}, р={4}", resource_heap.Key,
                    resource_heap.Value[ResourceType.Stone], resource_heap.Value[ResourceType.Leaf],
                    resource_heap.Value[ResourceType.Stick], resource_heap.Value[ResourceType.Dew]);
            }

            Console.WriteLine();
            
            Console.WriteLine("Глобальный эффект: <Легендарный муравей воин> - селится в случайную колонию и " + "\n" +
                              "блокирует возможность других колоний ходить в походы на '2' дня ({0})",
                global_effect_begun ? (global_effect_turns <= 0 ? "завершено" : $"еще {global_effect_turns} дней") : "не начолось");
        }

        public static void day_cycle()
        {
            if (days_to_end - day <= 0 || !check_heaps())
            {
                // end the program
            }
            else
            {
                
            }
        }

        public static void crusade()
        {
            // crusade
        }
    }
}