using UnityEngine;

public class TurretHealth : Health
{
    [SerializeField] private GameObject m_turretDeathVFX;
    [SerializeField] private Transform m_turretExplosionLocationTF;
    [SerializeField] private AudioSource m_audioSourceTakeDamage;
    [SerializeField] private AudioSource m_audioSourceDeath;
    [SerializeField] private AudioClip[] m_clipShoot;

    [SerializeField] private GameObject m_explosionAudio;

    public override void Die()
    {
        m_audioSourceDeath.PlayOneShot(m_audioSourceDeath.clip);
        Instantiate(m_turretDeathVFX, m_turretExplosionLocationTF.position, Quaternion.identity);
        Instantiate(m_explosionAudio, transform.position, Quaternion.identity);
        base.Die();
    }

    public override void TakeDamage(float _damage, bool isPlayer)
    {
        base.TakeDamage(_damage, false);
        m_audioSourceTakeDamage.PlayOneShot(m_audioSourceTakeDamage.clip);
    }
}
