using UnityEngine.AI;
using UnityEngine;
using Leopotam.Ecs;
public struct Enemy
{
    public NavMeshAgent navMeshAgent;
    public Transform transform;
    public GameObject enemyObject;
    public float meleeAttackDistance;
    public float triggerDistance;
    public float meleeAttackInterval;
    public int damage;
    public bool isShootingEnemy;
    public GameObject projectilePrefab;
    public Transform projectileSocket;
}
public struct Idle : IEcsIgnoreInFilter { }