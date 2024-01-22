using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class PlayerHealth : Health
{

    [SerializeField] private GameObject m_deathScreen;
    [SerializeField] private PlayerMovement m_palyerMovementCS;
    [SerializeField] private PlayerLook m_palyerLookCS;
    [SerializeField] private Pistol m_pistolCS;
    [SerializeField] private Animator anim;
    [SerializeField] private Slider healthSlider;

    [SerializeField] private GameObject m_deathSound;
    private bool m_deathSoundPlayed = false;

    [SerializeField] private GameObject[] m_gettingHitSounds;

    private void Start()
    {
        float temp = m_maxHealth * (3 - PlayerPrefs.GetFloat("Difficulty"));
        m_health = temp;
        m_maxHealth = temp;
        healthSlider.maxValue = temp;
    }

    private void Update()
    {
        healthSlider.value = m_health;
    }

    public override void TakeDamage(float _damage, bool isPlayer)
    {
        base.TakeDamage(_damage,isPlayer);

        if(m_deathSoundPlayed) return;
        
        var obj = Instantiate(m_gettingHitSounds[UnityEngine.Random.Range(0, m_gettingHitSounds.Length)], transform.position, Quaternion.identity);
        obj.transform.SetParent(transform);

    }

    public override void Die()
    {
        anim.SetTrigger("PlayerDeath");
        m_pistolCS.enabled = false;
        m_palyerLookCS.enabled = false;
        m_palyerMovementCS.enabled = false;
        m_deathScreen.SetActive(true);
        Invoke(nameof(RestartGame),5f);
        if(m_deathSoundPlayed == false)
        {
            Instantiate(m_deathSound, transform.position, Quaternion.identity);
        }
        m_deathSoundPlayed = true;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
