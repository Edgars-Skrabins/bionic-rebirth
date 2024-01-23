using UnityEngine;

public class DroneHealth : Health
{
    [SerializeField] private GameObject m_turretDeathVFX;
    [SerializeField] private Transform m_turretExplosionLocationTF;
    [SerializeField] private AudioSource m_audioSourceTakeDamage;
    [SerializeField] private AudioSource m_audioSourceDeath;
    [SerializeField] private AudioClip[] m_clipShoot;

    public override void Die()
    {
        base.Die();
        m_audioSourceDeath.PlayOneShot(m_audioSourceDeath.clip);
        Instantiate(m_turretDeathVFX, m_turretExplosionLocationTF.position, Quaternion.identity);
    }

    public override void TakeDamage(float _damage, bool isPlayer)
    {
        base.TakeDamage(_damage, false);
        m_audioSourceTakeDamage.PlayOneShot(m_audioSourceTakeDamage.clip);
    }
}