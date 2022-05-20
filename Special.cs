using System;

namespace AntLife
{
    public class Special : Ant
    {
        private int dmg;
        private int targets;
        private int bites;
        private bool invulnerable;

        public Special(Colony colony) : base(colony, AntType.Special)
        {
            int special_type = 4;
            variant = 4;
            basic_modifier = colony.AntInfo.AntVariants[special_type].basic_modifier;
            behavior_modifier = colony.AntInfo.AntVariants[special_type].behavior_modifier;
            hp = colony.AntInfo.SpecialStats[0];
            def = colony.AntInfo.SpecialStats[1];
            dmg = colony.AntInfo.SpecialStats[2];
            targets = colony.AntInfo.SpecialTargets;
            bites = colony.AntInfo.SpecialBites;
            invulnerable = colony.AntInfo.SpecialInvulnerable;
        }

        public override void info()
        {
            base.info();
            Console.WriteLine("--- Параметры: здоровье={0}, защита={1}, урон={2}", hp, def, dmg);
        }
        
        public void hit(Ant target, out bool target_alive)
        {
            target_alive = take_hit(dmg * bites);
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