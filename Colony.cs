using System;
using System.Collections.Generic;
using System.Threading.Channels;

namespace AntLife
{
    public class Colony
    {
        private static int colony_count = 0;
        public readonly ColonyAntInfo AntInfo;
        public readonly ColonyTeam team;
        public readonly int colony_number;
        public readonly Queen queen;
        private List<Ant> ants = new List<Ant>();
        private Dictionary<ResourceType, int> resources = 
            new Dictionary<ResourceType, int>() 
            {
                {ResourceType.Dew, 0},
                {ResourceType.Stick, 0},
                {ResourceType.Stone, 0},
                {ResourceType.Leaf, 0}
            };

        public Colony(Queen queen, ColonyAntInfo antInfo)
        {
            colony_count++;
            colony_number = colony_count;
            this.team = queen.team;
            this.queen = queen;
            this.AntInfo = antInfo;

        }

        public void init(int workers = 0, int warriors = 0, int specials = 0)
        {
            for (int i = 0; i < workers; i++)
            {
                add_ant(new Worker(this));
            }

            for (int i = 0; i < warriors; i++)
            {
                add_ant((new Warrior(this)));
            }

            for (int i = 0; i < specials; i++)
            {
                add_ant(new Special(this));
            }
        }

        public void create_ants(int ants)
        {
            for (int i = 0; i < ants; i++)
            {
                queen.give_birth(this);
            }
        }

        public void add_ant(Ant ant)
        {
            ants.Add(ant);
        }

        public int count_variants(int variant)
        {
            int count = 0;
            foreach (var ant in ants)
            {
                if (ant.Variant == variant) count++;
            }

            return count;
        }

        public int AntCount()
        {
            return ants.Count;
        }

        public Ant GetAnt(int index)
        {
            return ants[index];
        }

        public void info()
        {
            Console.WriteLine("Колония \"{0}-{1}\"", Modifier.Name(team), colony_number);
            Console.WriteLine("--- Королева <{0}>: здоровье={1}, защита={2}, урон={3}", queen.name, queen.hp, queen.def, queen.dmg);
            Console.WriteLine("--- Ресурсы: к={0}, л={1}, в={2}, р={3}", resources[ResourceType.Stone],
                resources[ResourceType.Leaf], resources[ResourceType.Stick], resources[ResourceType.Dew]);
            Console.WriteLine("\n<<<<<<<<<< Рабочие >>>>>>>>>>");
            for (int variant = 0; variant < 2; variant++)
            {
                Console.WriteLine("Тип: \"{0}\"", get_variant_string(variant));
                Console.WriteLine("--- Параметры: здоровье={0}, защита={1}",
                    Modifier.Stat(AntInfo.AntVariants[variant].basic_modifier, "hp"),
                    Modifier.Stat(AntInfo.AntVariants[variant].basic_modifier, "def"));
                if (AntInfo.AntVariants[variant].behavior_modifier != BehaviorModifier.Default)
                {
                    Console.WriteLine("--- Модификатор: \"{0}\"", Modifier.Desc(AntInfo.AntVariants[variant].behavior_modifier));
                }
                Console.WriteLine("--- Количество: {0}", count_variants(variant));
                Console.WriteLine();
            }

            Console.WriteLine("<<<<<<<<<< Воины >>>>>>>>>>");
            for (int variant = 2; variant < 4; variant++)
            {
                Console.WriteLine("Тип: \"{0}\"", get_variant_string(variant));
                Console.WriteLine("--- Параметры: здоровье={0}, защита={1}, атака={2}",
                    Modifier.Stat(AntInfo.AntVariants[variant].basic_modifier, "hp"),
                    Modifier.Stat(AntInfo.AntVariants[variant].basic_modifier, "def"),
                    Modifier.Stat(AntInfo.AntVariants[variant].basic_modifier, "dmg"));
                if (AntInfo.AntVariants[variant].behavior_modifier != BehaviorModifier.Default)
                {
                    Console.WriteLine("--- Модификатор: \"{0}\"", Modifier.Desc(AntInfo.AntVariants[variant].behavior_modifier));
                }
                Console.WriteLine("--- Количество: {0}", count_variants(variant));
                Console.WriteLine();
            }

            Console.WriteLine("<<<<<<<<<< Особое >>>>>>>>>>");
            Console.WriteLine("Тип: \"ленивый {0}уязвимый агрессивный{1}\"", AntInfo.SpecialInvulnerable ? "не":"", get_variant_string(4));
            Console.WriteLine("Параметры: здоровье={0}, защита={1}, атака={2}",
                AntInfo.SpecialStats[0],
                AntInfo.SpecialStats[1],
                AntInfo.SpecialStats[2]);
            Console.WriteLine("Модификаторы:");
            Console.WriteLine("--- не может брать ресурсы;");
            Console.WriteLine("--- " + (AntInfo.SpecialInvulnerable ? "не" : "") + " может быть атакован воинами;");
            Console.WriteLine("--- атакует врагов ({0} целей за раз и наносит {1} укуса);", AntInfo.SpecialTargets, AntInfo.SpecialBites);
            Console.WriteLine("--- {0}", Modifier.Desc(AntInfo.AntVariants[4].behavior_modifier));
            Console.WriteLine("Количество: {0}", count_variants(4));
        }

        public void short_info()
        {
            Console.WriteLine("Колония \"{0}-{1}\":", Modifier.Name(team), colony_number);
            Console.WriteLine("--- Королева \"{0}\", личинок:{1}", queen.name, queen.Larva);
            Console.WriteLine("--- Ресурсы: к={0}, л={1}, в={2}, р={3}", resources[ResourceType.Stone], resources[ResourceType.Leaf],
                resources[ResourceType.Stick], resources[ResourceType.Dew]);
            Console.WriteLine("--- Популяция: р={0} в={1} о={2}",
                count_variants(0)+count_variants(1),
                count_variants(2)+count_variants(3),
                count_variants(4));
        }
        public void add_resources(Dictionary<ResourceType, int> reses)
        {
            foreach (var keyvalue in reses)
            {
                resources[keyvalue.Key] += keyvalue.Value;
            }
        }

        public void remove_ant(Ant ant)
        {
            ants.Remove(ant);
        }
        public string get_variant_string(int variant)
        {
            return AntInfo.AntVariants[variant].variant_string();
        }

        public Dictionary<int, List<Ant>> SendAnts()
        {
            var sortedAnts = new Dictionary<int, List<Ant>>()
            {
                {1, new List<Ant>()},
                {2, new List<Ant>()},
                {3, new List<Ant>()},
                {4, new List<Ant>()},
                {5, new List<Ant>()}
            };

            return sortedAnts;
        }
    }
}