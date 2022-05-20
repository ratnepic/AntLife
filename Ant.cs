using System;

namespace AntLife
{
    public abstract class Ant
    {
        public readonly Colony colony;
        public readonly AntType type;
        protected int variant;
        public int Variant => variant;
        protected int hp;
        protected int def;
        protected BasicModifier basic_modifier;
        protected BehaviorModifier behavior_modifier;

        protected Ant(Colony colony, AntType type)
        {
            this.colony = colony;
            this.type = type;
        }

        protected void set_stats(int ant_variant)
        {
            variant = ant_variant;
            basic_modifier = colony.AntInfo.AntVariants[ant_variant].basic_modifier;
            behavior_modifier = colony.AntInfo.AntVariants[ant_variant].behavior_modifier;
            hp = Modifier.Stat(basic_modifier, "hp");
            def = Modifier.Stat(basic_modifier, "def");
        }

        public virtual void info()
        {
            Console.WriteLine("Тип: {0}", colony.get_variant_string(variant));
            Console.WriteLine("--- Королева \"{0}\"", colony.queen.name);
        }

        public void queen_info()
        {
            Console.WriteLine("Наша королева {0} самая лучшая!", colony.queen.name);
            Console.WriteLine("Ее характеристики: здоровье={0}, защита={1}, урон={2}", colony.queen.hp, colony.queen.def, colony.queen.dmg);
            Console.WriteLine("Вся колония ее уважает!");
        }
    }
}