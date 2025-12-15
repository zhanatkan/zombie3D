using Leopotam.Ecs;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public EcsEntity entity;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void Shoot()
    {
        if (entity.IsNull())
        {
            return;
        }

        if (!entity.Has<HasWeapon>())
        {
            return;
        }

        var weaponEntity = entity.Get<HasWeapon>().weapon;
        if (weaponEntity.IsNull())
        {
            return;
        }
        weaponEntity.Get<Shoot>();
    }
    
    public void Reload()
    {
        if (entity.IsNull())
        {
            return;
        }

        if (!entity.Has<HasWeapon>())
        {
            return;
        }

        var weaponEntity = entity.Get<HasWeapon>().weapon;
        if (weaponEntity.IsNull())
        {
            return;
        }
        weaponEntity.Get<ReloadingFinished>();
    }
}