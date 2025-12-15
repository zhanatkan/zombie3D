using System.Diagnostics;
using Leopotam.Ecs;
public class DamageSystem : IEcsRunSystem
{
    private EcsFilter<DamageEvent> damageEvents;
    private UI ui;
    public void Run()
    {
        foreach (var damageEvent in damageEvents)
        {
            ref var e = ref damageEvents.Get1(damageEvent);
            ref var health = ref e.target.Get<Health>();

            health.health -= e.value;

            if (health.health <= 0)
            {
                if(e.target.Has<Enemy>())
                {
                    e.target.Get<EnemyDeathEvent>();
                }
                else
                {
                    e.target.Get<DeathEvent>();
                }
            }
            if(e.target.Has<Player>())
            {
                ui.gameScreen.SetHealth(health.health);
            }
            damageEvents.GetEntity(damageEvent).Destroy();
        }
    }
}