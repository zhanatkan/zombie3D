using Leopotam.Ecs;
using UnityEngine;
public class ShootingEnemySystem : IEcsRunSystem
{
    private EcsFilter<ShootingEnemy, Enemy, PositionComponent> shootingEnemies;
    private EcsWorld ecsWorld;
    private RuntimeData runtimeData;

    public void Run()
    {
        foreach (var i in shootingEnemies)
        {
            ref var shootingEnemy = ref shootingEnemies.Get1(i);
            ref var enemy = ref shootingEnemies.Get2(i);
            ref var position = ref shootingEnemies.Get3(i);

            if (runtimeData == null || !runtimeData.playerEntity.IsAlive() || !runtimeData.playerEntity.Has<TransformRef>())
            {
                return;
            }

            if (Time.time < shootingEnemy.nextShootTime) continue;
            shootingEnemy.nextShootTime = Time.time + shootingEnemy.shootInterval;

            var playerPos = runtimeData.playerEntity.Get<TransformRef>().transform.position;
            var distanceToPlayer = Vector3.Distance(position.Position, playerPos);

            if (distanceToPlayer > shootingEnemy.shootDistance) continue;

            var projectileEntity = ecsWorld.NewEntity();
            ref var projectile = ref projectileEntity.Get<EnemyProjectile>();

            projectile.direction = (playerPos - enemy.projectileSocket.position).normalized;
            projectile.speed = 20;
            projectile.damage = enemy.damage;

            var projectileGO = Object.Instantiate(enemy.projectilePrefab, enemy.projectileSocket.position, Quaternion.identity);
            projectile.projectileGO = projectileGO;

            projectileEntity.Get<EnemyBullet>();
        }
    }
}