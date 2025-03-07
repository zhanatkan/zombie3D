using Leopotam.Ecs;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
internal class PauseSystem : IEcsRunSystem
{
    private EcsFilter<PauseEvent> pauseFilter;
    private EcsFilter<ResumeEvent> resumeFilter;
    private RuntimeData runtimeData;
    private UI ui;
    public void Run()
    {
        foreach (var i in pauseFilter)
        {
            pauseFilter.GetEntity(i).Del<PauseEvent>();

            if (runtimeData.gameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                continue;
            }

            runtimeData.isPaused = true;
            Time.timeScale = 0f;
            ui.pauseScreen.Show(true);
        }
        foreach (var i in resumeFilter)
        {
            resumeFilter.GetEntity(i).Del<ResumeEvent>();

            runtimeData.isPaused = false;
            Time.timeScale = 1f;
            ui.pauseScreen.Show(false); 
        }
    }
}
public class PauseButtonSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
{
    private EcsWorld _world;
    private Button _pauseButton;
    private Button _resumeButton;
    private RuntimeData _runtimeData;
    public void Init()
    {
        _pauseButton = GameObject.Find("PauseButton")?.GetComponent<Button>();
        if (_pauseButton != null)
        {
            _pauseButton.onClick.AddListener(OnPauseButtonClicked);
        }
    }
    public void Run()
    {
        if (_runtimeData.isPaused && _resumeButton == null)
        {
            _resumeButton = GameObject.Find("ResumeButton")?.GetComponent<Button>();
            if (_resumeButton != null)
            {
                _resumeButton.onClick.AddListener(OnResumeButtonClicked);
            }
        }
    }

    private void OnPauseButtonClicked()
    {
        _world.NewEntity().Get<PauseEvent>();
    }
    private void OnResumeButtonClicked()
    {
        _world.NewEntity().Get<ResumeEvent>();
    }

    public void Destroy()
    {
        if (_pauseButton != null)
        {
            _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
        }
        if (_resumeButton != null)
        {
            _resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
        }
    }
}