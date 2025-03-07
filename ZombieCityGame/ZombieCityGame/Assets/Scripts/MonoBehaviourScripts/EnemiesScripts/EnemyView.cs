using UnityEngine.AI;
using UnityEngine;
using Leopotam.Ecs;

public class EnemyView : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public GameObject enemyObject;
    public float meleeAttackDistance;
    public float triggerDistance;
    public float meleeAttackInterval;
    public int startHealth;
    public int damage;

    [SerializeField] public bool isShootingEnemy; 
    [SerializeField] public ShootingEnemyData shootingEnemyData;

    public bool IsShootingEnemy => isShootingEnemy;
    public ShootingEnemyData ShootingEnemyData => shootingEnemyData;

    public EcsEntity entity;
}
