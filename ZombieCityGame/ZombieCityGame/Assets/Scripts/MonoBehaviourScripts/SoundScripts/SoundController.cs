using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundController : MonoBehaviour
{
    public string volumeParameter = "WorldAudio";
    public AudioMixer mixer;
    public Slider slider;
    private const float _multiplier = 20f;

    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderVolumeChanged);
    }
    private void HandleSliderVolumeChanged(float value)
    {
        var volumeValue = Mathf.Log10(value) * _multiplier;
        mixer.SetFloat(volumeParameter, volumeValue);
    }
}
