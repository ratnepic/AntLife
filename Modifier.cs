using System.Collections.Generic;

namespace AntLife
{
    public class Modifier
    {
        private static readonly Dictionary<BasicModifier, Dictionary<string, int>> Stats =
            new Dictionary<BasicModifier, Dictionary<string, int>>()
            {
                {
                    BasicModifier.Elite, new Dictionary<string, int>()
                    {
                        {"hp", 8},
                        {"def", 4},
                        {"resource_limit", 2},
                        {"collection_type", 1}
                    }
                },
                {
                    BasicModifier.Regular, new Dictionary<string, int>()
                    {
                        {"hp", 1},
                        {"def", 0},
                        {"dmg", 1},
                        {"targets", 1},
                        {"resource_limit", 1},
                        {"collection_type", 1}
                    }
                },
                {
                    BasicModifier.Elder, new Dictionary<string, int>()
                    {
                        {"hp", 2},
                        {"def", 1},
                        {"dmg", 2},
                        {"targets", 1},
                        {"resource_limit", 1},
                        {"collection_type", 0}
                    }
                },
                {
                    BasicModifier.Advanced, new Dictionary<string, int>()
                    {
                        {"hp", 6},
                        {"def", 2},
                        {"dmg", 4},
                        {"targets", 2}
                    }
                },
                {
                    BasicModifier.Legend, new Dictionary<string, int>()
                    {
                        {"hp", 10},
                        {"def", 6},
                        {"dmg", 4},
                        {"targets", 3}
                    }
                }
            };

        private static readonly Dictionary<BehaviorModifier, string> mod_desc =
            new Dictionary<BehaviorModifier, string>()
            {
                {BehaviorModifier.Chunky, "может пережить 1 любой укус"},
                {BehaviorModifier.ChunkyOff, "может пережить 1 любой укус (сработало)"},
                {BehaviorModifier.Fragile, "умирает с одного укуса"},
                {BehaviorModifier.Naughty, "игнорирует каждый 2й поход"},
                {BehaviorModifier.Runner, "после смерти все равно доставляет ресурс в колонию"},
                {BehaviorModifier.Sergeant, "если атакует первый в походе, то убивает с одного укуса любое насекомое даже неуязвимое"},
                {BehaviorModifier.Sloppy, "защита уменьшена в двое"},
                {BehaviorModifier.Unlucky, "урон вражеского укуса увеличен в двое"},
                {BehaviorModifier.Vengeful, "убивает своего убийцу, даже если он неуязвим"},
                {BehaviorModifier.Villain, "всегда атакует последним и убивает с одного укуса любое насекомое даже неуязвимое"}
            };

        private static readonly Dictionary<BasicModifier, string> BasicName =
            new Dictionary<BasicModifier, string>()
            {
                {BasicModifier.Advanced, "продвинутый"},
                {BasicModifier.Elder, "старший"},
                {BasicModifier.Elite, "элитный"},
                {BasicModifier.Legend, "легендарный"},
                {BasicModifier.Regular, "обычный"},
                {BasicModifier.Special, ""}
            };

        private static readonly Dictionary<BehaviorModifier, string> BehaviorName =
            new Dictionary<BehaviorModifier, string>()
            {
                {BehaviorModifier.Chunky, "коренастый"},
                {BehaviorModifier.Fragile, "хрупкий"},
                {BehaviorModifier.Naughty, "капризный"},
                {BehaviorModifier.Runner, "марафонец"},
                {BehaviorModifier.Sergeant, "сержант"},
                {BehaviorModifier.Sloppy, "неряшливый"},
                {BehaviorModifier.Unlucky, "невезучий"},
                {BehaviorModifier.Vengeful, "мстительный"},
                {BehaviorModifier.Villain, "злодей"},
                {BehaviorModifier.Default, ""}
            };

        public static readonly Dictionary<ColonyTeam, string> TeamName =
            new Dictionary<ColonyTeam, string>()
            {
                {ColonyTeam.Ginger, "рыжие"},
                {ColonyTeam.Red, "красные"},
                {ColonyTeam.Green, "зеленые"}
            };

        public static int Stat(BasicModifier mod, string st)
        {
            return Stats[mod][st];
        }

        public static string Name(BasicModifier mod)
        {
            return BasicName[mod];
        }

        public static string Name(BehaviorModifier mod)
        {
            return BehaviorName[mod];
        }

        public static string Name(ColonyTeam tm)
        {
            return TeamName[tm];
        }

        public static string Desc(BehaviorModifier mod)
        {
            return mod_desc[mod];
        } 
    }
}