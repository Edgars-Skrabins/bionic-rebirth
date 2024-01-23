using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField] private GameObject m_deathScreen;
    [SerializeField] private PlayerMovement m_palyerMovementCS;
    [SerializeField] private PlayerLook m_palyerLookCS;
    [SerializeField] private Pistol m_pistolCS;
    [SerializeField] private Animator m_anim;
    [SerializeField] private Slider m_healthSlider;

    [SerializeField] private GameObject m_deathSound;

    [SerializeField] private GameObject[] m_gettingHitSounds;
    private bool m_deathSoundPlayed;

    private void Start()
    {
        float temp = m_maxHealth * (3 - PlayerPrefs.GetFloat("Difficulty"));
        m_health = temp;
        m_maxHealth = temp;
        m_healthSlider.maxValue = temp;
    }

    private void Update()
    {
        m_healthSlider.value = m_health;
    }

    public override void TakeDamage(float _damage, bool isPlayer)
    {
        base.TakeDamage(_damage, isPlayer);

        if (m_deathSoundPlayed) return;

        GameObject obj = Instantiate(m_gettingHitSounds[Random.Range(0, m_gettingHitSounds.Length)], transform.position,
            Quaternion.identity);
        obj.transform.SetParent(transform);
    }

    public override void Die()
    {
        m_anim.SetTrigger("PlayerDeath");
        m_pistolCS.enabled = false;
        m_palyerLookCS.enabled = false;
        m_palyerMovementCS.enabled = false;
        m_deathScreen.SetActive(true);
        Invoke(nameof(RestartGame), 5f);
        if (m_deathSoundPlayed == false)
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
