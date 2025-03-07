using UnityEngine;
using Leopotam.Ecs;
public class PlayerInitSystem : IEcsInitSystem
{
    private EcsWorld ecsWorld;
    private StaticData staticData;
    private SceneData sceneData;
    private UI ui;
    private RuntimeData runtimeData;

    public void Init()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0); 

        if (selectedCharacterIndex < 0 || selectedCharacterIndex >= staticData.playerPrefabs.Length)
        {
            selectedCharacterIndex = 0; 
        }

        EcsEntity playerEntity = ecsWorld.NewEntity();

        ref var player = ref playerEntity.Get<Player>();
        ref var inputData = ref playerEntity.Get<PlayerInputData>();
        ref var hasWeapon = ref playerEntity.Get<HasWeapon>();
        ref var animatorRef = ref playerEntity.Get<AnimatorRef>();
        ref var health = ref playerEntity.Get<Health>();
        ref var transformRef = ref playerEntity.Get<TransformRef>();

        GameObject playerGO = Object.Instantiate(staticData.playerPrefabs[selectedCharacterIndex], sceneData.playerSpawnPoint.position, Quaternion.identity);
        player.playerTransform = playerGO.transform;
        player.playerAnimator = playerGO.GetComponent<Animator>();
        player.playerRigidbody = playerGO.GetComponent<Rigidbody>();
        player.playerSpeed = staticData.playerSpeed;

        var weaponEntity = ecsWorld.NewEntity();
        var weaponView = playerGO.GetComponentInChildren<WeaponSettings>();
        ref var weapon = ref weaponEntity.Get<Weapon>();
        weapon.owner = playerEntity;
        weapon.projectilePrefab = weaponView.projectilePrefab;
        weapon.projectileRadius = weaponView.projectileRadius;
        weapon.projectileSocket = weaponView.projectileSocket;
        weapon.projectileSpeed = weaponView.projectileSpeed;
        weapon.weaponDamage = weaponView.weaponDamage;
        weapon.currentInMagazine = weaponView.currentInMagazine;
        weapon.maxInMagazine = weaponView.maxInMagazine;
        weapon.totalAmmo = weaponView.totalAmmo;
        weapon.shootAudio = weaponView.shootAudio;

        health.health = staticData.playerHealth;

        transformRef.transform = playerGO.transform;
        runtimeData.playerEntity = playerEntity;

        ui.gameScreen.SetHealth(health.health);
        ui.gameScreen.SetAmmo(weapon.currentInMagazine, weapon.totalAmmo);

        hasWeapon.weapon = weaponEntity;

        playerGO.GetComponent<PlayerView>().entity = playerEntity;

        animatorRef.animator = player.playerAnimator;
    }
}