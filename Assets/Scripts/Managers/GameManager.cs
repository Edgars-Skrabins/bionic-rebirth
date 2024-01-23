using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}