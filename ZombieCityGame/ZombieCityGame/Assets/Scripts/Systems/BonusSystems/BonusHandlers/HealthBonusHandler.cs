using Leopotam.Ecs;
public class HealthBonusHandler : IBonusHandler
{
    private EcsWorld ecsWorld;
    private UI ui;

    public HealthBonusHandler(EcsWorld ecsWorld, UI ui)
    {
        this.ecsWorld = ecsWorld;
        this.ui = ui;
    }

    public void ApplyBonus(EcsEntity playerEntity, ref BonusEffect effect)
    {
        ref var health = ref playerEntity.Get<Health>();

        if (health.health <= 80)
        {
            health.health += (int)effect.effectValue;
            ui.gameScreen.SetHealth(health.health);
        }
        else if (health.health == 90)
        {
            health.health += 10;
            ui.gameScreen.SetHealth(health.health);
        }
        effect.applied = true;

        playerEntity.Del<BonusEffect>();
    }
}