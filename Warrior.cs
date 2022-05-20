using System;
using System.Runtime.InteropServices;

namespace AntLife
{
    public class Warrior : Ant
    {
        private int dmg;
        public readonly int targets;

        public Warrior(Colony colony) : base(colony, AntType.Warrior)
        {
            int warrior_type = random.randint(2, 3);
            set_stats(warrior_type);
            dmg = Modifier.Stat(basic_modifier, "dmg");
            targets = Modifier.Stat(basic_modifier, "targets");
        }

        public override void info()
        {
            base.info();
            Console.WriteLine("--- Параметры: здоровье={0}, защита={1}, урон={2}", hp, def, dmg);
        }

        public void hit(Ant target, out bool target_alive)
        {
            target_alive = take_hit(dmg);
        }

        public bool take_hit(int taken_dmg)
        {
            int precision = random.randint(1, 10);
            if (precision >= def)
            {
                hp -= taken_dmg;
            }

            if (hp <= 0 || behavior_modifier == BehaviorModifier.Fragile)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}