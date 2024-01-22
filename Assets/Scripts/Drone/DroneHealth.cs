using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneHealth : Health
{
    [SerializeField] private GameObject m_turretDeathVFX;
    [SerializeField] private Transform m_turretExplosionLocationTF;
    [SerializeField] private AudioSource audioSourceTakeDamage;
    [SerializeField] private AudioSource audioSourceDeath;
    [SerializeField] private AudioClip[] clipShoot;

    public override void Die()
    {
        base.Die();
        audioSourceDeath.PlayOneShot(audioSourceDeath.clip);
        Instantiate(m_turretDeathVFX, m_turretExplosionLocationTF.position, Quaternion.identity);
    }

    public override void TakeDamage(float _damage, bool isPlayer)
    {
        base.TakeDamage(_damage, false);
        audioSourceTakeDamage.PlayOneShot(audioSourceTakeDamage.clip);
    }
}
