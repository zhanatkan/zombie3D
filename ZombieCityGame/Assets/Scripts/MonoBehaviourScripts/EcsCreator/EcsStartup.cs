using Leopotam.Ecs;
using UnityEngine;
public class EcsStartup : MonoBehaviour
{
    public StaticData configuration;
    public SceneData sceneData;
    public UI ui;
    public BonusStaticData bonus;

    [HideInInspector]public EcsWorld ecsWorld;
    private EcsSystems updateSystems;
    private EcsSystems fixedUpdateSystems;
    private EnemyWaveSystem waveSystem;
    private void Start()
    {
        Time.timeScale = 1f;

        ecsWorld = new EcsWorld();
        updateSystems = new EcsSystems(ecsWorld);
        fixedUpdateSystems = new EcsSystems(ecsWorld);
        waveSystem = new EnemyWaveSystem();
        RuntimeData runtimeData = new RuntimeData();
#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(ecsWorld);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(updateSystems);
#endif
        updateSystems
            .Add(new PauseButtonSystem())
            .Add(new PlayerInitSystem())
            .Add(new EnemyInitSystem())
            .OneFrame<TryReload>()
            .Add(new PlayerInputSystem())
            .Add(new PlayerRotationSystem())
            .Add(new PlayerAnimationSystem())
            .Add(new EnemyIdleSystem())
            .Add(new EnemyFollowSystem())
            .Add(new ShootingEnemySystem())
            .Add(new EnemyProjectileMoveSystem())
            .Add(new PlayerDeathSystem())
            .Add(new WeaponShootSystem())
            .Add(new SpawnProjectileSystem())
            .Add(new ProjectileMoveSystem())
            .Add(new ProjectileHitSystem())
            .Add(new DamageSystem())
            .Add(new ReloadingSystem())
            .Add(new EnemyDeathSystem())
            .Add(new BonusSpawnerSystem())
            .Add(new BonusIconSystem())
            .Add(new DestroySystem())
            .Add(waveSystem)
            .Add(new BonusEffectSystem(ecsWorld, ui))
            .Add(new BonusPickupSystem())
            .Add(new ActiveBonusSystem())
            .Add(new PauseSystem())
            .Add(new RestartSystem())
            .Inject(waveSystem)
            .Inject(configuration)
            .Inject(sceneData)
            .Inject(ui)
            .Inject(bonus)
            .Inject(runtimeData);

        fixedUpdateSystems
            .Add(new PlayerMoveSystem())
            .Inject(configuration)
            .Inject(sceneData)
            .Inject(runtimeData); 
        updateSystems.Init();
        fixedUpdateSystems.Init();
    }
    private void Update()
    {
        updateSystems?.Run();
    }
    private void FixedUpdate()
    {
        fixedUpdateSystems?.Run();
    }
    private void OnDestroy()
    {
        RuntimeData runtimeData = new RuntimeData();
        ecsWorld?.Destroy();
        ecsWorld = null;
        updateSystems?.Destroy();
        updateSystems = null;
    }
}