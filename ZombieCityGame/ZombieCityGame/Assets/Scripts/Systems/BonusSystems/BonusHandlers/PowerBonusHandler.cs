using Leopotam.Ecs;
using UnityEngine;
public class PowerBonusHandler : IBonusHandler
{
    private EcsWorld ecsWorld;
    private UI ui;
    public PowerBonusHandler(EcsWorld ecsWorld, UI ui)
    {
        this.ecsWorld = ecsWorld;
        this.ui = ui;
    }

    public void ApplyBonus(EcsEntity playerEntity, ref BonusEffect effect)
    {
        if (!playerEntity.Has<ActiveBonus>())
        {
            if (playerEntity.Has<HasWeapon>())
            {
                ref var hasWeapon = ref playerEntity.Get<HasWeapon>();
                ref var weaponEcs = ref hasWeapon.weapon.Get<Weapon>();
                weaponEcs.weaponDamage *= 2;
            }
            effect.timer = 5f;
            effect.applied = true;
            playerEntity.Get<ActiveBonus>() = new ActiveBonus { bonusType = effect.bonusType, timer = effect.timer };

            var powerIconEntity = ecsWorld.NewEntity();
            ref var powerIcon = ref powerIconEntity.Get<PowerIcon>();
            powerIcon.iconObject = CreateIcon("PowerIcon");
            powerIcon.timer = 5f;
        }
    }

    private GameObject CreateIcon(string iconName)
    {
        var iconObject = ui.gameScreen.transform.Find(iconName)?.gameObject;
        if (iconObject != null)
        {
            iconObject.SetActive(true);
            return iconObject;
        }
        return null;
    }
}