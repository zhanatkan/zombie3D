using Leopotam.Ecs;
using UnityEngine;

public class EnemyDeathSystem : IEcsRunSystem
{
    private EcsFilter<Enemy, Health> enemyFilter;
    private EcsWorld ecsWorld;

    public void Run()
    {
        foreach (var i in enemyFilter)
        {
            ref var enemy = ref enemyFilter.Get1(i);
            ref var health = ref enemyFilter.Get2(i);
            if (health.health > 0) continue; 

            ref var entity = ref enemyFilter.GetEntity(i);

            entity.Get<EnemyDeathEvent>();

            entity.Get<DestroyEvent>();
        }
    }
}

public struct EnemyDeathEvent { }
public struct DestroyEvent { }
