using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderSettingsController : MonoBehaviour
{
    [SerializeField] private string m_sliderType;
    [SerializeField] private bool m_isAudioSetting;

    private void Start()
    {
        Settings settings = Settings.I;
        Slider slider = GetComponentInChildren<Slider>();
        slider.value = PlayerPrefs.GetFloat(m_sliderType);
        if (!m_isAudioSetting)
        {
            return;
        }

        AudioMixer audioMixer = settings.audioMixer;
        audioMixer.SetFloat(m_sliderType, PlayerPrefs.GetFloat(m_sliderType));
    }
}
