using System;

namespace AntLife
{
    public class AntVariant
    {
        public readonly string name;
        public readonly BasicModifier basic_modifier;
        public readonly BehaviorModifier behavior_modifier;

        public AntVariant(string name, BasicModifier basic_modifier, BehaviorModifier behavior_modifier)
        {
            this.name = name;
            this.basic_modifier = basic_modifier;
            this.behavior_modifier = behavior_modifier;
        }
        
        public string variant_string()
        {
            return (Modifier.Name(this.basic_modifier) + " " + Modifier.Name(this.behavior_modifier) + " " + name);
        }
    }
}