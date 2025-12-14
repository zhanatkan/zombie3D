using Leopotam.Ecs;
using UnityEngine;

public class BonusPickupSystem : IEcsRunSystem
{
    private EcsFilter<Player> playerFilter;
    private EcsFilter<Bonus, BonusTrigger> bonusFilter;

    public void Run()
    {
        foreach (var p in playerFilter)
        {
            ref var player = ref playerFilter.Get1(p);
            var playerCollider = player.playerTransform.GetComponent<Collider>();

            foreach (var b in bonusFilter)
            {
                ref var bonus = ref bonusFilter.Get1(b);
                ref var trigger = ref bonusFilter.Get2(b);

                if (trigger.collider.bounds.Intersects(playerCollider.bounds))
                {
                    if (bonus.bonusObject.TryGetComponent<BonusView>(out var bonusView))
                    {
                        var bonusData = bonusView.bonusData; // Получаем данные бонуса
                        if (bonusData != null && bonusData.pickupParticles != null)
                        {
                            // Создаем частицы на позиции бонуса
                            var particles = Object.Instantiate(bonusData.pickupParticles, bonus.bonusObject.transform.position, Quaternion.identity);
                            particles.GetComponent<ParticleSystem>().Play(); // Воспроизводим частицы
                        }
                    }

                    ref var playerEntity = ref playerFilter.GetEntity(p);
                    ref var effect = ref playerEntity.Get<BonusEffect>();

                    effect.bonusType = bonus.bonusType;
                    effect.effectValue = bonus.effectValue;
                    effect.timer = 5f;
                    GameObject.Destroy(bonus.bonusObject.gameObject);
                    bonusFilter.GetEntity(b).Destroy();
                }
            }
        }
    }
}
