using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject m_continueGameBtn;
    [SerializeField] private TMP_Dropdown m_dropdown;

    private void Start()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") <= 1 || PlayerPrefs.GetFloat("Difficulty") == 0)
        {
            PlayerPrefs.SetFloat("Difficulty", 1f);
            PlayerPrefs.SetInt("PlayerProLevel", 0);
            m_continueGameBtn.SetActive(false);
        }

        if (PlayerPrefs.GetFloat("Difficulty") == 2)
        {
            m_dropdown.value = 2;
        }
        else if (PlayerPrefs.GetFloat("Difficulty") == 1.5f)
        {
            m_dropdown.value = 1;
        }
        else
        {
            m_dropdown.value = 0;
            PlayerPrefs.SetFloat("Difficulty", 1f);
        }

        m_dropdown.RefreshShownValue();
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("CurrentLevel", 1);
        PlayerPrefs.SetInt("PlayerProLevel", 0);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel"));
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        Settings.I.m_canvas.SetActive(true);
    }

    public void OnDifficultyChange()
    {
        if (m_dropdown.value == 0)
        {
            PlayerPrefs.SetFloat("Difficulty", 1);
        }
        else if (m_dropdown.value == 1)
        {
            PlayerPrefs.SetFloat("Difficulty", 1.5f);
        }
        else
        {
            PlayerPrefs.SetFloat("Difficulty", 2);
        }
    }
}
