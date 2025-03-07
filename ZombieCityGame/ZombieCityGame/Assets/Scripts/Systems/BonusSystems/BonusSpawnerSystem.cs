using Leopotam.Ecs;
using UnityEngine;

public class BonusSpawnerSystem : IEcsRunSystem
{
    private EcsFilter<Enemy, EnemyDeathEvent> deadEnemies;
    private BonusStaticData staticData;
    private EcsWorld world;

    private const float dropChance = 0.6f; 

    public void Run()
    {
        if (staticData == null) return;
        foreach (var i in deadEnemies)
        {
            ref var enemyTransform = ref deadEnemies.Get1(i);

            if (Random.value > dropChance)
            {
                continue;
            }
            var bonusData = staticData.bonuses[Random.Range(0, staticData.bonuses.Length)];

            var bonusGO = Object.Instantiate(bonusData.bonusPrefab, enemyTransform.enemyObject.transform.position, Quaternion.identity);

            var bonusEntity = world.NewEntity();

            ref var trigger = ref bonusEntity.Get<BonusTrigger>();
            trigger.collider = bonusGO.GetComponent<Collider>();

            ref var bonus = ref bonusEntity.Get<Bonus>();

            bonus.bonusType = bonusData.bonusType;
            bonus.bonusObject = bonusGO; 
            bonus.effectValue = bonusData.effectValue;

            bonusGO.GetComponent<BonusView>().entity = bonusEntity;
        }
    }
}
