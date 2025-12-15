using Leopotam.Ecs;
using UnityEngine;

public class BonusPickupSystem : IEcsRunSystem
{
    private EcsFilter<Player> _playerFilter;
    private EcsFilter<Bonus, BonusTrigger> _bonusFilter;

    public void Run()
    {
        foreach (var playerFilter in _playerFilter)
        {
            ref var player = ref _playerFilter.Get1(playerFilter);
            var playerCollider = player.playerTransform.GetComponent<Collider>();

            foreach (var bonusFilter in _bonusFilter)
            {
                ref var bonus = ref _bonusFilter.Get1(bonusFilter);
                ref var trigger = ref _bonusFilter.Get2(bonusFilter);

                if (trigger.collider.bounds.Intersects(playerCollider.bounds))
                {
                    if (bonus.bonusObject.TryGetComponent<BonusView>(out var bonusView))
                    {
                        var bonusData = bonusView.bonusData; 
                        if (bonusData != null && bonusData.pickupParticles != null)
                        {
                            var particles = Object.Instantiate(bonusData.pickupParticles, bonus.bonusObject.transform.position, Quaternion.identity);
                            particles.GetComponent<ParticleSystem>().Play(); 
                        }
                    }

                    ref var playerEntity = ref _playerFilter.GetEntity(playerFilter);
                    ref var effect = ref playerEntity.Get<BonusEffect>();

                    effect.bonusType = bonus.bonusType;
                    effect.effectValue = bonus.effectValue;
                    effect.timer = 5f;
                    GameObject.Destroy(bonus.bonusObject.gameObject);
                    _bonusFilter.GetEntity(bonusFilter).Destroy();
                }
            }
        }
    }
}
