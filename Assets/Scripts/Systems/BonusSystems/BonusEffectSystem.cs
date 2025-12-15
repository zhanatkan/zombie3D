using Leopotam.Ecs;
using System.Collections.Generic;
public class BonusEffectSystem : IEcsRunSystem
{
    private EcsFilter<Player, BonusEffect, Health> playerFilter;
    private EcsFilter<Player, BonusEffect> weaponFilter;
    private Dictionary<BonusType, IBonusHandler> bonusHandlers;

    public BonusEffectSystem(EcsWorld ecsWorld, UI ui)
    {
        bonusHandlers = new Dictionary<BonusType, IBonusHandler>
        {
            { BonusType.AmmoCount, new AmmoCountBonusHandler(ecsWorld, ui) },
            { BonusType.Power, new PowerBonusHandler(ecsWorld, ui) },
            { BonusType.Speed, new SpeedBonusHandler(ecsWorld, ui) },
            { BonusType.Health, new HealthBonusHandler(ecsWorld, ui) },
            { BonusType.Shield, new ShieldBonusHandler(ecsWorld, ui) }
        };
    }
    public void Run()
    {
        foreach (var j in weaponFilter)
        {
            var playerEntity = weaponFilter.GetEntity(j);
            ref var effect = ref weaponFilter.Get2(j);

            if (!effect.applied && bonusHandlers.TryGetValue(effect.bonusType, out var handler))
            {
                handler.ApplyBonus(playerEntity, ref effect);
            }
        }
        
        foreach (var i in playerFilter)
        {
            var playerEntity = playerFilter.GetEntity(i);
            ref var effect = ref playerFilter.Get2(i);

            if (!effect.applied && bonusHandlers.TryGetValue(effect.bonusType, out var handler))
            {
                handler.ApplyBonus(playerEntity, ref effect);
            }
        }
    }
}