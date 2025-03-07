using UnityEngine;
using Leopotam.Ecs;

public class RestartButton : MonoBehaviour
{
    private EcsWorld ecsWorld;

    private void Start()
    {
        ecsWorld = FindObjectOfType<EcsStartup>().ecsWorld;
    }

    public void RestartGame()
    {
        ecsWorld.NewEntity().Get<RestartEvent>();
    }
}