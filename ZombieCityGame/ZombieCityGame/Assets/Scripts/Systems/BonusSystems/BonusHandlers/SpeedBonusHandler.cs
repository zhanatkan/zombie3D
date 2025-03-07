using Leopotam.Ecs;
using UnityEngine;
public class SpeedBonusHandler : IBonusHandler
{
    private EcsWorld ecsWorld;
    private UI ui;
    public SpeedBonusHandler(EcsWorld ecsWorld, UI ui)
    {
        this.ecsWorld = ecsWorld;
        this.ui = ui;
    }

    public void ApplyBonus(EcsEntity playerEntity, ref BonusEffect effect)
    {
        if (!playerEntity.Has<ActiveBonus>())
        {
            ref var player = ref playerEntity.Get<Player>();
            player.playerSpeed *= 1.5f;
            effect.timer = 5f;
            effect.applied = true;
            playerEntity.Get<ActiveBonus>() = new ActiveBonus { bonusType = effect.bonusType, timer = effect.timer };

            var speedIconEntity = ecsWorld.NewEntity();
            ref var speedIcon = ref speedIconEntity.Get<SpeedIcon>();
            speedIcon.iconObject = CreateIcon("SpeedIcon");
            speedIcon.timer = 5f;
        }
        return;
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