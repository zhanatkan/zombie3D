using Leopotam.Ecs;
using UnityEngine;
public class ActiveBonusSystem : IEcsRunSystem
{
    private EcsFilter<ActiveBonus> activeBonuses;

    public void Run()
    {
        foreach (var i in activeBonuses)
        {
            ref var activeBonus = ref activeBonuses.Get1(i);
            activeBonus.timer -= Time.deltaTime;

            if (activeBonus.timer <= 0)
            {
                var playerEntity = activeBonuses.GetEntity(i);

                switch (activeBonus.bonusType)
                {
                    case BonusType.Speed:
                        if (playerEntity.Has<Player>())
                        {
                            ref var player = ref playerEntity.Get<Player>();
                            player.playerSpeed /= 1.5f;
                        }
                        break;

                    case BonusType.Power:
                        if (playerEntity.Has<HasWeapon>())
                        {
                            ref var hasWeapon = ref playerEntity.Get<HasWeapon>();
                            ref var weapon = ref hasWeapon.weapon.Get<Weapon>();
                            weapon.weaponDamage /= 2; 
                        }
                        break;

                    case BonusType.Shield:
                        if (playerEntity.Has<Shield>())
                        {
                            ref var shield = ref playerEntity.Get<Shield>();
                            if (shield.shieldObject != null)
                            {
                                Object.Destroy(shield.shieldObject);
                            }
                            playerEntity.Del<Shield>(); 
                        }
                        break;
                }
                playerEntity.Del<ActiveBonus>();
                playerEntity.Del<BonusEffect>();
            }
        }
    }
}