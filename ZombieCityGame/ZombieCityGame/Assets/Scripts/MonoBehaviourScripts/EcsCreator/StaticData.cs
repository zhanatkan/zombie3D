using UnityEngine;

[CreateAssetMenu(fileName = "StaticData", menuName = "GameResources/PlayerResources/ShootingEnemyData")]
public class StaticData : ScriptableObject
{
    public GameObject[] playerPrefabs;
    public float playerSpeed;
    public int playerHealth;
}
