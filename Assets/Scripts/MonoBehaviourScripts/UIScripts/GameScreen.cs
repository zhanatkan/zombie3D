using TMPro;
using UnityEngine;

public class GameScreen : Screen
{
    [Header("Weapon")]
    [SerializeField] private TextMeshProUGUI currentInMagazineLabel;
    [SerializeField] private TextMeshProUGUI totalAmmoLabel;
    [Header("Health")]
    [SerializeField] private TextMeshProUGUI healthCount;
    [Header("WavesUI")]
    [SerializeField] private TextMeshProUGUI waveCount;
    public void SetAmmo(int current, int total)
    {
        currentInMagazineLabel.text = current.ToString();
        totalAmmoLabel.text = total.ToString();
    }
    public void SetHealth(int currentHealth)
    {
        healthCount.text = currentHealth.ToString();
    }
    public void SetWave(int currentWave)
    {
        waveCount.text = currentWave.ToString();
    }
}