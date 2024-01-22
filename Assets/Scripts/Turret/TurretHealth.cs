using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealth : Health
{

    [SerializeField] private GameObject m_turretDeathVFX;
    [SerializeField] private Transform m_turretExplosionLocationTF;
    [SerializeField] private AudioSource audioSourceTakeDamage;
    [SerializeField] private AudioSource audioSourceDeath;
    [SerializeField] private AudioClip[] clipShoot;
    
    [SerializeField] private GameObject m_explosionAudio;

    public override void Die()
    {
        audioSourceDeath.PlayOneShot(audioSourceDeath.clip);
        Instantiate(m_turretDeathVFX, m_turretExplosionLocationTF.position, Quaternion.identity);
        Instantiate(m_explosionAudio, transform.position, Quaternion.identity);
        base.Die();
    }

    public override void TakeDamage(float _damage, bool isPlayer)
    {
        base.TakeDamage(_damage, false);
        audioSourceTakeDamage.PlayOneShot(audioSourceTakeDamage.clip);
    }
}
