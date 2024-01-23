using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneCollider : MonoBehaviour
{
    private Animator m_animator;
    private GameObject m_playerGO;

    private void Start()
    {
        m_playerGO = GameObject.FindGameObjectWithTag("Player");
        m_animator = m_playerGO.GetComponentInChildren<Canvas>().gameObject.GetComponent<Animator>();
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

    private IEnumerator CompleteScene()
    {
        GetComponentInParent<Animator>().SetBool("ElevatorOpen", true);
        m_animator.SetTrigger("FadeOn");
        yield return new WaitForSecondsRealtime(5);
        int level = PlayerPrefs.GetInt("CurrentLevel") + 1;
        PlayerPrefs.SetInt("CurrentLevel", level);
        if (SceneManager.sceneCountInBuildSettings - 1 == SceneManager.GetActiveScene().buildIndex)
        {
            //Play escaped animation
            m_animator.SetTrigger("Escaped");
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
