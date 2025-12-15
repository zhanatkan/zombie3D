using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;
public class EnemyInitSystem : IEcsInitSystem
{
    private EcsWorld ecsWorld;
    private UI ui; 
    public void Init()
    {
        foreach (var enemyView in Object.FindObjectsOfType<EnemyView>())
        {
            var enemyEntity = ecsWorld.NewEntity();

            ref var enemy = ref enemyEntity.Get<Enemy>();
            ref var health = ref enemyEntity.Get<Health>();
            ref var enemyPosition = ref enemyEntity.Get<PositionComponent>();
            enemyEntity.Get<Idle>();

            health.health = enemyView.startHealth;
            enemy.damage = enemyView.damage;
            enemy.meleeAttackDistance = enemyView.meleeAttackDistance;
            enemy.navMeshAgent = enemyView.navMeshAgent;
            enemy.transform = enemyView.transform;
            enemy.enemyObject = enemyView.enemyObject;
            enemy.meleeAttackInterval = enemyView.meleeAttackInterval;
            enemy.triggerDistance = enemyView.triggerDistance;
            enemy.isShootingEnemy = enemyView.isShootingEnemy;

            if (enemyView.isShootingEnemy && enemyView.shootingEnemyData != null)
            {
                ref var shootingEnemy = ref enemyEntity.Get<ShootingEnemy>();
                shootingEnemy.shootInterval = enemyView.shootingEnemyData.shootInterval;
                shootingEnemy.shootDistance = enemyView.shootingEnemyData.shootDistance;
                shootingEnemy.nextShootTime = Time.time + shootingEnemy.shootInterval;

                enemy.projectilePrefab = enemyView.shootingEnemyData.projectilePrefab;
                enemy.projectileSocket = enemyView.shootingEnemyData.projectileSocket;
            }
            enemyView.entity = enemyEntity;
        }
    }
}