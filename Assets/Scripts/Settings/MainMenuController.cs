using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject continueGameBtn;
    [SerializeField] private TMP_Dropdown dropdown;
    void Start()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") <= 1 || PlayerPrefs.GetFloat("Difficulty") == 0)
        {
            PlayerPrefs.SetFloat("Difficulty", 1f);
            PlayerPrefs.SetInt("PlayerProLevel", 0); 
            continueGameBtn.SetActive(false);
        }
        if (PlayerPrefs.GetFloat("Difficulty") == 2)
        {
            dropdown.value = 2;
            
        }
        else if (PlayerPrefs.GetFloat("Difficulty") == 1.5f)
        {
            dropdown.value = 1;
        }
        else
        {
            dropdown.value = 0;
            PlayerPrefs.SetFloat("Difficulty", 1f);
        }
        dropdown.RefreshShownValue();
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
        Settings.I.canvas.SetActive(true);
    }

    public void OnDifficultyChange()
    {
        if (dropdown.value == 0)
        {
            PlayerPrefs.SetFloat("Difficulty", 1);
        }else if (dropdown.value == 1)
        {
            PlayerPrefs.SetFloat("Difficulty", 1.5f);
        }
        else
        {
            PlayerPrefs.SetFloat("Difficulty", 2);
        }
    }
}
