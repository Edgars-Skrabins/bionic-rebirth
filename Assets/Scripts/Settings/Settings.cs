using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : Singleton<Settings>
{
    [SerializeField] public GameObject m_canvas;
    [SerializeField] private Slider[] m_sliders;
    private bool m_isPauseActive;
    private bool m_isWebGL;

    private void Start()
    {
        if (PlayerPrefs.GetFloat("Sens") == 0)
        {
            SetRes();
            ResetSettings();
        }

        GetSettings();
        //PlayerPref get then set all values for audio
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            TogglePause();
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void TogglePause()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            return;
        }

        PauseGame(!m_isPauseActive);
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            m_isPauseActive = true;
            m_canvas.SetActive(true);
            //animate pause opening
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            m_isPauseActive = false;
            m_canvas.SetActive(false);
            //Animate  pause closing
            Time.timeScale = 1;
            Cursor.lockState = SceneManager.GetActiveScene().buildIndex == 0
                ? CursorLockMode.None
                : CursorLockMode.Locked;
        }
    }

    public void ResetSettings()
    {
        SetSens(300);
        SetMastVol(0);
        SetMusicVol(0);
        SetSfxVol(0);
        SetUIVol(0);
    }

    private void GetSettings()
    {
        audioMixer.SetFloat("Master", PlayerPrefs.GetFloat("Master"));
        audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("Music"));
        audioMixer.SetFloat("Sfx", PlayerPrefs.GetFloat("Sfx"));
        audioMixer.SetFloat("UI", PlayerPrefs.GetFloat("UI"));
        m_sliders[0].value = PlayerPrefs.GetFloat("Sens");
    }

    #region Audio

    public AudioMixer audioMixer;

    public void SetMastVol(float volume)
    {
        audioMixer.SetFloat("Master", volume);
        PlayerPrefs.SetFloat("Master", volume);
        m_sliders[1].value = PlayerPrefs.GetFloat("Master");
    }

    public void SetMusicVol(float volume)
    {
        audioMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("Music", volume);
        m_sliders[2].value = PlayerPrefs.GetFloat("Music");
    }

    public void SetSfxVol(float volume)
    {
        audioMixer.SetFloat("Sfx", volume);
        PlayerPrefs.SetFloat("Sfx", volume);
        m_sliders[3].value = PlayerPrefs.GetFloat("Sfx");
    }

    public void SetUIVol(float volume)
    {
        audioMixer.SetFloat("UI", volume);
        PlayerPrefs.SetFloat("UI", volume);
        m_sliders[4].value = PlayerPrefs.GetFloat("UI");
    }

    public void SetSens(float sens)
    {
        PlayerPrefs.SetFloat("Sens", sens);
        m_sliders[0].value = PlayerPrefs.GetFloat("Sens");
    }

    #endregion

    #region Application Settings

    public void GetPlatform()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            m_isWebGL = true;
        }
        else
        {
            m_isWebGL = false;
        }
    }

    public void SetRes()
    {
        if (m_isWebGL)
        {
            Screen.SetResolution(1280, 720, true);
        }
        else
        {
            Screen.SetResolution(1920, 1080, true);
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ResetLevel()
    {
        PauseGame(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SwitchScene(string sceneName)
    {
        PauseGame(false);
        SceneManager.LoadScene(sceneName);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    #endregion
}
