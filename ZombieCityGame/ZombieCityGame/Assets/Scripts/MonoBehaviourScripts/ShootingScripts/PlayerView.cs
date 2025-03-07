using Leopotam.Ecs;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public EcsEntity entity;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ЛКМ для стрельбы
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R)) // Клавиша R для перезарядки
        {
            Reload();
        }
    }

    public void Shoot()
    {
        if (entity.IsNull()) return;

        if (!entity.Has<HasWeapon>()) return;

        var weaponEntity = entity.Get<HasWeapon>().weapon;
        if (weaponEntity.IsNull()) return;

        weaponEntity.Get<Shoot>();
        return;
    }
    public void Reload()
    {
        if (entity.IsNull()) return;

        if (!entity.Has<HasWeapon>()) return;

        var weaponEntity = entity.Get<HasWeapon>().weapon;
        if (weaponEntity.IsNull()) return;

        weaponEntity.Get<ReloadingFinished>();
    }
}