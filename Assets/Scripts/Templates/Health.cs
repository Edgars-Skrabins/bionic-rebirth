using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public float m_health;

    public float m_maxHealth;

    protected virtual void Update()
    {
        m_health = Mathf.Clamp(m_health, 0, m_maxHealth);
    }

    public virtual void TakeDamage(float _damage, bool isPlayer)
    {
        if (isPlayer)
        {
            m_health -= _damage;
        }
        else
        {
            m_health -= _damage * (3 - PlayerPrefs.GetFloat("Difficulty"));
            Debug.Log("Damage: " + _damage);
            Debug.Log("PlayerPref: " + PlayerPrefs.GetFloat("Difficulty"));
        }

        if (m_health <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int _healAmount)
    {
        m_health += _healAmount;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}