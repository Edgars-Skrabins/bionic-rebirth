using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderSettingsController : MonoBehaviour
{
    [SerializeField] private string SliderType;
    [SerializeField] private bool isAudioSetting;

    private void Start()
    {
        Settings settings = Settings.I;
        Slider slider = GetComponentInChildren<Slider>();
        slider.value = PlayerPrefs.GetFloat(SliderType);
        if (!isAudioSetting) { return; }
        AudioMixer audioMixer = settings.audioMixer;
        audioMixer.SetFloat(SliderType, PlayerPrefs.GetFloat(SliderType));
    }
}
