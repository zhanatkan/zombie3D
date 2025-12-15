using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[CustomEditor(typeof(EnemyView))]
public class EnemyViewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var enemyView = (EnemyView)target;

        enemyView.navMeshAgent = (NavMeshAgent)EditorGUILayout.ObjectField("NavMeshAgent", enemyView.navMeshAgent, typeof(NavMeshAgent), true);
        enemyView.enemyObject = (GameObject)EditorGUILayout.ObjectField("Enemy Object", enemyView.enemyObject, typeof(GameObject), true);
        enemyView.meleeAttackDistance = EditorGUILayout.FloatField("Melee Attack Distance", enemyView.meleeAttackDistance);
        enemyView.triggerDistance = EditorGUILayout.FloatField("Trigger Distance", enemyView.triggerDistance);
        enemyView.meleeAttackInterval = EditorGUILayout.FloatField("Melee Attack Interval", enemyView.meleeAttackInterval);
        enemyView.startHealth = EditorGUILayout.IntField("Start Health", enemyView.startHealth);
        enemyView.damage = EditorGUILayout.IntField("Damage", enemyView.damage);

        enemyView.isShootingEnemy = EditorGUILayout.Toggle("Is Shooting Enemy", enemyView.isShootingEnemy);

        if (enemyView.isShootingEnemy)
        {
            enemyView.shootingEnemyData = (ShootingEnemyData)EditorGUILayout.ObjectField("Shooting Enemy Data", enemyView.shootingEnemyData, typeof(ShootingEnemyData), false);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(enemyView);
        }
    }
}