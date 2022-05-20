namespace AntLife
{
    public class Queen
    {
        public readonly ColonyTeam team;
        public readonly string name;
        public readonly AntType type;
        public readonly int hp;
        public readonly int def;
        public readonly int dmg;
        private int[] maturity_cycle;
        private int[] daughter_limit;
        private int daughters_left;
        private int larva;
        private int days_to_mature;
        public int Larva => larva;

        public Queen(ColonyTeam team, string name, int hp, int def, int dmg, int[] maturityCycle, int[] daughter_limit)
        {
            this.team = team;
            this.name = name;
            this.type = AntType.Queen;
            this.hp = hp;
            this.def = def;
            this.dmg = dmg;
            maturity_cycle = maturityCycle;
            this.daughter_limit = daughter_limit;
            daughters_left = random.randint(daughter_limit[0], daughter_limit[1]);
            larva = random.randint(3, 5);
            days_to_mature = random.randint(maturityCycle[0], maturityCycle[1]);
        }

        public void give_birth(Colony col)
        {
            int lower_limit = (daughters_left >= 0) ? 1 : 3;
            int ant_type = random.randint(lower_limit, 20);
            switch (ant_type)
            {
                case <= 2:
                    daughters_left--;
                    if (random.randint(0, 1) == 1)
                    {
                        LifeCycle.create_colony(new Queen(this.team, this.name+"-дочь", this.hp, this.def, this.dmg,
                            this.maturity_cycle, this.daughter_limit));
                    }
                    break;
                case <= 6:
                    col.add_ant(new Worker(col));
                    break;
                case <= 13:
                    col.add_ant(new Warrior(col));
                    break;
                case <= 20:
                    col.add_ant(new Special(col));
                    break;
            }
        }

        public void mature(Colony col)
        {
            days_to_mature--;
            if (days_to_mature <= 0)
            {
                col.create_ants(larva);
                larva = random.randint(3, 5);
                days_to_mature = random.randint(maturity_cycle[0], maturity_cycle[1]);
            }
        }
    }
}