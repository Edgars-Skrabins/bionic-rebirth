using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
        
    }

}
