using Leopotam.Ecs;
using UnityEngine;

public struct EnemyProjectile
{
    public Vector3 direction;
    public GameObject projectileGO;
    public float speed;
    public int damage;
}
public struct EnemyBullet : IEcsIgnoreInFilter { }
