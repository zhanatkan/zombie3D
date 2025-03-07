using UnityEngine;

[CreateAssetMenu(fileName = "BonusData", menuName = "GameResources/BonusResources/BonusData")]
public class BonusScriptableObject : ScriptableObject
{
    public BonusType bonusType;
    public GameObject bonusPrefab;
    public float effectValue;
    public GameObject pickupParticles; // Префаб системы частиц для подбора
}