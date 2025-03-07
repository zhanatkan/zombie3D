using Leopotam.Ecs;
public class AmmoCountBonusHandler : IBonusHandler
{
    private EcsWorld ecsWorld;
    private UI ui;

    public AmmoCountBonusHandler(EcsWorld ecsWorld, UI ui)
    {
        this.ecsWorld = ecsWorld;
        this.ui = ui;
    }

    public void ApplyBonus(EcsEntity playerEntity, ref BonusEffect effect)
    {
        if (playerEntity.Has<HasWeapon>())
        {
            ref var hasWeapon = ref playerEntity.Get<HasWeapon>();
            ref var weaponEcs = ref hasWeapon.weapon.Get<Weapon>();
            weaponEcs.totalAmmo += (int)effect.effectValue;

            Weapon weaponMono = hasWeapon.weapon.Get<Weapon>();
            weaponMono.totalAmmo = weaponEcs.totalAmmo;
            weaponMono.currentInMagazine = weaponEcs.currentInMagazine;

            ui.gameScreen.SetAmmo(weaponMono.currentInMagazine, weaponMono.totalAmmo);
        }
        effect.applied = true;

        playerEntity.Del<BonusEffect>();
    }
}