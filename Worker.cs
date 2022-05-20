using System;
using System.Collections.Generic;

namespace AntLife
{
    public class Worker : Ant
    {
        private int resource_limit;
        private ResourceType[] collectable_resource;
        private bool strict_collection;
        private Dictionary<ResourceType, int> resources =
            new Dictionary<ResourceType, int>()
            {
                {ResourceType.Dew, 0},
                {ResourceType.Stick, 0},
                {ResourceType.Stone, 0},
                {ResourceType.Leaf, 0}
            };

        public Worker(Colony colony) : base(colony, AntType.Worker)
        {
            int worker_type = random.randint(0, 1);
            set_stats(worker_type);
            resource_limit = Modifier.Stat(basic_modifier, "resource_limit");
            collectable_resource = colony.AntInfo.WorkerResources[worker_type];
            strict_collection = Modifier.Stat(basic_modifier, "collection_type") == 0 ? false : true;
        }

        public override void info()
        {
            base.info();
            Console.WriteLine("--- Параметры: здоровье={0}, защита={1}", hp, def);
        }

        public bool take_hit()
        {
            if (behavior_modifier == BehaviorModifier.Chunky)
            {
                behavior_modifier = BehaviorModifier.ChunkyOff;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void harvest(Dictionary<ResourceType, int> heap)
        {
            if (strict_collection)
            {
                int res_at_a_time = resource_limit / collectable_resource.Length;
                foreach (ResourceType res in collectable_resource)
                {
                    if (heap[res] >= res_at_a_time)
                    {
                        heap[res] -= res_at_a_time;
                        resources[res] += res_at_a_time;
                    }
                }
            }
            else
            {
                List<ResourceType> temp_coll_res = new List<ResourceType>(collectable_resource);
                for (int attempt = 0; attempt < resource_limit;)
                {
                    ResourceType res = temp_coll_res[random.randint(0, temp_coll_res.Count - 1)];
                    if (heap[res] >= 0)
                    {
                        heap[res]--;
                        resources[res]++;
                        attempt++;
                    }
                    else
                    {
                        temp_coll_res.Remove(res);
                    }
                }
            }
        }

        public void return_resources()
        {
            colony.add_resources(resources);
            resources = new Dictionary<ResourceType, int>()
                {
                    {ResourceType.Dew, 0},
                    {ResourceType.Stick, 0},
                    {ResourceType.Stone, 0},
                    {ResourceType.Leaf, 0}
                };
        }
    }
}