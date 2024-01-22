using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    private AudioSource audioSource;
    private int lastScene;
    private bool musicAlreadyShot;
    
    private void Start()
    {
        lastScene = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            audioSource.clip = clips[0];
            musicAlreadyShot = false;
        }
        else
        {
            audioSource.clip = clips[1];
            musicAlreadyShot = true;
        }
    }

    private void Update()
    {
        if (lastScene != SceneManager.GetActiveScene().buildIndex)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                audioSource.clip = clips[0];
                audioSource.Play();
                lastScene = SceneManager.GetActiveScene().buildIndex;
                musicAlreadyShot = false;
            }
            else
            {
                if (musicAlreadyShot) return;
                audioSource.clip = clips[1];
                audioSource.Play();
                lastScene = SceneManager.GetActiveScene().buildIndex;
            }
        }
    }

    

}
