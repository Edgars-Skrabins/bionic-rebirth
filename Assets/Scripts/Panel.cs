using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Panel : InteractionObject
{
    private Animator anim;
    public UnityEvent eventTrigger;

    [SerializeField] private GameObject m_panelSound;
    private bool m_panelSoundPlayed;
    [SerializeField] private GameObject m_brainSound;
    private bool m_brainSoundPlayed;

    [SerializeField] private bool m_soundPlayed;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if(!m_soundPlayed && m_panelSoundPlayed == false)
        {
            m_panelSoundPlayed = true;
            Instantiate(m_panelSound, transform.position, Quaternion.identity);
        }
        // Play panel animation
        eventTrigger.Invoke();
        anim.SetTrigger("LeverTrigger");
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1) 
        {
            return;
        }
        GameEvents.I.InvokeOnFacilityShutdown();
    }

    public void PlayBrainSound()
    {
        if(!m_brainSoundPlayed)
        {
            m_brainSoundPlayed = true;
            Instantiate(m_brainSound, transform.position, Quaternion.identity);
        }
    }
}
