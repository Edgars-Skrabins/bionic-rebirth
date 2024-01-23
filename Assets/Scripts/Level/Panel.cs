using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Panel : InteractionObject
{
    public UnityEvent eventTrigger;

    [SerializeField] private GameObject m_panelSound;
    [SerializeField] private GameObject m_brainSound;

    [SerializeField] private bool m_soundPlayed;
    private Animator m_animator;
    private bool m_brainSoundPlayed;
    private bool m_panelSoundPlayed;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (!m_soundPlayed && m_panelSoundPlayed == false)
        {
            m_panelSoundPlayed = true;
            Instantiate(m_panelSound, transform.position, Quaternion.identity);
        }

        eventTrigger.Invoke();
        m_animator.SetTrigger("LeverTrigger");
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            return;
        }

        EventManager.I.InvokeOnFacilityShutdown();
    }

    public void PlayBrainSound()
    {
        if (m_brainSoundPlayed) return;
        m_brainSoundPlayed = true;
        Instantiate(m_brainSound, transform.position, Quaternion.identity);
    }
}
