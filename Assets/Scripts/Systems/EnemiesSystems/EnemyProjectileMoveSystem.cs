using Leopotam.Ecs;
using UnityEngine;
public class EnemyProjectileMoveSystem : IEcsRunSystem
{
    private EcsFilter<EnemyProjectile> enemyProjectiles;
    private EcsWorld ecsWorld;
    public void Run()
    {
        foreach (var i in enemyProjectiles)
        {
            ref var projectile = ref enemyProjectiles.Get1(i);

            projectile.projectileGO.transform.position += projectile.direction * projectile.speed * Time.deltaTime;

            if (Physics.Raycast(projectile.projectileGO.transform.position, projectile.direction, out var hit, projectile.speed * Time.deltaTime))
            {
                if (hit.collider.gameObject.TryGetComponent(out PlayerView playerView))
                {
                    ref var damageEvent = ref ecsWorld.NewEntity().Get<DamageEvent>();
                    damageEvent.target = playerView.entity;
                    damageEvent.value = projectile.damage;
                }
                Object.Destroy(projectile.projectileGO);
                enemyProjectiles.GetEntity(i).Destroy();
            }
        }
    }
}