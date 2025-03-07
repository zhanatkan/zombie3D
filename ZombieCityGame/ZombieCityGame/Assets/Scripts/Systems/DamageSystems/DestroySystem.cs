using Leopotam.Ecs;
using UnityEngine;

public class DestroySystem : IEcsRunSystem
{
    private EcsFilter<DestroyEvent, Enemy> destroyFilter;
    private EnemyWaveSystem waveSystem;
    public void Run()
    {
        foreach (var i in destroyFilter)
        {
            ref var entity = ref destroyFilter.GetEntity(i);
            ref var enemy = ref destroyFilter.Get2(i);
            GameObject.Destroy(enemy.enemyObject);
            entity.Destroy(); 
            waveSystem.DecrementEnemyCount();
        }
    }
}
