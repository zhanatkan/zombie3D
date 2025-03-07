using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSystem : IEcsRunSystem
{
    private EcsFilter<RestartEvent> restartFilter;

    public void Run()
    {
        foreach (var i in restartFilter)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            restartFilter.GetEntity(i).Destroy();
        }
    }
}