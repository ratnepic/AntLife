namespace AntLife
{
    public struct ColonyAntInfo
    {
        public readonly ResourceType[][] WorkerResources;
        public readonly AntVariant[] AntVariants; 
        public readonly int[] SpecialStats;
        public readonly int SpecialTargets;
        public readonly int SpecialBites;
        public readonly bool SpecialInvulnerable;

        public ColonyAntInfo(AntVariant[] antVariants, ResourceType[][] workerResources, int[] specialStats, int specialTargets, int specialBites, bool specialInvulnerable)
        {
            WorkerResources = workerResources;
            AntVariants = antVariants;
            SpecialStats = specialStats;
            SpecialTargets = specialTargets;
            SpecialBites = specialBites;
            SpecialInvulnerable = specialInvulnerable;
        }
    }
}