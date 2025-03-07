using Leopotam.Ecs;
using UnityEngine;
public class RuntimeData 
{
    public bool isPaused = false;
    public bool gameOver = false;
    public EcsEntity playerEntity;

    public Vector3 spawnAreaMin;
    public Vector3 spawnAreaMax;
}
