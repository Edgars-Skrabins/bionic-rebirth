using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EndSceneCollider : MonoBehaviour
{
    private GameObject Player;
    private Animator anim;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = Player.GetComponentInChildren<Canvas>().gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (EventManager.I.OnFacilityShutdownHasInvoked)
        {
            
            StartCoroutine(CompleteScene());
        }
        else
        {
            GetComponentInParent<Animator>().SetBool("ElevatorOpen", true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        GetComponentInParent<Animator>().SetBool("ElevatorOpen", false);
    }

    public void OpenElevator()
    {
        GetComponentInParent<Animator>().SetBool("ElevatorOpen", true);
    }

    IEnumerator CompleteScene()
    {
        GetComponentInParent<Animator>().SetBool("ElevatorOpen", true);
        anim.SetTrigger("FadeOn");
        yield return new WaitForSecondsRealtime(5); 
        int level = PlayerPrefs.GetInt("CurrentLevel") + 1;
        PlayerPrefs.SetInt("CurrentLevel", level);
        if (SceneManager.sceneCountInBuildSettings - 1 == SceneManager.GetActiveScene().buildIndex)
        {
            //Play escaped animation
            anim.SetTrigger("Escaped");
            yield return new WaitForSecondsRealtime(6);
            PlayerPrefs.SetInt("CurrentLevel", 0);
            PlayerPrefs.SetInt("PlayerProLevel", 0);
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        
    }
}
