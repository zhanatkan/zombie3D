using Leopotam.Ecs;
using UnityEngine;

public class EnemyWaveSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld ecsWorld;
    private RuntimeData runtimeData;
    private EnemySpawner enemySpawner;

    private UI ui;

    private SpawnArea spawnArea;

    private int currentWave = 0;
    private int enemiesToSpawn;
    private int enemiesAlive;

    public void Init()
    {
        enemySpawner = Resources.Load<EnemySpawner>("EnemySpawner");

        GameObject spawnAreaObject = GameObject.Find("SpawnArea");
        if (spawnAreaObject != null)
        {
            spawnArea = spawnAreaObject.GetComponent<SpawnArea>();
        }

        StartNextWave();
    }
    private Vector3 GetRandomSpawnPosition()
    {
        return spawnArea?.GetRandomSpawnPosition() ?? Vector3.zero;
    }

    public void Run()
    {
        if (enemiesAlive == 0)
        {
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        currentWave++;
        ui.gameScreen.SetWave(currentWave);
        enemiesToSpawn = 2 + currentWave; 
        enemiesAlive = enemiesToSpawn;
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemyPrefab = enemySpawner.enemyPrefabs[Random.Range(0, enemySpawner.enemyPrefabs.Length)];
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject enemyObject = Object.Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            var enemyView = enemyObject.GetComponent<EnemyView>();
            InitializeEnemy(enemyView);
        }
    }

    private void InitializeEnemy(EnemyView enemyView)
    {
        var enemyEntity = ecsWorld.NewEntity();
        ref var enemy = ref enemyEntity.Get<Enemy>();
        ref var health = ref enemyEntity.Get<Health>();
        ref var enemyTransform = ref enemyEntity.Get<PositionComponent>();

        enemy.damage = enemyView.damage;
        enemy.meleeAttackDistance = enemyView.meleeAttackDistance;
        enemy.navMeshAgent = enemyView.navMeshAgent;
        enemy.transform = enemyView.transform;
        enemy.enemyObject = enemyView.enemyObject;
        enemy.meleeAttackInterval = enemyView.meleeAttackInterval;
        enemy.triggerDistance = enemyView.triggerDistance;
        enemy.isShootingEnemy = enemyView.isShootingEnemy;

        health.health = enemyView.startHealth;
        enemyView.entity = enemyEntity;
        if (enemyView.isShootingEnemy && enemyView.shootingEnemyData != null)
        {
            ref var shootingEnemy = ref enemyEntity.Get<ShootingEnemy>();
            shootingEnemy.shootInterval = enemyView.shootingEnemyData.shootInterval;
            shootingEnemy.shootDistance = enemyView.shootingEnemyData.shootDistance;
            shootingEnemy.nextShootTime = Time.time + shootingEnemy.shootInterval;

            enemy.projectilePrefab = enemyView.shootingEnemyData.projectilePrefab;
            enemy.projectileSocket = enemyView.shootingEnemyData.projectileSocket;

        }
        enemyEntity.Get<Idle>();
    }

    public void DecrementEnemyCount()
    {
        enemiesAlive--;
    }
}