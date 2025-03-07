using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    private Bounds spawnBounds;

    private void Awake()
    {
        spawnBounds = GetComponent<Collider>().bounds;
    }

    public Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(spawnBounds.min.x, spawnBounds.max.x);
        float z = Random.Range(spawnBounds.min.z, spawnBounds.max.z);
        return new Vector3(x, spawnBounds.min.y, z);
    }
}